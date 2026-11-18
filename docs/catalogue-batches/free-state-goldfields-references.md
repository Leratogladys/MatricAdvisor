# Free State: Goldfields TVET College

Ten NC(V) listings, five each from the official [Tosa](https://goldfieldstvet.edu.za/courses-page/tosa-campus-ncv/) and [Welkom](https://goldfieldstvet.edu.za/courses-page/welkom-campus-ncv/) campus lists. The [college FAQ](https://goldfieldstvet.edu.za/study/apply/frequently-asked-questions/) identifies Grade 9 as a general entry point and NC(V) as spanning NQF Levels 2–4. Each record starts at Level 2. The campus pages are undated, so no intake year is asserted. Detailed admission alternatives and placement are not encoded as automatic comparisons. Report 191 and occupational routes are outside this batch.

From the existing project directory in WSL, preview:

```bash
repo_dir=/mnt/c/Users/lerat/Desktop/Projects/MatricConnect/MatricConnect
catalogue_db="${XDG_DATA_HOME:-$HOME/.local/share}/MatricConnect/MatricConnect.db"
batch="$repo_dir/docs/catalogue-batches/free-state-goldfields-references.json"
dotnet run --project tools/MatricConnect.CatalogueImport -- --database "$catalogue_db" --references "$batch"
```

Expect ten additions. Stop the web app and back up the database, then repeat with `--apply`. Preview again without it to confirm ten unchanged. No migration is required.
