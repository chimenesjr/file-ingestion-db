#!/bin/bash

gcsfuse --foreground --debug_gcs --debug_fuse --key-file=/app/key/book-255910-1e410cf3767f.json chimenesjr-ingest-files /app/saved-files

dotnet file-ingest-db.dll
