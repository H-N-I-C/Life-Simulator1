"""Skills projection (additive under state["skills"]) and the read-only
requirement query A/B use for role/program prerequisites."""

from src.game.features.skills import data, state, training


def project(env):
    """Additive projection for the v2 Skills module. Never mutates."""
    player = env["player"]
    values = state.values(env, player.person_id)
    uses = state.uses_this_year(env, player.person_id)
    training_specs = []
    for spec in training.action_specs(env):
        labels = spec.pop("_labels", None)
        training_specs.append(spec)
    return {
        "schema_version": 1,
        "values": dict(values),
        "training": training_specs,
        "uses_this_year": dict(uses),
        "skills_meta": {s["id"]: {"label": s["label"],
                                  "description": s.get("description", "")}
                        for s in data.load()["skills"]},
    }


def requirements_met(person_values, requirements):
    """Read-only prerequisite check for A (roles) and B (programs).

    person_values: {skill_id: int} (state.values output)
    requirements: [{"skill": skill_id, "min": int}]

    Returns {"ok": True, "missing": []} or {"ok": False, "missing": [...]}.
    Never raises on unknown skill ids — they surface as missing instead of
    passing silently."""
    missing = []
    for req in requirements or []:
        skill = req.get("skill")
        need = req.get("min", 0)
        have = person_values.get(skill)
        if not isinstance(have, int) or have < need:
            missing.append({"skill": skill, "need": need,
                            "have": have if isinstance(have, int) else None})
    return {"ok": not missing, "missing": missing}


def requirement_labels(requirements):
    """Human-readable requirement rows for UIs: derived from versioned
    data labels, never fabricated."""
    out = []
    for req in requirements or []:
        sdef = data.skill_def(req.get("skill"))
        out.append({
            "skill": req.get("skill"),
            "label": sdef["label"] if sdef else str(req.get("skill")),
            "min": req.get("min", 0),
        })
    return out
