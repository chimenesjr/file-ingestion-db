#!/bin/bash

gcsfuse chimenesjr-ingest-files /app/saved-files

# --foreground --debug_gcs --debug_fuse

dotnet file-ingest-db.dll
