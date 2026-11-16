# Growing the Matric Connect catalogue

Start with **public university undergraduate programmes for school leavers with a South African NSC**, with the public institution directory as the entry point. Bring in a small batch for one university and intake at a time. Track the number of reviewed listings and supported comparisons separately. Count distinct qualifications, not modules, web pages, or repeated campus entries.

TVET still belongs in the directory. Its programme import needs separate qualification types, campus links, intake periods and prerequisite rules. A college applicant may be applying based on a school grade or a prior qualification. Leave those requirements as references until that model is in place. Postgraduate comparison is outside the school-leaver workflow; keep using official institution links for discovery.

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

## Next releases

1. **Use reviewed rules as data.** Add a versioned rule format for institution-specific scores and subject options. Cover both mandatory sets and alternatives. Start by expressing the current CUT and Mandela checks in that format and run their existing examples before turning it on for another programme. Anything not captured by the calculation stays visible for the learner to check.
2. **Run one real Wits batch.** Start with 10–20 programmes in one faculty. Verify names, codes, intake dates and rules from official sources. Use the importer preview and examine the resulting pages on a test copy. Enable comparisons only when the relevant Wits scoring method and requirements have been verified and are supported.
3. **Add search and pagination before increasing volume.** Today the university page loads its full programme list. Add a safe way to review later intakes without replacing historical data or changing saved programme IDs. Then examine TVET-specific formats separately.
