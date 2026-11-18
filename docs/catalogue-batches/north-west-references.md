# North West programme review

The [reviewed batch](north-west-references.json) has 283 listings and 236 comparisons. The 2027 university expansion adds 194 listings and updates four previously imported Law listings in place. It does not cover all North West qualifications.

| Institution | Listings | Comparisons | Evidence and gaps |
| --- | ---: | ---: | --- |
| North-West University | 218 | 172 | All eight [undergraduate faculties](https://studies.nwu.ac.za/undergraduate-studies/fields-study) are represented, including named specialisations with their own campus or minimums. The [2027 prospectus](https://studies.nwu.ac.za/sites/studies.nwu.ac.za/files/files/undergrad/2027-Grade-12-prospectus.pdf) describes NWU’s APS and gives a second set of requirements to cross-check. Postgraduate and continuation pathways are outside this batch. |
| ORBIT TVET College | 21 | 20 | Published [2026 occupational schedule](https://www.orbitcollege.co.za/PQM%20OCCUPATIONAL%20QUALIFICATIONS%20OFFERED%20FROM%202026%20%281%29.pdf), [NC(V) prospectus](https://www.orbitcollege.co.za/2026%20NCV%20Programmes.pdf) and [admission policy](https://www.orbitcollege.co.za/Policy%20Admission%202025.pdf). A placement score example does not set a minimum. The pre-vocational listing describes a referral route; no standalone minimum was published there. Report 191 availability remains open for review. |
| Vuselela TVET College | 35 | 35 | All 35 entries in the [public programme index](https://vuselelacollege.co.za/programmes), including routes requiring a school subject or an existing qualification. Entry requirements do not verify application dates. |
| Taletso TVET College | 9 | 9 | Named NC(V) programmes in the [official college app’s 9 September 2026 admission notice](https://taletsotvetcollege.app/). The poster lists a 1–30 September 2026 window for the 2027 intake and gives some school criteria by subject area. The app checks the documented criteria only. Taletso’s other qualifications and occupational notices still need review. |

The online [Law table](https://studies.nwu.ac.za/undergraduate-studies/law-2027) and the 2027 prospectus disagree on APS or subject minimums for the BA in Law, BCom in Law, LLB and Extended LLB. The [Faculty of Law’s LLB page](https://law.nwu.ac.za/undergraduate-studies/llb-and-llb-extended) does not resolve the conflict for the stated intake. The four listings show the conflicting thresholds, and comparison is unavailable pending confirmation. The same approach is used for other unresolved conflicts, including Nursing and Forensic Accountancy. Comparisons for complex specialisations are available only where the published checks can be represented accurately. Programme-specific application dates are not inferred from the guide’s year.

In WSL, use a checkout containing both the updated batch and the comparison migration from the earlier release:

```bash
catalogue_db="${XDG_DATA_HOME:-$HOME/.local/share}/MatricConnect/MatricConnect.db"
dotnet run --project tools/MatricConnect.CatalogueImport -- \
  --database "$catalogue_db" --references docs/catalogue-batches/north-west-references.json --refresh
```

With the first North West batch already imported, expect **194 to add; 4 to update; 85 unchanged**. The preview is read-only. Append `--apply` to import once verified, then preview again and expect **283 unchanged**. Refresh preserves listing IDs and existing shortlists. Use the updated batch for future North West imports.
