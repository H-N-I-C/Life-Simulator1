"""M2.5 relationship_policy tests (Kimi B, Phase 2 step 1).

Real objects only: the actual catalog data file, the real evaluator, and
the FROZEN cross-contract fixtures from
docs/collab/M2.5/contracts/policy-fixtures.json executed input ->
expected output. Graph/policy reads go through small dict-backed fakes
that mimic A's read-only query surface (a true boundary).

Run: python3 -m unittest tests.test_relationship_policy
"""

import json
import os
import random
import sys
import unittest

ROOT = os.path.dirname(os.path.dirname(os.path.abspath(__file__)))
if ROOT not in sys.path:
    sys.path.insert(0, ROOT)

from src.game.features.relationship_policy import catalog, profiles, \
    evaluator  # noqa: E402

CONTRACTS = os.path.join(ROOT, "docs", "collab", "M2.5", "contracts")
CATALOG_CONTRACT = os.path.join(CONTRACTS, "policy-catalogs.json")
FIXTURES = os.path.join(CONTRACTS, "policy-fixtures.json")
RUNTIME_CATALOG = os.path.join(
    ROOT, "src", "game", "features", "relationship_policy", "data",
    "policy_catalogs.json")


def load_json(path):
    with open(path, encoding="utf-8") as f:
        return json.load(f)


class FakeGraph(evaluator.GraphView):
    """Dict-backed stand-in for A's read-only graph queries."""

    def __init__(self, persons, relatedness="unrelated", scores=None,
                 target_unions=0):
        self._persons = persons
        self._relatedness = relatedness
        self._scores = scores or {}
        self._target_unions = target_unions

    def person(self, person_id):
        try:
            return self._persons[person_id]
        except KeyError:
            from src.game.features.relationship_policy.catalog import \
                PolicyError
            raise PolicyError("unknown_person", person_id)

    def relatedness_category(self, a_id, b_id):
        return self._relatedness

    def relationship_scores(self, a_id, b_id):
        return self._scores

    def active_union_count(self, person_id):
        return self._target_unions


def fixture_persona(personas, ref):
    """Build graph-person + policy profile from a fixture persona ref.
    A dict ref is an inline persona; a string ref not present in the
    personas table is an intentionally unknown id."""
    if isinstance(ref, dict):
        persona = ref
    else:
        persona = personas[ref]
    person = {"person_id": persona["person_ref"],
              "age": persona.get("age", 30),
              "is_adult": persona.get("age", 30) >= 18,
              "living": True, "gender": persona.get("gender")}
    return person, dict(persona["profile"])


def build_fixture_case(personas, inp, overrides=None):
    """input (frozen fixture shape) -> (graph, policy_view, ids)."""
    persons, profiles_by_id = {}, {}
    i_person, i_profile = fixture_persona(personas, inp["initiator"])
    persons[i_person["person_id"]] = i_person
    profiles_by_id[i_person["person_id"]] = i_profile
    try:
        t_person, t_profile = fixture_persona(personas, inp["target"])
    except KeyError:
        t_person = t_profile = None  # unknown target id
    ctx = inp.get("context", {})
    if t_person is not None and "age_gap_years" in ctx:
        # the fixtures express gaps directly; real integration reads
        # protected chronology from the graph
        t_person["age"] = int(persons[i_person["person_id"]]["age"]) + \
            int(ctx["age_gap_years"])
        t_person["is_adult"] = t_person["age"] >= 18
    if t_person is not None:
        persons[t_person["person_id"]] = t_person
        profiles_by_id[t_person["person_id"]] = t_profile
    graph = FakeGraph(
        persons,
        relatedness=ctx.get("kinship_category", "unrelated"),
        scores=ctx.get("relationship", {}),
        target_unions=len(ctx.get("existing_unions_target", [])))
    cat = catalog.load_catalog()
    policy = evaluator.StaticPolicyView(cat, profiles_by_id,
                                        ctx.get("jurisdiction",
                                                "jur_metropolis_charter"),
                                        overrides=overrides)
    t_id = t_person["person_id"] if t_person is not None else inp["target"]
    return graph, policy, i_person["person_id"], t_id


