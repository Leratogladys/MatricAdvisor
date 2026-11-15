# National institution directory

Source review: 15 November 2026. The directory lists South Africa’s 26 public universities (including six universities of technology) and 50 public TVET colleges. Private institutions are outside this directory.

Each institution appears once: universities are grouped by their main location and TVET colleges by their Department of Higher Education and Training (DHET) province. Use the institution’s website to check individual campuses and the programmes they offer. Provincial search is separate from the home province on a student profile.

The nine programme records in the verified catalogue remain associated with the University of the Free State, University of Johannesburg, University of Pretoria and University of the Witwatersrand. Other directory entries link to the institution’s website for programme details. A directory entry does not imply that Matric Connect has verified the institution’s programmes or that applications are open.

## Sources

- [DHET 2026/27 Annual Performance Plan](https://www.dhet.gov.za/Strategic%20Plans/Annual%20Perfomance%20Plans/DEPARTMENT%20OF%20HIGHER%20EDUCATION%20AND%20TRAINING%20APP%20FOR%202026-27.pdf) (national institution totals)
- [Universities South Africa](https://usaf.ac.za/prospective-students/) (public university names and sites)
- [ETDP SETA 2026/27 Sector Skills Plan](https://www.etdpseta.org.za/etd/sites/default/files/2025-09/2026_2027%20ETDP%20SETA%20Sector%20Skills%20Plan.pdf) (university types and provincial counts)
- [DHET public TVET college directory](https://www.dhet.gov.za/SitePages/TVETlinks.aspx) (TVET names and province grouping)
- [North West Provincial Treasury career guide](https://treasury.nwpg.gov.za/wp-content/uploads/2026/01/Career-Guidance-Booklet-Print-ready_compressed.pdf) and [SAQA NQF booklet](https://www.saqa.org.za/wp-content/uploads/2024/05/NQF-Booklet-Digital.pdf) (supplemental college website references)

## Maintenance

Directory data lives in `MatricConnect.Infrastructure/Data/NationalDirectorySeed.cs`. The database migration adds a university type flag and a separate TVET college table. Startup adds missing institutions after the initial catalogue is seeded, so existing university and programme IDs and saved records are preserved. For later name or website changes, update the source data and explicitly update affected existing records in a migration.
