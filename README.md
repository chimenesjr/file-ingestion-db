# File-ingest-db

1. file-ingest-front

2. file-ingest-back

3. **file-ingest-db**

Load the files from a specific folder that automatically download files from the Cloud Store.

These files are added to a pile to be ingested to a database in other container.



## Migrations Commands

`dotnet ef migrations add Tiger`

`dotnet ef database update`

`dotnet ef database update 0`

`dotnet ef migrations remove`


# SQL Commands

`sudo docker exec -it sql-server-db "bash"`

`/opt/mssql-tools/bin/sqlcmd -S localhost -U SA -P "hard!@1029"`