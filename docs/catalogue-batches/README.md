# Growing the Matric Connect catalogue

Use the public institution directory for **school-leaver pathways at public universities and TVET colleges**. Gather sources by province, structure them in a reviewed batch and import the whole batch together. Track the number of reviewed listings and supported comparisons separately. Count distinct qualifications, not modules, web pages, or repeated campus entries.

TVET listings carry their own qualification types, campuses, intake notes and prerequisite information where published. A college applicant may be applying based on a school grade or a prior qualification. Published grade, qualification and placement requirements can be recorded as checks where the institution establishes how they apply. Postgraduate listings use separate qualification routes and do not reuse school-leaver APS.

| Stage | What the team does | What learners see |
| --- | --- | --- |
| Collect | Find candidate programmes from official course finders, prospectuses, application calendars or data supplied by the institution. Record the official code, route, intake, campus, sources and page/section. Use parsers to make drafts. | No programme is published from a draft. |
| Review | Cross-check the qualification's identity, dates, scoring method and subject routes. Record the reviewer and date. Hold conflicts or incomplete requirements for follow-up. | Published requirements and source links for a reviewed intake. |
| Enable comparison | Record only the conditions the app can evaluate. Test threshold values, alternatives, missing marks, and any non-numeric conditions. Keep a calculation scoped to the institution, qualification, intake and applicant route. | Separate results for supported checks and a list of what still needs to be checked. |

The [Wits Course Finder](https://www.wits.ac.za/course-finder/) is a discovery starting point; review each offer on its official programme and admissions pages. The [DHET TVET directory](https://www.dhet.gov.za/SitePages/TVETlinks.aspx) identifies institutions. Use the [SAQA register](https://qspe.saqa.org.za/search.php) to cross-check a qualification's registration, not as evidence of current admission rules or campus availability.

## First release: preview and import of reviewed university listings

The standalone .NET 8 importer reads a JSON batch, checks it against an explicit Matric Connect SQLite database, and prints the additions. Use the already seeded [Wits 2027 example](wits-2027-example.json) to see the format and test duplicate detection.

From the repository root in Ubuntu/WSL, choose your database and run:

```bash
db="${XDG_DATA_HOME:-$HOME/.local/share}/MatricConnect/MatricConnect.db"
dotnet run --project tools/MatricConnect.CatalogueImport -- \
  --database "$db" --batch docs/catalogue-batches/wits-2027-example.json
```

On a database seeded with these programmes, expect **0 to add; 2 unchanged**. The preview opens the database read-only. For a new reviewed batch, preview on a test database and append `--apply` to the same command only after checking the plan. The importer revalidates and writes the whole batch in one transaction, or adds nothing. Replaying the same batch leaves it unchanged.

A batch is for one university and intake. Its reviewer attests that the source links were opened and the values were checked. The tool checks link format and domain, not the content of a page. Required sources are the programme requirements, application dates, and institutional scoring guide. For institution-controlled publications hosted elsewhere, hold the entry for explicit source review. An unconfirmed deadline is null, and an unconfirmed time stays null. Don't substitute a general date or copy last year's date. Use the numeric minimum or the published score alternatives; hold entries that cannot be represented by the current model. Don't write a guessed score.

The tool only adds new programmes. It rejects a batch if an entry would alter an existing listing, reuse a programme code or name for different data, or introduce a programme that would immediately match an existing comparison. Apply batches deliberately to the intended database; keep their reviewed manifests in version control so deployments can repeat the import. There is no automatic import on app startup. To undo a new batch in a test environment, restore the pre-import snapshot; in an environment with users, account for saved references before removing any records.

## Comparison rules

The [versioned comparison catalogue](comparison-rules.md) defines the existing CUT and Mandela checks as data. Existing programme intakes still use these rules. A province reference entry can also include an optional `comparison` with named routes and a list of minimums for each. Each minimum has a label, scale and threshold: `level`, `grade`, `nqf`, `aps`, `percent`, or `yesNo`. Use named routes for published alternatives and keep selection, contextual restrictions and unclear criteria in the published notes. Document the scoring method and do not substitute a generic APS.

## Reviewed batches

- [Wits Faculty of Science, 2027](wits-2027-science.md): 11 new application codes, with a source and minimum requirements for each. Follow the read-only preview before applying the batch to an intended database.

## Next releases

1. Review Wits comparisons separately from the import. Cross-check the Wits scoring method and validate the programme and intake rules before enabling Compare my results.
2. Add search and pagination before increasing volume. Today the university page loads its full programme list. Add a safe way to review later intakes without replacing historical data or changing saved programme IDs. Then examine TVET-specific formats separately.

## Province batches and faculties

Use a reviewed university `--batch` when an intake, application code, score or published score alternatives, and required evidence are verified. Its optional `faculty` field controls grouping. The existing faculty assignments for Wits and the other seeded programmes were matched against their published sources and applied in place. No programme IDs or comparison rules changed.

Use `--references` to load reviewed published offerings in a province, including ones without a confirmed intake. This format gives each institution a directory name, kind (`university` or `tvet`), and one or more records with a name, faculty or qualification group, official source, and availability. Keep campus, intake, and entry conditions blank when unconfirmed. Link historical data as historical rather than reusing it for new applicants.

Reference listings open their own details page and can be saved to the student shortlist. The page shows the published conditions we have, keeps unknown intake dates explicit and links to the official source. Entries with reviewed route rules support a comparison of the published minimums. Missing or conflicting entry requirements leave comparison unavailable. Use `--refresh` to preview updates to exact existing titles and groups from a reviewed batch. Adding `--apply` writes the additions and updates together and preserves existing IDs and shortlists. Refresh never removes listings and will reject an older verification date.

The [Northern Cape review](northern-cape-references.md) [Mpumalanga review](mpumalanga-references.md), and [North West review](north-west-references.md) show the batch format and official sources. The Northern Cape review also lists the national rollout order. Both import modes retain the read-only preview and atomic `--apply`.

Existing faculty assignments were checked against the [Wits Science catalogue](https://www.wits.ac.za/undergraduate/academic-programmes/faculty-of-science/), [CUT IT department](https://www.cut.ac.za/department/information-technology), the [Mandela University EBET](https://www.mandela.ac.za/getmedia/8d278dd0-b607-4ac3-a7d1-8450ba54a8e2/Prospectus-Faculty-of-EBET?disposition=attachment) and [Science](https://www.mandela.ac.za/getmedia/0bd96b8d-b5cc-4c60-a6a9-d596fa4396d6/Prospectus-Faculty-of-Science?disposition=attachment) prospectuses, the [UJ School of Computer Science](https://www.uj.ac.za/faculties/science/departments-2/academy-of-computer-science-and-software-engineering/courses-and-programmes/undergraduate-courses/), the [UP faculty catalogue](https://www.up.ac.za/faculty-of-engineering-built-environment-it/undergraduate-programmes), and the [UFS 2027 prospectus](https://www.ufs.ac.za/docs/librariesprovider44/undergraduate/ufs---ug-prospectus-2027---jan.pdf).