class CatalogParityTests(unittest.TestCase):
    def test_runtime_catalog_matches_frozen_contract(self):
        contract = load_json(CATALOG_CONTRACT)
        runtime = load_json(RUNTIME_CATALOG)
        contract = dict(contract, _status=runtime.get("_status"))
        self.assertEqual(runtime, contract)


class EvaluatorFixtureTests(unittest.TestCase):
    """Execute the frozen fixtures against the real evaluator."""

    @classmethod
    def setUpClass(cls):
        cls.fixtures = load_json(FIXTURES)
        cls.personas = cls.fixtures["personas"]

    def run_fixture(self, inp):
        graph, policy, i_id, t_id = build_fixture_case(self.personas, inp)
        return evaluator.evaluate_offer(inp["interaction_kind"], i_id,
                                        t_id, graph, policy)

    def by_name(self, name):
        for fx in self.fixtures["fixtures"]:
            if fx["name"] == name:
                return fx
        self.fail("fixture %s missing" % name)

    def assert_fixture_result(self, fx, result):
        exp = fx["expected"]
        for key, want in exp["hard"].items():
            self.assertEqual(result["hard"].get(key), want, fx["name"])
        if exp["hard"]["ok"]:
            self.assertEqual(result["score"], exp["score"],
                             fx["name"])
            for key, want in exp["score_breakdown"].items():
                self.assertEqual(result["score_breakdown"].get(key), want,
                                 "%s breakdown %s" % (fx["name"], key))
            if "reasons_include" in exp:
                codes = {r["code"] for r in result["reasons"]}
                for code in exp["reasons_include"]:
                    self.assertIn(code, codes)
            refusal = exp.get("refusal")
            if refusal:
                self.assertTrue(result["refusal"]["possible"])
                self.assertAlmostEqual(result["refusal"]["p_refuse"],
                                       refusal["p_refuse"], places=2)
            proj = exp.get("projected_recognition")
            if proj:
                self.assertEqual(result["projected_recognition"], proj)
            cons = exp.get("projected_consequences")
            if cons:
                self.assertEqual(result["projected_consequences"], cons)

    def test_frozen_fixtures_all_pass(self):
        skipped = {"hard_validity_rejections", "neutral_defaults_no_name_"
                   "gender_inference"}
        for fx in self.fixtures["fixtures"]:
            if fx["name"] in skipped or "input_variants" in fx:
                continue
            with self.subTest(fixture=fx["name"]):
                self.assert_fixture_result(
                    fx, self.run_fixture(fx["input"]))

    def test_neutral_defaults_identical_regardless_of_names(self):
        fx = self.by_name("neutral_defaults_no_name_gender_inference")
        for variant in fx["input_variants"]:
            with self.subTest(variant=variant["label"]):
                result = self.run_fixture(fx["input"])
                self.assertEqual(result["score"], fx["expected"]
                                 ["same_score_for_all_variants"])

    def test_hard_validity_rejections(self):
        fx = self.by_name("hard_validity_rejections")
        base = {"interaction_kind": "propose_union",
                "initiator": "initiator_generic",
                "target": "target_riverlands_faithful",
                "context": {"relationship": {"chemistry": 50, "trust": 50},
                            "existing_unions_target": [],
                            "kinship_category": "none",
                            "age_gap_years": 2,
                            "jurisdiction": "jur_metropolis_charter",
                            "reputation": 0}}
        personas = dict(self.personas)
        for case in fx["cases"]:
            with self.subTest(case=case["input_overrides"]):
                inp = json.loads(json.dumps(base))
                persons_override = {}
                for key, value in case["input_overrides"].items():
                    if key == "target_age":
                        persons_override["target_age"] = value
                    elif key == "initiator_age":
                        persons_override["initiator_age"] = value
                    elif key == "target_dead":
                        persons_override["dead"] = True
                    elif key == "target_id_same_as_initiator":
                        inp["target"] = "initiator_generic"
                    elif key == "target_id":
                        inp["target"] = value
                graph, policy, i_id, t_id = build_fixture_case(personas, inp)
                if "target_age" in persons_override:
                    t = graph._persons[t_id]
                    t["age"] = persons_override["target_age"]
                    t["is_adult"] = t["age"] >= 18
                if "initiator_age" in persons_override:
                    p = graph._persons[i_id]
                    p["age"] = persons_override["initiator_age"]
                    p["is_adult"] = p["age"] >= 18
                if persons_override.get("dead"):
                    graph._persons[t_id]["living"] = False
                result = evaluator.evaluate_offer(inp["interaction_kind"],
                                                  i_id, t_id, graph,
                                                  policy)
                self.assertFalse(result["hard"]["ok"])
                self.assertEqual(result["hard"]["code"],
                                 case["expected_hard_code"])
                self.assertIsNone(result["score"])

    def test_unknown_interaction_kind_is_typed_error(self):
        graph, policy, i_id, t_id = build_fixture_case(
            self.personas,
            {"interaction_kind": "propose_union",
             "initiator": "initiator_generic",
             "target": "target_riverlands_faithful", "context": {}})
        with self.assertRaises(catalog.PolicyError) as cm:
            evaluator.evaluate_offer("elope_now", i_id, t_id, graph, policy)
        self.assertEqual(cm.exception.code, "invalid_value")

    def test_self_target_hard_error(self):
        graph, policy, i_id, _ = build_fixture_case(
            self.personas,
            {"interaction_kind": "flirt", "initiator": "initiator_generic",
             "target": "target_riverlands_faithful", "context": {}})
        result = evaluator.evaluate_offer("flirt", i_id, i_id, graph,
                                          policy)
        self.assertEqual(result["hard"]["code"], "self_target")


