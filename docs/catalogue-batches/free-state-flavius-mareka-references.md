# Free State: Flavius Mareka TVET College

This starter batch records six school-entry NC(V) routes, each listed at Level 2 in the college’s [2026 NC(V) course schedule](https://flaviusmareka.net/wp-content/uploads/2025/09/2026-NCV-COURSE-FEE-LIST-SUMMARY.pdf). The listed campuses are for Level 2. These are reference listings for the published 2026 course year. They do not confirm a 2027 intake or offer automatic comparison. Progression-only listings and the existing catalogue are outside this batch.

In WSL, from the working project folder, preview:

```bash
repo_dir=/mnt/c/Users/lerat/Desktop/Projects/MatricConnect/MatricConnect
catalogue_db="${XDG_DATA_HOME:-$HOME/.local/share}/MatricConnect/MatricConnect.db"
batch="$repo_dir/docs/catalogue-batches/free-state-flavius-mareka-references.json"
dotnet run --project tools/MatricConnect.CatalogueImport -- --database "$catalogue_db" --references "$batch"
```

Expect six additions. Back up the database before repeating with `--apply`, then preview without it again and expect six unchanged. No migration is required.
