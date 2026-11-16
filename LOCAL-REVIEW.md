Local setup and security review — 2026-11-16

Source: https://github.com/H-N-I-C/Life-Simulator1
Reviewed revision: 46d69c02b1196a4e01b8dbc1a1cf107a7252a107

Run: lifesim
Alternative: /home/user/Life-Simulator1/play.sh
Requires only system Python; no packages or install hooks were run.

Reviewed Python import/call surfaces, filesystem and shell operations, tracked
file types, editor settings and GitHub workflows. No apparent runtime telemetry,
networking, credential collection, persistence, obfuscated payloads or malicious
behavior was found in these checks. social_media.py is an in-game model only.
The game writes saves under game_saves and crash traces to error.log locally.
The clear-screen shell command is a fixed command. GitHub workflows are remote
CI configuration and are not executed when playing locally.
pygettext.py is an optional translation utility, not part of game startup; it
contains eval with empty builtins and was not executed during setup.

Security changes:
- Replaced unrestricted pickle loading with an explicit allowlist of game classes
  and collections.deque; arbitrary globals and persistent references are rejected.
- Save listing excludes symlinks and non-pickle files; loading rejects paths
  outside the save directory and symlinks.
- Saved filesystem paths are replaced with the actual local save path on load.
- Added play.sh and ~/.local/bin/lifesim; Python ignores PYTHON* environment
  variables and user site packages when started through this launcher.

Validation passed: source syntax, interactive startup into gameplay, new player,
save/load/resave, malicious pickle rejection without executing its payload,
and save symlink rejection. Test characters were not retained.

Limitations: this is a targeted source review, not a guarantee of safety or a
full manual audit of every game branch. OS sandbox launch was attempted but this
environment denies its required namespace/socket operations. The launcher is
NOT an OS sandbox, and the process retains normal user permissions. Do not import
untrusted saves; malformed data can still cause errors or resource exhaustion.
Future upstream updates require a fresh review and may conflict with local fixes.