class ProfileTests(unittest.TestCase):
    def setUp(self):
        self.cat = catalog.load_catalog()

    def test_orientation_draw_uses_passed_rng_reproducibly(self):
        a = profiles.new_profile(self.cat, random.Random(7),
                                 "culture_meridian")
        b = profiles.new_profile(self.cat, random.Random(7),
                                 "culture_meridian")
        self.assertEqual(a, b)
        self.assertIn(a["orientation"], profiles.ORIENTATIONS)

    def test_neutral_profile_defaults(self):
        p = profiles.new_profile(self.cat, random.Random(1))
        self.assertEqual(p["faith_id"], "faith_unaffiliated")
        self.assertEqual(p["attraction_to_initiator"], 0.5)
        self.assertEqual(p["personal_tenet_overrides"], {})

    def test_culture_defaults_applied(self):
        p = profiles.new_profile(self.cat, random.Random(2),
                                 "culture_veldan")
        self.assertEqual(p["monogamy_preference"], 0.7)
        self.assertEqual(p["culture_id"], "culture_veldan")

    def test_validate_profile_patch_bounds_and_types(self):
        with self.assertRaises(catalog.PolicyError) as cm:
            profiles.validate_profile_patch({"openness": 1.5})
        self.assertEqual(cm.exception.code, "invalid_value")
        with self.assertRaises(catalog.PolicyError) as cm:
            profiles.validate_profile_patch({"openness": "high"})
        self.assertEqual(cm.exception.code, "type_error")
        with self.assertRaises(catalog.PolicyError) as cm:
            profiles.validate_profile_patch({"orientation": "curious"})
        self.assertEqual(cm.exception.code, "invalid_value")
        with self.assertRaises(catalog.PolicyError) as cm:
            profiles.validate_profile_patch({"person_id": "x"})
        self.assertEqual(cm.exception.code, "invalid_value")
        cleaned = profiles.validate_profile_patch({"openness": 0.7})
        self.assertEqual(cleaned, {"openness": 0.7})

    def test_validate_tenet_overrides(self):
        with self.assertRaises(catalog.PolicyError):
            profiles.validate_tenet_overrides({"plural_unions": "maybe"})
        cleaned = profiles.validate_tenet_overrides(
            {"plural_unions": "favoured"})
        self.assertEqual(cleaned, {"plural_unions": "favoured"})


