"""Character-editor domain commands (frozen editor-protocol.json).

In-play editor: typed, allowlisted, revisioned. Preview computes scope
and validates; apply is atomic (all commands validate before anything
mutates), bumps policy_revision when policy data changed, appends
EditorAuditEntry records, and notifies the pending-supersession hook
(A implements it; injected here).

The editor operates on a per-save policy_state dict:

    {
      "jurisdiction_id": "jur_metropolis_charter",
      "profiles": {person_id: attraction_profile},
      "overrides": {"faiths": {}, "cultures": {}, "jurisdictions": {}},
      "policy_revision": 0,
      "editor_audit": []
    }

Money edits are omitted (no frozen ledger-adjustment command). Protected
fields (age/DOB/chronology/person_id/consent/parentage/RNG/pending/
ledger) are simply not in any allowlist — anything else fails typed
validation.
"""

import copy

from . import profiles as profile_mod
from .catalog import PolicyError, load_catalog, STANCE_VALUES, \
    RECOGNITION_VALUES

PROTECTED_PATTERNS = ("age", "date_of_birth", "chronology", "maturity",
                      "person_id", "consent", "acceptance",
                      "relationship_score", "biological_parents",
                      "parentage", "birth_risk", "seed", "rng",
                      "sampled", "pending", "money", "ledger", "balance")

UNION_TYPES = ("monogamous_civil", "plural_civil", "religious_only",
               "customary", "private")


