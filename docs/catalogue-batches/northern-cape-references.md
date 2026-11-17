# Northern Cape programme review

Reviewed on **16 November 2026**. The [reviewed province batch](northern-cape-references.json) covers the three Northern Cape institutions in the project directory. The university listings use official faculty names; the college listings use qualification routes rather than treating the colleges as university faculties.

| Institution | Listings | Coverage and limits |
| --- | ---: | --- |
| Sol Plaatje University | 17 | Pre-degree-entry qualifications and first degrees published by its four faculties. The [2026 undergraduate prospectus](https://www.spu.ac.za/wp-content/uploads/2025/04/2026-Undergraduate-Prospectus.pdf) was cross-checked with faculty programme pages. The [2027 application window](https://www.spu.ac.za/index.php/how-to-apply/) ended on 31 October 2026. The batch does not carry the 2026 minima forward as 2027 rules or claim a later intake is open. |
| Northern Cape Rural TVET College | 28 | The college’s [published programme catalogue](https://www.ncrtvet.com/) lists campus and entry summaries for its foundation, NC(V), occupational and trade offerings. It marks the 2027 intake closed and lists NATED for continuing students only. Programme names shared by different routes remain separate. |
| Northern Cape Urban TVET College | 19 | The [college catalogue](https://www.ncutvet.edu.za/courses/) and campus pages cover NC(V), NATED and occupational listings. Entries only found in the [2024 annual report](https://www.ncutvet.edu.za/wp-content/uploads/2026/05/Annual-Report-2024-final_compressed.pdf) are labelled historical. The [admissions guidance](https://www.ncutvet.edu.za/admission-requirements/) is dated 2024, so it is not treated as a current intake determination. |

SPU faculty sources: [Economic and Management Sciences](https://www.spu.ac.za/index.php/spu-economic-and-management-sciences/), [Education](https://www.spu.ac.za/index.php/faculty-of-edu-programmes/), [Humanities](https://www.spu.ac.za/index.php/faculty-of-hum-programmes/), and [Natural and Applied Sciences](https://www.spu.ac.za/index.php/spu-nas-programmes/). Teaching streams and BSc specialisations are not counted as separate qualifications without a distinct reviewed route. Advanced diplomas and postgraduate study are outside the school-leaver scope.

Northern Cape Urban campus sources: [City](https://www.ncutvet.edu.za/city-campus/), [Phatsimang](https://www.ncutvet.edu.za/phatsimang/), and [Moremogolo](https://www.ncutvet.edu.za/moremogolo-campus/). The batch only names a campus where supported. Confirm new intake and entry-level availability with the college.

## Apply one reviewed province batch

After migrating the intended database, run from the repository root in Ubuntu/WSL:

```bash
catalogue_db="${XDG_DATA_HOME:-$HOME/.local/share}/MatricConnect/MatricConnect.db"
dotnet run --project tools/MatricConnect.CatalogueImport -- \
  --database "$catalogue_db" \
  --references docs/catalogue-batches/northern-cape-references.json
```

The preview should show **64 to add; 0 unchanged** on a database with no prior Northern Cape reference imports. Verify the institution breakdown, then append `--apply` on the final line. Repeating the preview after the import should show **0 to add; 64 unchanged**. This does not enable a numeric comparison for those references.

## National queue

The counts below come from Matric Connect’s [public institution directory](../institution-directory.md). Sort by total institutions, then province name when tied. A batch tracks what is sourced for each institution; it does not label an institution complete for every intake.

| Order | Province | Universities | TVET colleges | Total |
| ---: | --- | ---: | ---: | ---: |
| 1 | Northern Cape | 1 | 2 | 3 |
| 2 | Mpumalanga | 1 | 3 | 4 |
| 3 | North West | 1 | 3 | 4 |
| 4 | Free State | 2 | 4 | 6 |
| 5 | Limpopo | 2 | 7 | 9 |
| 6 | Western Cape | 4 | 6 | 10 |
| 7 | Eastern Cape | 4 | 8 | 12 |
| 8 | KwaZulu-Natal | 4 | 9 | 13 |
| 9 | Gauteng | 7 | 8 | 15 |