class PredicateTests(unittest.TestCase):
    def setUp(self):
        self.cat = catalog.load_catalog()

    def test_predicates_match_contract_names(self):
        profile = {"faith_id": "faith_tidebound", "openness": 0.8,
                   "monogamy_preference": 0.2,
                   "attraction_to_initiator": 0.1}
        self.assertEqual(evaluator.predicate_faith_id(profile),
                         "faith_tidebound")
        self.assertEqual(evaluator.predicate_openness_band(profile), "high")
        self.assertEqual(evaluator.predicate_monogamy_band(profile), "low")
        self.assertEqual(evaluator.predicate_attraction_band(profile),
                         "low")
        self.assertEqual(evaluator.predicate_tenet_stance(
            self.cat, None, "faith_tidebound", "plural_unions"), "favoured")

    def test_save_local_override_wins_over_catalog(self):
        overrides = {"faiths": {"faith_tidebound": {
            "id": "faith_tidebound", "tenets": [
                {"id": "plural_unions", "stance": "disfavoured",
                 "scope": "public"}]}}}
        self.assertEqual(evaluator.predicate_tenet_stance(
            self.cat, overrides, "faith_tidebound", "plural_unions"),
            "disfavoured")
        # other tenets fall back to the catalog preset
        self.assertEqual(evaluator.predicate_tenet_stance(
            self.cat, overrides, "faith_tidebound", "interfaith_unions"),
            "disfavoured")

    def test_unknown_ids_resolve_neutral(self):
        self.assertEqual(evaluator.predicate_tenet_stance(
            self.cat, None, "faith_nope", "tenet_nope"), "neutral")
        jur = catalog.resolve_jurisdiction(self.cat, None, "jur_nope")
        self.assertEqual(jur["recognition"]["plural_civil"], "recognized")


if __name__ == "__main__":
    unittest.main()


