"""Skills feature (Kimi C, M3). Owns skill state, training actions,
annual contributions, projection and requirement queries.

Integration (requested from A, contracts.md M3 §Skills):
  dispatcher: route "train_skill" -> training.cmd_train(env, payload, rng)
  annual hook: run training.annual_hook(env, rng) after work_income and
               before education, once per year per player
  projection: state["skills"] = projection.project(env)
  migration:  state.backfill(env) once for pre-M3 saves via the registry
"""
