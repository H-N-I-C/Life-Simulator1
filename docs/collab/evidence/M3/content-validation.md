# M3 content validation (Kimi D tooling)

Command: `python3 tools/validate_m3_content.py`

- files checked: 22
- errors: 2, warnings: 0
- ERROR `EREF` **careers.json:role:qualified_care_worker** : prerequisite qualification 'care_diploma' not awarded by any frozen program
- ERROR `EREF` **careers.json:role:nurse** : prerequisite qualification 'nursing_bachelor' not awarded by any frozen program

Family validators (careers/programs/skills): DRAFT mode, WARN-only, pending schema freeze in docs/collab/M3/02-contracts.md.