class EditorTests(unittest.TestCase):
    def setUp(self):
        from src.game.features.relationship_policy import editor
        self.editor_mod = editor
        self.state = editor.new_policy_state("jur_metropolis_charter")
        self.state["profiles"] = {
            "p_a": {"faith_id": "faith_tidebound",
                    "culture_id": "culture_meridian",
                    "orientation": "bisexual", "openness": 0.5,
                    "jealousy": 0.4, "monogamy_preference": 0.5,
                    "attraction_to_initiator": 0.5,
                    "personal_tenet_overrides": {}},
            "p_b": {"faith_id": "faith_tidebound",
                    "culture_id": "culture_meridian",
                    "orientation": "heterosexual", "openness": 0.5,
                    "jealousy": 0.4, "monogamy_preference": 0.5,
                    "attraction_to_initiator": 0.5,
                    "personal_tenet_overrides": {"plural_unions":
                                                 "disfavoured"}},
        }

    def test_preview_invalid_scope_is_typed_error_no_mutation(self):
        ed = self.editor_mod.Editor(self.state)
        result = ed.preview([
            {"name": "edit_person_attributes", "scope": "faith_profile",
             "target_id": "p_a", "patch": {"openness": 0.9}}])
        self.assertFalse(result["valid"])
        self.assertEqual(result["errors"][0]["code"], "invalid_scope")
        self.assertEqual(self.state["profiles"]["p_a"]["openness"], 0.5)

    def test_preview_unknown_target_and_protected_field(self):
        ed = self.editor_mod.Editor(self.state)
        result = ed.preview([
            {"name": "edit_person_attributes", "scope": "person",
             "target_id": "p_zzz", "patch": {"openness": 0.9}},
            {"name": "edit_person_attributes", "scope": "person",
             "target_id": "p_a", "patch": {"age": 30}}])
        codes = {e["code"] for e in result["errors"]}
        self.assertEqual(codes, {"unknown_id", "protected_field"})

    def test_apply_person_attributes_audits_before_after(self):
        ed = self.editor_mod.Editor(self.state)
        out = ed.apply([
            {"name": "edit_person_attributes", "scope": "person",
             "target_id": "p_a", "patch": {"openness": 0.9,
                                           "monogamy_preference": 0.1}}],
            turn=12)
        self.assertEqual(self.state["profiles"]["p_a"]["openness"], 0.9)
        entry = out["audit"][0]
        self.assertEqual(entry["before"]["openness"], 0.5)
        self.assertEqual(entry["after"]["openness"], 0.9)
        self.assertEqual(entry["turn"], 12)
        self.assertEqual(self.state["policy_revision"], 0)  # no policy data

    def test_personal_tenets_affect_only_that_person(self):
        ed = self.editor_mod.Editor(self.state)
        ed.apply([{"name": "edit_personal_tenets",
                   "scope": "personal_tenets", "target_id": "p_a",
                   "patch": {"tenet_overrides":
                             {"plural_unions": "disfavoured"}}}])
        self.assertEqual(
            self.state["profiles"]["p_a"]["personal_tenet_overrides"]
            ["plural_unions"], "disfavoured")
        self.assertNotIn("interfaith_unions",
                         self.state["profiles"]["p_a"]
                         ["personal_tenet_overrides"])
        # p_b untouched
        self.assertEqual(
            self.state["profiles"]["p_b"]["personal_tenet_overrides"],
            {"plural_unions": "disfavoured"})

    def test_faith_profile_scope_lists_believers_without_override(self):
        ed = self.editor_mod.Editor(self.state)
        result = ed.preview([
            {"name": "edit_faith_profile", "scope": "faith_profile",
             "target_id": "faith_tidebound",
             "patch": {"tenet_stances": {"plural_unions": "disfavoured"}}}])
        self.assertTrue(result["valid"])
        # p_b has a personal override on plural_unions -> not affected
        self.assertEqual(result["affected_people"], ["p_a"])
        self.assertEqual(result["policy_revision_delta"], 1)

    def test_faith_profile_apply_bumps_revision_fires_hook_merges(self):
        calls = []
        ed = self.editor_mod.Editor(self.state,
                                    supersession_hook=calls.append)
        out = ed.apply([
            {"name": "edit_faith_profile", "scope": "faith_profile",
             "target_id": "faith_tidebound",
             "patch": {"tenet_stances": {"plural_unions": "disfavoured"}}}],
            turn=3)
        self.assertEqual(self.state["policy_revision"], 1)
        self.assertEqual(len(calls), 1)
        self.assertEqual(out["policy_revision"], 1)
        # merge: only the edited tenet changed, others keep preset stances
        from src.game.features.relationship_policy import catalog
        faith = catalog.resolve_faith(catalog.load_catalog(),
                                      self.state["overrides"],
                                      "faith_tidebound")
        by_id = {t["id"]: t["stance"] for t in faith["tenets"]}
        self.assertEqual(by_id["plural_unions"], "disfavoured")
        self.assertEqual(by_id["same_gender_unions"], "favoured")

    def test_evaluator_sees_editor_changes(self):
        ed = self.editor_mod.Editor(self.state)
        ed.apply([
            {"name": "edit_faith_profile", "scope": "faith_profile",
             "target_id": "faith_tidebound",
             "patch": {"tenet_stances": {"plural_unions": "disfavoured"}}}])
        graph = FakeGraph(
            {"p_a": {"person_id": "p_a", "age": 30, "is_adult": True,
                     "living": True, "gender": "female"},
             "p_b": {"person_id": "p_b", "age": 29, "is_adult": True,
                     "living": True, "gender": "male"}})
        policy = evaluator.StaticPolicyView(
            catalog.load_catalog(), self.state["profiles"],
            "jur_metropolis_charter", overrides=self.state["overrides"])
        result = evaluator.evaluate_offer("propose_plural_union", "p_a",
                                          "p_b", graph, policy,
                                          policy_revision=1)
        self.assertEqual(result["policy_revision"], 1)
        # p_b: personal override on plural (halved public -3 -> +8/-3),
        # same faith as p_a so interfaith tenet is not relevant
        self.assertEqual(result["score_breakdown"]["faith_tenets"], -3)
        self.assertEqual(result["score_breakdown"]["personal_tenets"], -8)

    def test_jurisdiction_edit_reprojects_and_bumps_law_version(self):
        class UnionGraph(FakeGraph):
            def all_unions(self):
                return [{"union_id": "u_1", "kind": "marriage",
                         "status": "active",
                         "recognition": {"status": "recognized",
                                         "jurisdiction_id":
                                         "jur_metropolis_charter",
                                         "law_version": 0,
                                         "basis": "ceremony"}}]
        ed = self.editor_mod.Editor(self.state, graph=UnionGraph({}))
        preview = ed.preview([
            {"name": "edit_jurisdiction", "scope": "jurisdiction",
             "target_id": "jur_metropolis_charter",
             "patch": {"recognition": {"plural_civil": "forbidden"}}}])
        self.assertTrue(preview["valid"])
        self.assertEqual(preview["affected_unions"], [{
            "union_id": "u_1", "current_recognition": "recognized",
            "projected_recognition": "recognized", "law_version": 0}])
        out = ed.apply([
            {"name": "edit_jurisdiction", "scope": "jurisdiction",
             "target_id": "jur_metropolis_charter",
             "patch": {"recognition": {"plural_civil": "forbidden"}}}])
        self.assertEqual(self.state["policy_revision"], 1)
        jur = self.state["overrides"]["jurisdictions"]["jur_metropolis_charter"]
        self.assertEqual(jur["law_version"], 1)
        self.assertEqual(jur["recognition"]["plural_civil"], "forbidden")
        # merge is read-time: the raw override holds only patched keys,
        # resolution over the preset preserves everything else
        resolved = catalog.resolve_jurisdiction(
            catalog.load_catalog(), self.state["overrides"],
            "jur_metropolis_charter")
        self.assertEqual(resolved["recognition"]["plural_civil"],
                         "forbidden")
        self.assertEqual(resolved["recognition"]["monogamous_civil"],
                         "recognized")

    def test_jurisdiction_bad_recognition_is_typed_error(self):
        ed = self.editor_mod.Editor(self.state)
        result = ed.preview([
            {"name": "edit_jurisdiction", "scope": "jurisdiction",
             "target_id": "jur_metropolis_charter",
             "patch": {"recognition": {"polygamy": "recognized"}}}])
        self.assertFalse(result["valid"])
        self.assertEqual(result["errors"][0]["code"], "invalid_value")

    def test_rename_validates_and_never_touches_policy(self):
        ed = self.editor_mod.Editor(self.state)
        bad = ed.preview([
            {"name": "rename_person", "scope": "person",
             "target_id": "p_a", "patch": {"first_name": ""}}])
        self.assertFalse(bad["valid"])
        out = ed.apply([
            {"name": "rename_person", "scope": "person",
             "target_id": "p_a", "patch": {"first_name": "Riley"}}])
        self.assertEqual(out["audit"][0]["after"],
                         {"first_name": "Riley"})
        self.assertEqual(self.state["policy_revision"], 0)

    def test_apply_is_atomic(self):
        ed = self.editor_mod.Editor(self.state)
        with self.assertRaises(catalog.PolicyError):
            ed.apply([
                {"name": "edit_person_attributes", "scope": "person",
                 "target_id": "p_a", "patch": {"openness": 0.9}},
                {"name": "edit_person_attributes", "scope": "person",
                 "target_id": "p_a", "patch": {"openness": 7.0}}])
        self.assertEqual(self.state["profiles"]["p_a"]["openness"], 0.5)
        self.assertEqual(self.state["editor_audit"], [])

    def test_convert_profile_typed_and_audited(self):
        ed = self.editor_mod.Editor(self.state)
        entry = ed.convert_profile("p_a", "faith_ancestorist", turn=9)
        self.assertEqual(self.state["profiles"]["p_a"]["faith_id"],
                         "faith_ancestorist")
        self.assertEqual(entry["before"], {"faith_id": "faith_tidebound"})
        self.assertEqual(entry["after"], {"faith_id": "faith_ancestorist"})
        with self.assertRaises(catalog.PolicyError) as cm:
            ed.convert_profile("p_a", "faith_nope")
        self.assertEqual(cm.exception.code, "unknown_id")
        with self.assertRaises(catalog.PolicyError) as cm:
            ed.convert_profile("p_zzz", "faith_tidebound")
        self.assertEqual(cm.exception.code, "unknown_id")

    def test_convert_profile_command_via_apply(self):
        ed = self.editor_mod.Editor(self.state)
        out = ed.apply([
            {"name": "convert_profile", "scope": "person",
             "target_id": "p_a",
             "patch": {"target_faith_id": "faith_ancestorist"}}], turn=4)
        self.assertEqual(self.state["profiles"]["p_a"]["faith_id"],
                         "faith_ancestorist")
        self.assertEqual(out["audit"][0]["command"], "convert_profile")
        self.assertEqual(out["audit"][0]["before"],
                         {"faith_id": "faith_tidebound"})
        # personal conversion is not shared policy data
        self.assertEqual(self.state["policy_revision"], 0)
        bad = ed.preview([
            {"name": "convert_profile", "scope": "person",
             "target_id": "p_a",
             "patch": {"target_faith_id": "faith_nope"}}])
        self.assertFalse(bad["valid"])
        self.assertEqual(bad["errors"][0]["code"], "unknown_id")
