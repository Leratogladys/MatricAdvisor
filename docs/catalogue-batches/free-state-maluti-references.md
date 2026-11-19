# Free State: Maluti TVET College

Twelve school-entry NC(V) listings from the [Maluti course catalogue](https://www.malutitvet.co.za/programmes). Campus and intake are unconfirmed. This batch does not include NATED or occupational routes.

From the existing project directory in WSL, preview:

```bash
repo_dir=/mnt/c/Users/lerat/Desktop/Projects/MatricConnect/MatricConnect
catalogue_db="${XDG_DATA_HOME:-$HOME/.local/share}/MatricConnect/MatricConnect.db"
batch="$repo_dir/docs/catalogue-batches/free-state-maluti-references.json"
dotnet run --project tools/MatricConnect.CatalogueImport -- --database "$catalogue_db" --references "$batch"
```

Expect twelve additions. Stop the web app and back up the database, then repeat with `--apply`. Preview again without it to confirm twelve unchanged. No migration is required.
