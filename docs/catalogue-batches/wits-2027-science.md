# Wits Faculty of Science: first reviewed batch

Reviewed on **16 November 2026** for the **2027 undergraduate intake** and the South African NSC route. The [application page](https://www.wits.ac.za/undergraduate/apply-to-wits/) confirms the intake is closed. The [2027 timeline](https://www.wits.ac.za/undergraduate/apply-to-wits/timelines/) lists 30 September 2026 as the Science application deadline and 31 October 2026 as the Science National Benchmark Test deadline. A later intake was not verified for this batch.

The [batch file](wits-2027-science.json) adds 11 distinct application codes as requirements-only listings. The two existing Wits programmes, SBA13 and SBA21, are outside this batch. Wits uses its own [APS guide](https://www.wits.ac.za/undergraduate/entry-requirements/); the listed scores do not refer to the generic profile APS.

| Code | Official Course Finder source | Wits APS | Mathematics | English | Physical Sciences |
| --- | --- | ---: | ---: | ---: | ---: |
| SBA00 | [BSc General](https://www.wits.ac.za/course-finder/undergraduate/science/bsc/) | 42 | 60% | 60% | — |
| SBA03 | [BSc Actuarial Science](https://www.wits.ac.za/course-finder/undergraduate/science/actuarial-science/) | 44 | 80% | 80% | 80% |
| SBA08 | [BSc Mathematical Sciences](https://www.wits.ac.za/course-finder/undergraduate/science/mathematical-sciences/) | 44 | 80% | 80% | 80% |
| SBA09 | [BSc Geological Sciences](https://www.wits.ac.za/course-finder/undergraduate/science/geological-sciences/) | 42 | 70% | 60% | 60% |
| SBA10 | [BSc Geographical and Archaeological Sciences](https://www.wits.ac.za/course-finder/undergraduate/science/geographical-and-archaeological-sciences/) | 42 | 60% | 60% | — |
| SBA15 | [BSc Astronomy and Astrophysics](https://www.wits.ac.za/course-finder/undergraduate/science/astronomy-and-astrophysics/) | 43 | 70% | 60% | 70% |
| SBA20 | [BSc Geospatial Sciences](https://www.wits.ac.za/course-finder/undergraduate/science/geospatial-sciences/) | 42 | 60% | 60% | — |
| SBA22 | [BSc Environmental Studies](https://www.wits.ac.za/course-finder/undergraduate/science/environmental-studies/) | 42 | 60% | 60% | — |
| SBA23 | [BSc Physics and Philosophy](https://www.wits.ac.za/course-finder/undergraduate/science/physics-and-philosophy/) | 43 | 70% | 70% | 70% |
| SBA24 | [BSc Physics and Finance](https://www.wits.ac.za/course-finder/undergraduate/science/physics-and-finance/) | 43 | 70% | 70% | 70% |
| SBA25 | [BSc Physics, Ecology and Sustainability](https://www.wits.ac.za/course-finder/undergraduate/science/physics-ecology-and-sustainability/) | 43 | 70% | 70% | 70% |

A dash means the programme page does not list a separate Physical Sciences minimum. English refers to Home Language or First Additional Language. The percentages represent NSC levels using the official Wits NSC scale. The published minima and any documented waiting-list criteria are separate; Wits decides admission.

## Held out for further review

- **SBA11 and SBA12:** Wits shows shared application codes across multiple course pages. For example, [Applied Bioinformatics](https://www.wits.ac.za/course-finder/undergraduate/science/applied-bioinformatics/) and [Applied Ecology and Global Change](https://www.wits.ac.za/course-finder/undergraduate/science/applied-ecology-and-global-change/) show SBA11. [Chemistry](https://www.wits.ac.za/course-finder/undergraduate/science/chemistry/) and [Physics](https://www.wits.ac.za/course-finder/undergraduate/science/physics/) show SBA12. A future batch should represent the application route once and link its subject streams without duplicate codes.
- **SBA04:** [Chemistry with Chemical Engineering](https://www.wits.ac.za/course-finder/undergraduate/science/chemistry-with-chemical-engineering/) gives two different wait-list ranges. Keep this listing out of the batch until the range is clarified.
- The [Archaeology](https://www.wits.ac.za/course-finder/undergraduate/science/archaeology/), [Mathematics](https://www.wits.ac.za/course-finder/undergraduate/science/mathematics/) and [Geography](https://www.wits.ac.za/course-finder/undergraduate/science/geography/) pages describe subjects or majors and direct applicants to degree-level requirements. Do not give them a new application code.

## Preview

From the repository root in Ubuntu/WSL:

```bash
db="${XDG_DATA_HOME:-$HOME/.local/share}/MatricConnect/MatricConnect.db"
dotnet run --project tools/MatricConnect.CatalogueImport -- \
  --database "$db" --batch docs/catalogue-batches/wits-2027-science.json
```

On the currently seeded catalogue, expect **11 to add; 0 unchanged**. This command opens the database read-only. Applying the batch on a test copy and replaying it should produce **0 to add; 11 unchanged**.