class Editor:
    """Preview/apply engine. `graph` is an optional read-only view used
    for previews (affected people/unions); `supersession_hook` is A's
    on_policy_revision_bump, called after a policy-changing apply."""

    def __init__(self, policy_state, graph=None, supersession_hook=None):
        self.state = policy_state
        self.graph = graph
        self.hook = supersession_hook
        self.catalog = load_catalog()

    # ---------- command validation ----------

    def _validate_command(self, cmd):
        name = cmd.get("name")
        scope = cmd.get("scope")
        target = cmd.get("target_id")
        patch = cmd.get("patch")
        if not isinstance(name, str) or not isinstance(patch, dict):
            raise PolicyError("type_error", "command needs name and patch")
        for key in patch:
            if any(p in key for p in PROTECTED_PATTERNS):
                raise PolicyError("protected_field",
                                  "field %r is protected" % key)
        handler = getattr(self, "_cmd_" + name, None)
        if handler is None:
            raise PolicyError("invalid_value",
                              "unknown editor command %r" % name)
        return handler(cmd, scope, target, patch)

    # ---------- individual commands (validate+describe) ----------

    def _cmd_edit_person_attributes(self, cmd, scope, target, patch):
        if scope != "person" or not target:
            raise PolicyError("invalid_scope",
                              "edit_person_attributes needs scope=person")
        if target not in self.state["profiles"]:
            raise PolicyError("unknown_id",
                              "no policy profile for %r" % target)
        cleaned = profile_mod.validate_profile_patch(patch)
        return ("person", target, cleaned,
                {"profile": cleaned})

    def _cmd_edit_personal_tenets(self, cmd, scope, target, patch):
        if scope != "personal_tenets" or not target:
            raise PolicyError(
                "invalid_scope",
                "edit_personal_tenets needs scope=personal_tenets")
        if target not in self.state["profiles"]:
            raise PolicyError("unknown_id",
                              "no policy profile for %r" % target)
        overrides = patch.get("tenet_overrides", patch)
        cleaned = {"tenet_overrides":
                   profile_mod.validate_tenet_overrides(overrides)}
        return ("personal_tenets", target, cleaned, {})

    def _cmd_edit_faith_profile(self, cmd, scope, target, patch):
        if scope != "faith_profile" or not target:
            raise PolicyError("invalid_scope",
                              "edit_faith_profile needs scope=faith_profile")
        stances = patch.get("tenet_stances")
        if not isinstance(stances, dict) or not stances:
            raise PolicyError("invalid_value",
                              "tenet_stances (non-empty map) is required")
        for tenet_id, stance in stances.items():
            if not isinstance(tenet_id, str) or not tenet_id:
                raise PolicyError("invalid_value", "bad tenet_id")
            if stance not in STANCE_VALUES:
                raise PolicyError("invalid_value",
                                  "stance must be one of %s"
                                  % (STANCE_VALUES,),
                                  field=tenet_id,
                                  allowed=list(STANCE_VALUES))
        faith = self._resolve_faith_or_error(target)
        return ("faith_profile", target, {"tenet_stances": stances},
                {"tenets": [{"id": t, "stance": s, "scope": "public"}
                            for t, s in stances.items()],
                 "base_label": faith.get("label", target)})

    def _cmd_edit_jurisdiction(self, cmd, scope, target, patch):
        if scope != "jurisdiction" or not target:
            raise PolicyError("invalid_scope",
                              "edit_jurisdiction needs scope=jurisdiction")
        recognition = patch.get("recognition")
        if recognition is not None:
            if not isinstance(recognition, dict):
                raise PolicyError("type_error", "recognition must be a map")
            for union_type, status in recognition.items():
                if union_type not in UNION_TYPES:
                    raise PolicyError("invalid_value",
                                      "unknown union_type %r" % union_type,
                                      field=union_type,
                                      allowed=list(UNION_TYPES))
                if status not in RECOGNITION_VALUES:
                    raise PolicyError(
                        "invalid_value",
                        "status must be one of %s" % (RECOGNITION_VALUES,),
                        field=union_type, allowed=list(RECOGNITION_VALUES))
        consequences = patch.get("consequences")
        if consequences is not None:
            if not isinstance(consequences, list):
                raise PolicyError("type_error",
                                  "consequences must be a list")
            for cons in consequences:
                if not isinstance(cons, dict) \
                        or not cons.get("max_per_occurrence_key"):
                    raise PolicyError(
                        "invalid_value",
                        "each consequence needs a max_per_occurrence_key")
                if cons.get("effect") == "civil_fine":
                    amount = cons.get("amount", {})
                    lo, hi = amount.get("randint", [None, None])
                    if not (isinstance(lo, int) and isinstance(hi, int)
                            and 0 <= lo <= hi):
                        raise PolicyError(
                            "invalid_value",
                            "civil_fine needs amount.randint [lo, hi] with "
                            "0 <= lo <= hi")
        return ("jurisdiction", target,
                {k: v for k, v in (("recognition", recognition),
                                   ("consequences", consequences)) if v
                 is not None},
                {})

    def _cmd_convert_profile(self, cmd, scope, target, patch):
        if scope != "person" or not target:
            raise PolicyError("invalid_scope",
                              "convert_profile needs scope=person")
        if target not in self.state["profiles"]:
            raise PolicyError("unknown_id",
                              "no policy profile for %r" % target)
        faith_id = patch.get("target_faith_id")
        if not isinstance(faith_id, str) or not faith_id:
            raise PolicyError("invalid_value",
                              "target_faith_id is required")
        self._resolve_faith_or_error(faith_id)
        return ("convert_profile", target,
                {"target_faith_id": faith_id}, {})

    def _cmd_rename_person(self, cmd, scope, target, patch):
        if scope != "person" or not target:
            raise PolicyError("invalid_scope",
                              "rename_person needs scope=person")
        cleaned = {}
        for field in ("first_name", "last_name"):
            if field in patch:
                value = patch[field]
                if not isinstance(value, str) or not \
                        (1 <= len(value) <= 40):
                    raise PolicyError(
                        "invalid_value",
                        "%s must be a string of 1..40 chars" % field,
                        field=field, allowed=[1, 40])
                cleaned[field] = value
        if not cleaned:
            raise PolicyError("invalid_value", "nothing to rename")
        return ("rename_person", target, cleaned, {})

    # ---------- preview / apply ----------

    def preview(self, commands):
        """Validate everything and describe scope. Never mutates."""
        prepared = []
        errors = []
        for index, cmd in enumerate(commands):
            try:
                prepared.append(self._validate_command(cmd))
            except PolicyError as exc:
                errors.append({"command_index": index, "code": exc.code,
                               "field": exc.field,
                               "allowed": exc.allowed})
        if errors:
            return {"valid": False, "errors": errors,
                    "affected_people": [], "affected_unions": [],
                    "policy_revision_delta": 0, "journal_preview": []}
        affected_people = sorted({target for kind, target, _c, _d in prepared
                                  if kind in ("person",
                                              "personal_tenets",
                                              "rename_person")})
        faith_scopes = [target for kind, target, _c, _d in prepared
                        if kind == "faith_profile"]
        jur_scopes = [target for kind, target, _c, _d in prepared
                      if kind == "jurisdiction"]
        # believers affected by a faith-profile edit: anyone whose
        # profile faith matches and who lacks a personal override on the
        # edited tenets
        for faith_id in faith_scopes:
            stances = [p for k, t, c, d in prepared
                       if k == "faith_profile" and t == faith_id
                       for p in [c.get("tenet_stances", {})]][0]
            for pid, profile in self.state["profiles"].items():
                if profile.get("faith_id") != faith_id:
                    continue
                personal = profile.get("personal_tenet_overrides") or {}
                if any(t in personal for t in stances):
                    continue
                if pid not in affected_people:
                    affected_people.append(pid)
        affected_unions = []
        if jur_scopes and self.graph is not None:
            affected_unions = self._project_recognition(jur_scopes)
        policy_delta = 1 if (faith_scopes or jur_scopes) else 0
        journal = ["%s: %s" % (kind, target) for kind, target, _c, _d
                   in prepared]
        return {"valid": True, "errors": [],
                "affected_people": sorted(affected_people),
                "affected_unions": affected_unions,
                "policy_revision_delta": policy_delta,
                "journal_preview": journal}

    def _project_recognition(self, jurisdiction_ids):
        """For each active union in the graph whose jurisdiction is
        edited: current vs projected recognition (never mutates)."""
        projected = []
        unions = getattr(self.graph, "all_unions", None)
        if unions is None:
            return projected
        for union in unions():
            rec = union.get("recognition", {})
            if rec.get("jurisdiction_id") not in jurisdiction_ids:
                continue
            jur_override = self.state["overrides"].get(
                "jurisdictions", {}).get(rec["jurisdiction_id"], {})
            new_recognition = dict(rec)
            patch = jur_override.get("recognition", {})
            union_type = _union_type_of(union)
            if union_type in patch:
                new_recognition["status"] = patch[union_type]
            projected.append({
                "union_id": union["union_id"],
                "current_recognition": rec.get("status"),
                "projected_recognition": new_recognition["status"],
                "law_version": rec.get("law_version", 0)})
        return projected

    def apply(self, commands, turn=0):
        """Atomic apply: revalidates, then mutates. Returns the audit
        entries appended. The supersession hook fires once if the
        policy_revision changed."""
        prepared = [self._validate_command(cmd) for cmd in commands]
        before_revision = int(self.state.get("policy_revision", 0))
        audit = []
        for kind, target, cleaned, detail in prepared:
            before, after = self._apply_one(kind, target, cleaned)
            audit.append({"command": _command_name(kind), "scope": kind,
                          "target_id": target, "before": before,
                          "after": after, "turn": int(turn)})
        policy_changed = any(kind in ("faith_profile", "jurisdiction")
                             for kind, _t, _c, _d in prepared)
        if policy_changed:
            self.state["policy_revision"] = before_revision + 1
        self.state.setdefault("editor_audit", []).extend(audit)
        if policy_changed and self.hook is not None:
            self.hook({"affected_person_ids": sorted(self.state["profiles"]),
                       "affected_jurisdictions": sorted({
                           t for k, t, _c, _d in prepared
                           if k == "jurisdiction"})})
        return {"policy_revision": int(self.state["policy_revision"]),
                "audit": audit}

    def _apply_one(self, kind, target, cleaned):
        profiles = self.state["profiles"]
        if kind == "convert_profile":
            profile = profiles[target]
            before = {"faith_id": profile.get("faith_id")}
            profile["faith_id"] = cleaned["target_faith_id"]
            return before, {"faith_id": cleaned["target_faith_id"]}
        if kind == "person":
            profile = profiles[target]
            before = {k: profile.get(k) for k in cleaned}
            profile.update(cleaned)
            return before, dict(cleaned)
        if kind == "personal_tenets":
            profile = profiles[target]
            overrides = profile.setdefault("personal_tenet_overrides", {})
            before = {k: overrides.get(k) for k in cleaned["tenet_overrides"]}
            overrides.update(cleaned["tenet_overrides"])
            return before, dict(cleaned["tenet_overrides"])
        if kind == "faith_profile":
            faiths = self.state["overrides"].setdefault("faiths", {})
            entry = faiths.setdefault(target, {"id": target, "tenets": []})
            by_id = {t["id"]: t for t in entry["tenets"]}
            before = {}
            for tenet in entry["tenets"]:
                if tenet["id"] in cleaned["tenet_stances"]:
                    before[tenet["id"]] = tenet["stance"]
            for tenet_id, stance in cleaned["tenet_stances"].items():
                if tenet_id in by_id:
                    by_id[tenet_id]["stance"] = stance
                else:
                    new = {"id": tenet_id, "stance": stance,
                           "scope": "public"}
                    entry["tenets"].append(new)
                    by_id[tenet_id] = new
            return before, dict(cleaned["tenet_stances"])
        if kind == "jurisdiction":
            jurs = self.state["overrides"].setdefault("jurisdictions", {})
            entry = jurs.setdefault(target, {"id": target})
            before = {}
            if "recognition" in cleaned:
                rec = entry.setdefault("recognition", {})
                for union_type, status in cleaned["recognition"].items():
                    before[union_type] = rec.get(union_type)
                    rec[union_type] = status
            if "consequences" in cleaned:
                before["consequences"] = copy.deepcopy(
                    entry.get("consequences", []))
                entry["consequences"] = copy.deepcopy(
                    cleaned["consequences"])
            entry["law_version"] = int(entry.get("law_version", 0)) + 1
            return before, cleaned
        if kind == "rename_person":
            # names live on A's person records; the service adapter
            # forwards this audit entry to A's rename transition
            return {}, cleaned
        raise PolicyError("invalid_value", "cannot apply %r" % kind)

    # ---------- conversion (task item 2; also a v3 effect target) ------

    def convert_profile(self, person_id, target_faith_id, turn=0):
        """Typed conversion: sets the person's faith. Family/community
        reactions are C's content, not this command."""
        if person_id not in self.state["profiles"]:
            raise PolicyError("unknown_id",
                              "no policy profile for %r" % person_id)
        self._resolve_faith_or_error(target_faith_id)
        profile = self.state["profiles"][person_id]
        before = {"faith_id": profile.get("faith_id")}
        profile["faith_id"] = target_faith_id
        entry = {"command": "convert_profile", "scope": "person",
                 "target_id": person_id, "before": before,
                 "after": {"faith_id": target_faith_id}, "turn": int(turn)}
        self.state.setdefault("editor_audit", []).append(entry)
        return entry

    def _resolve_faith_or_error(self, faith_id):
        for faith in self.catalog.get("faiths", []):
            if faith["id"] == faith_id:
                return faith
        raise PolicyError("unknown_id", "unknown faith %r" % faith_id)


def _command_name(kind):
    return {"person": "edit_person_attributes",
            "personal_tenets": "edit_personal_tenets",
            "faith_profile": "edit_faith_profile",
            "jurisdiction": "edit_jurisdiction",
            "rename_person": "rename_person",
            "convert_profile": "convert_profile"}[kind]


def _union_type_of(union):
    kinds = {"marriage": "monogamous_civil",
             "civil_partnership": "monogamous_civil",
             "handfasting": "religious_only",
             "consort_union": "customary"}
    return kinds.get(union.get("kind"), "customary")


def new_policy_state(jurisdiction_id="jur_metropolis_charter"):
    return {"jurisdiction_id": jurisdiction_id, "profiles": {},
            "overrides": {"faiths": {}, "cultures": {}, "jurisdictions": {}},
            "policy_revision": 0, "editor_audit": []}
