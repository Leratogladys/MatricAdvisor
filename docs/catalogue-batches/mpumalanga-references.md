# Mpumalanga programme review

The [province batch](mpumalanga-references.json) contains 179 listings across the four public institutions in the directory. All have programme pages and can be saved. The 86 comparisons check the individual minimums published for a named entry route. The programme page describes other requirements and the institution makes the admission decision.

| Institution | Listings | Comparisons | Sources and limits |
| --- | ---: | ---: | --- |
| University of Mpumalanga | 96 | 53 | All three [faculties](https://www.ump.ac.za/Study-with-us/Faculties-and-Schools.aspx), the [academic almanac](https://www.ump.ac.za/getattachment/Study-with-us/Almanac/UMP-Almanac-2026.pdf.aspx?lang=en-US), the [undergraduate guide](https://www.ump.ac.za/getattachment/Study-with-us/Application-Process/Online-Applications/Undergraduate-Programmes.pdf.aspx?lang=en-US) and programme pages. Includes undergraduate, advanced and postgraduate qualifications. A university-wide announcement does not establish an intake for each programme. |
| Ehlanzeni TVET College | 19 | 18 | [Course directory](https://www.ehlanzenicollege.co.za/courses), campus programme listings and [application guidance](https://www.ehlanzenicollege.co.za/apply). Applications for a year do not mean every campus and route has places. |
| Gert Sibande TVET College | 36 | 0 | [College programme listings](https://gscollege.edu.za/) and its [2025 prospectus](https://gscollege.edu.za/wp-content/uploads/2025/02/2025-Prospectors.pdf). Where only older published rules were available, the page identifies them as historical; it does not assess current entry. |
| Nkangala TVET College | 28 | 15 | [Programme pages](https://www.ntc.edu.za/), [occupational offerings](https://www.ntc.edu.za/occupational-programmes.html) and [pre-vocational learning](https://www.ntc.edu.za/pre-vocational-learning.html). Pages marked discontinued or without a route-specific minimum do not offer automatic comparison. |

UMP’s almanac headline count differs from the named entries in its programme lists. The batch counts those named entries and additional qualifications on official programme pages, after matching existing aliases. It does not treat these as separate programme intakes. Where the published requirements and intake year are established at different levels, requirements are shown with an unconfirmed intake. Where official sources disagree on minimums, the listing explains the conflict and leaves comparison unavailable; examples include Agricultural Extension, Nature Conservation and the general BSc. The Plant Production diploma has separate NSC and NC(V) routes and separates Mathematics from Mathematical Literacy.

After migrating the intended database, preview using Ubuntu/WSL:

```bash
catalogue_db="${XDG_DATA_HOME:-$HOME/.local/share}/MatricConnect/MatricConnect.db"
dotnet run --project tools/MatricConnect.CatalogueImport -- \
  --database "$catalogue_db" --references docs/catalogue-batches/mpumalanga-references.json --refresh
```

The earlier 77-listing batch and agriculture update produced **102 to add; 76 to update; 1 unchanged** on the test database. For a database with no Mpumalanga references, expect **179 to add**. Append `--apply` to write after previewing and repeat the preview to confirm **179 unchanged**. Refresh preserves listing IDs and saved programmes; it does not remove entries.
