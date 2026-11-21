# M4 social liveness sweep (Kimi D QA, 2026-11-21)

20 seeded lives, real play only: school 0->18 (first free option), a real job (apply -> stored offer -> accept), then to 25. Per-life thresholds: >=1 friend by 18, >=3 soc_ events, popularity not stuck, 0 surname collisions, employed at 25 with >= 1 coworker. Aggregate imbalance thresholds: bullying arc rate across lives in [15%, 50%], median friends at 18 in [2, 5].

Counts: {"PASS": 20, "aggregate_failures": 0}

- **PASS** bullying arc rate in [15%%, 50%%] — 9/20 lives = 45%
- **PASS** median friends at 18 in [2, 5] — median=4.0

- **PASS** seed 41000 — friends@18=5 soc=11 invites=2 (adult 0) bullies=0 band_changes=1 employed=True coworkers=5 collisions=0 
- **PASS** seed 41001 — friends@18=3 soc=11 invites=2 (adult 0) bullies=1 band_changes=3 employed=True coworkers=4 collisions=0 
- **PASS** seed 41002 — friends@18=6 soc=11 invites=2 (adult 0) bullies=0 band_changes=1 employed=True coworkers=5 collisions=0 
- **PASS** seed 41003 — friends@18=4 soc=11 invites=3 (adult 1) bullies=0 band_changes=1 employed=True coworkers=3 collisions=0 
- **PASS** seed 41004 — friends@18=4 soc=11 invites=2 (adult 0) bullies=1 band_changes=3 employed=True coworkers=4 collisions=0 
- **PASS** seed 41005 — friends@18=3 soc=10 invites=2 (adult 0) bullies=0 band_changes=1 employed=True coworkers=3 collisions=0 
- **PASS** seed 41006 — friends@18=3 soc=11 invites=2 (adult 0) bullies=1 band_changes=1 employed=True coworkers=5 collisions=0 
- **PASS** seed 41007 — friends@18=5 soc=11 invites=2 (adult 0) bullies=1 band_changes=1 employed=True coworkers=5 collisions=0 
- **PASS** seed 41008 — friends@18=5 soc=11 invites=2 (adult 0) bullies=0 band_changes=1 employed=True coworkers=3 collisions=0 
- **PASS** seed 41009 — friends@18=4 soc=11 invites=2 (adult 0) bullies=1 band_changes=3 employed=True coworkers=5 collisions=0 
- **PASS** seed 41010 — friends@18=4 soc=11 invites=2 (adult 0) bullies=0 band_changes=1 employed=True coworkers=5 collisions=0 
- **PASS** seed 41011 — friends@18=5 soc=11 invites=3 (adult 1) bullies=0 band_changes=1 employed=True coworkers=3 collisions=0 
- **PASS** seed 41012 — friends@18=4 soc=12 invites=3 (adult 1) bullies=0 band_changes=1 employed=True coworkers=4 collisions=0 
- **PASS** seed 41013 — friends@18=4 soc=11 invites=1 (adult 0) bullies=1 band_changes=1 employed=True coworkers=3 collisions=0 
- **PASS** seed 41014 — friends@18=4 soc=11 invites=2 (adult 0) bullies=0 band_changes=1 employed=True coworkers=5 collisions=0 
- **PASS** seed 41015 — friends@18=3 soc=10 invites=3 (adult 1) bullies=0 band_changes=1 employed=True coworkers=4 collisions=0 
- **PASS** seed 41016 — friends@18=3 soc=11 invites=2 (adult 0) bullies=1 band_changes=3 employed=True coworkers=4 collisions=0 
- **PASS** seed 41017 — friends@18=5 soc=11 invites=3 (adult 1) bullies=0 band_changes=1 employed=True coworkers=4 collisions=0 
- **PASS** seed 41018 — friends@18=3 soc=12 invites=2 (adult 0) bullies=1 band_changes=3 employed=True coworkers=5 collisions=0 
- **PASS** seed 41019 — friends@18=5 soc=11 invites=2 (adult 0) bullies=1 band_changes=3 employed=True coworkers=3 collisions=0 
