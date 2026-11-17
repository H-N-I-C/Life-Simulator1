# UNRESOLVED TASKS — BitLife 3.26.1 recovery (parked 2026-11-17, ~14:00 Sydney)

Parked by the user's instruction ("stop this task, store it in unresolved tasks to continue another time").
This file is the single resume point. Read together with `herdr-reports/HANDOFF.md` (especially §27–§28).

## State at park time (all verified)

- **Fixer source: `tools/RecoveredMethodFixer/` asserts `Reconstructed 142 methods` and passes on a
  copy** (scratch dir with `Assembly-CSharp.dll` + `Assembly-CSharp-firstpass.dll` + real Plugins as
  resolver arg). Exit 0. `IlInvalidScan` on the copy: **0 inconsistent stack-height joins**;
  value-type-invalid queue 1185 → 1180 (the five new methods left it).
- **The shared project DLL (`reconstructed/.../Assets/Plugins/Assembly-CSharp.dll`, mtime 13:13:17)
  was NOT rewritten** since the 13:13 demolition run — desktop (dsh-desktop-h2) was mid-bisect on
  build 44's regression. It does NOT yet contain: t1h's 13:18 GetNPCWinningDriver generic fix, nor
  the five RacingCupSeriesActiveRace repairs. **First step on resume: confirm desktop is done
  bisecting, then run the fixer on the real Plugins dir and expect `Reconstructed 142 methods`.**
- Office messaging at park time: t1h/t2m/t2y "NOT READY: can't reach Nexiant (VPN?)";
  desktop agents had no reachable terminal. Stop/park notices could not be delivered interactively —
  this file is the notification.

## Completed and verified (do not redo)

| item | evidence |
| --- | --- |
| GetNPCWinningDriver (0x3967d78) | `RacingCupMethodRepairs.cs` + t1h's 13:18 concrete-generic fix; copy-verified: locals now `Enumerator/KeyValuePair<SimRaceCarDriver,RacingCupSeriesResults>` |
| Street/Circuit/Demolition `RefreshScenario` (0x396afec / 0x3960e7c / 0x3964494) | `RacingCupScenarioMethodRepairs.cs`; IL dumps verified; literals decoded from metadata tokens |
| `GetPlayerTotalPoints` (0x3953eb8), `GetPlayerTotalTime` (0x3953ffc), `GetRacerTotalPoints` (0x3954214), `GetRacerTotalTime` (0x39543bc), `GetCurrentPlacement` (0x3954624) | `RacingCupActiveRaceMethodRepairs.cs`; all five copy-verified (`Reconstructed 142`, 0 imbalanced joins); `GetRacerTotalTime` + `GetCurrentPlacement` + `GetRacerTotalPoints` IL dumps read line-by-line; `GetPlayerTotalTime` shares the exact same `?? TimeSpan.Zero` shape (fix verified in the racer twin) |
| Reviews #62/#66, tasks #50, profile-decimal pair (#56 partial) | `herdr-reports/review-62-compliance.md`, `review-66-racing-cup.md`, HANDOFF §23–§26 |

## Remaining work, in priority order (resume here)

1. **Apply + build**: after desktop's bisect concludes, run the fixer on the real Plugins dir
   (expect 142), then the next Unity export per HANDOFF §7 (`tools/iterate_webgl_build.sh`, shader
   cache preserved). Build 44's crash cause (open `!0,!1` locals) is fixed in source.
2. **School-activity** (open item of #56): `ScenarioSchoolActivityJoin::ChoiceSelectedAtIndex(Int32)`,
   native **0x39e29d0** (idx 27248), ~1,670 instructions, ~185 usage slots, ~40 call targets.
   NOT recovered; the dummy has 256 `NoteDecompilerIssue` sites and is not surgically repairable.
   Decode already started: entry is `ctx`-stack zeroing + `choice = ChoiceArray[index]` via
   `IList::get_Item` (vtable slot 0x2e8) + cast, then `bl 0x68391d8` on `[choice+0x10]`; helpers
   0x6839b80 / 0x412ae8c / 0x412ada8 / 0x3a29018 / 0x4032404 / 0x6835438 / 0x3ff26b0 unresolved yet.
   Use the literal-decoding chain in HANDOFF §27 for the ~185 slots.
3. **Racing-cup peripherals still dummies** (scanner-flagged unless noted):
   `RacingCupSeriesActiveRace::ConvertTrackResults(TrackResults)` (only family member still in the
   1,180-flagged queue), `GetRacingCupSeriesWinnings(String)` (DataHub data-layer — see §21 stub
   caveat), `RacingCupSeriesRaceResultsFactory::CreateScenario(RacingCupSeriesResults)`,
   `SimRacingCup::.ctor(String,RACING_LICENSE,RACING_TRACK_CATEGORY,List<SimRacingTrack>)`, plus the
   class helpers `CreateTableOrder`/`CreatePointTotalDictionary` (street/circuit/demolition variants —
   not flagged but dummy bodies).
4. **The ~1,180-method scanner queue** and IL2CPP-as-oracle loop (HANDOFF §22) — needs the user's
   option-1/2/3 decision; no authorization to stub was given.

## Key facts for whoever resumes

- Literal/usage-token decode: kind = token bits 29–31, **index = bits 1–28** (bit 0 runtime flag);
  string literals via the 71,700-entry offset table in `global-metadata.dat` (HANDOFF §27 has the
  full recipe). The naive low-29-bit reading yields plausible WRONG strings.
- `tools/va_to_method.py <VA>` (reverse resolver), `tools/FieldOffsetDump` (mind its
  align-to-field-size bug — always calibrate against native ldfld offsets).
- Layouts: HANDOFF §27 (registration struct, activeRace fields, TrackResults: Score@0x10,
  Placement@0x14, FinishTime(Nullable: HasValue@0x18, ticks@0x20), TrackCompleted@0x28,
  CompetitorScores@0x30).
- Shared thunk identities: List::get_Item 0x633572c, List::GetEnumerator 0x63369f0, MoveNext
  0x5f0089c, Dispose 0x5f00898, Dict::TryGetValue 0x5db5bd8, Dict::get_Item 0x5db3e18,
  TimeSpan::op_Addition 0x69c7990, op_GreaterThan 0x69c7aa4 (verified `cmp; cset gt`).
- Copy-verification protocol (from t1h): never rewrite the shared DLL without desktop's go; use a
  scratch copy **including Assembly-CSharp-firstpass.dll** (3 of the repairs live there — a copy
  with only Assembly-CSharp.dll prints 134, not the full count) and pass the real Plugins dir as
  the resolver argument.
- Evidence hashes (t1h, still valid): UnityFramework sha256 6b4a4b4568a1c53940c216a3fd126ae21f4582a8,
  global-metadata.dat sha256 6a691594910cb775ce18f1c1dd7794b13600d2f9.

## Scratch artifacts (safe to delete, kept for reference)

`/private/var/folders/wx/p8phhhs15s593tchy8sgp1rw0000gn/T/opencode/racing68/` (street/circuit/school
disassemblies), `.../opencode/t2y/` (copy-verified patched DLLs). The `school.asm` disassembly
(0x39e29d0, 2,000 instructions) is the head start for item 2.
