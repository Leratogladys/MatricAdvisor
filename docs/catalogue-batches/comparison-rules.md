# Reviewed programme comparisons

[programme-comparisons.json](../../MatricConnect.Core/Data/programme-comparisons.json) is the versioned comparison catalogue. It ships with the app as part of a reviewed release. It is separate from the programme import batches and does not require a database migration. A listing imported in a batch continues to show its published requirements even when it has no comparison.

## Version 1

| Field | What to record |
| --- | --- |
| `scoreSchemes` | A reusable institution-specific ID, label, maximum and official scoring guide URL. Scores are entered by the learner; the tool does not calculate them from school marks or reuse a generic profile APS. |
| `university`, `programmeCode`, `intakeYear`, `applicantRoute` | The reviewed scope. Version 1 supports South African NSC applicants only. Each programme and intake has at most one comparison; do not combine alternative campus, intake or qualification rules into one entry. |
| `requirementsUrl`, `verifiedOn` | The reviewed programme source and its verification date. The app enables the comparison only if these and the scoring guide match the catalogue intake. A revised intake or source needs another review. |
| `minimumScore`, `subjectOptions` | A learner chooses one subject option. That option requires both its mark and the score. Either give one common `minimumScore` for the programme or specify it on every option. |
| `requiredMarks` | Optional additional subjects that must all be entered and checked independently of the chosen option. Do not list an alternative subject here or put it in both places. |
| `remainingRequirements`, `extendedCurriculum` | Checks that still need confirmation (such as an NSC endorsement, tests or selection). The optional extended curriculum score range shows guidance only. |

The result shows minimums individually; it does not make an admission decision. Version 1 does not model alternative groups beyond the single subject choice, campus-specific conditions, or routes that do not fit this input form. Keep those programmes as reference listings until their requirements can be represented correctly.

## Review and verification

1. Review the programme page, official scoring guide and intake. Keep the corresponding published catalogue evidence consistent; a rule file edit cannot update an existing database record by itself.
2. Add or update the JSON entry, then run this from the repository root in Ubuntu/WSL:

   ```bash
   dotnet run --project tools/MatricConnect.CatalogueImport -- --check-rules
   ```

   This checks the rule file without opening a database. Invalid rules are logged by the website and leave reference pages available. The importer refuses to run with an invalid rule file, and it reserves comparison codes when planning a new import.
3. Check the programme page on a test database: a result at the minimum, just below a threshold, missing or unsupported marks, and whether the comparison is hidden when the intake or evidence does not match.
4. Rerun the CUT and Mandela cases. For CUT `DP_ITC`, use APS 27 and Computer Science 40%, then APS 26 and Mathematical Literacy 60%. For Mandela `7224`, AS 344 and Mathematical Literacy 60% should show a score below the minimum and a mark that meets it.

Only the two existing 2027 pilots are enabled in this change. New comparison entries should be reviewed separately from programme batches.
