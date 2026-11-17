# North West programme review

The [reviewed batch](north-west-references.json) has 89 programme listings and 88 comparisons. It covers part of the province's published catalogue.

| Institution | Listings | Comparisons | Evidence and gaps |
| --- | ---: | ---: | --- |
| North-West University | 24 | 24 | First set across all eight [undergraduate faculties](https://studies.nwu.ac.za/undergraduate-studies/fields-study). Admission scores use the university’s [2027 prospectus](https://studies.nwu.ac.za/sites/studies.nwu.ac.za/files/files/undergrad/2027-Grade-12-prospectus.pdf). The remaining undergraduate programme rows and postgraduate yearbooks are outside this batch. |
| ORBIT TVET College | 21 | 20 | Published [2026 occupational schedule](https://www.orbitcollege.co.za/PQM%20OCCUPATIONAL%20QUALIFICATIONS%20OFFERED%20FROM%202026%20%281%29.pdf), [NC(V) prospectus](https://www.orbitcollege.co.za/2026%20NCV%20Programmes.pdf) and [admission policy](https://www.orbitcollege.co.za/Policy%20Admission%202025.pdf). A placement score example does not set a minimum. The pre-vocational listing describes a referral route; no standalone minimum was published there. Report 191 availability remains open for review. |
| Vuselela TVET College | 35 | 35 | All 35 entries in the [public programme index](https://vuselelacollege.co.za/programmes), including routes requiring a school subject or an existing qualification. Entry requirements do not verify application dates. |
| Taletso TVET College | 9 | 9 | Named NC(V) programmes in the [official college app’s 9 September 2026 admission notice](https://taletsotvetcollege.app/). The poster lists a 1–30 September 2026 window for the 2027 intake and gives some school criteria by subject area. The app checks the documented criteria only. Taletso’s other qualifications and occupational notices still need review. |

To preview this batch separately in Ubuntu/WSL after starting the updated app to migrate the intended database:

```bash
catalogue_db="${XDG_DATA_HOME:-$HOME/.local/share}/MatricConnect/MatricConnect.db"
dotnet run --project tools/MatricConnect.CatalogueImport -- \
  --database "$catalogue_db" --references docs/catalogue-batches/north-west-references.json --refresh
```

Append `--apply` to import this batch after checking the preview. Run the preview again and expect **89 unchanged**.
