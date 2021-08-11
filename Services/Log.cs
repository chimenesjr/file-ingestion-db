using System;

namespace file_ingest_db.Services
{
    public class LogService : ILogService
    {
        public void Log(string txt)
        {
            var index = "file-ingest-db|| ";
            Console.WriteLine($"{index}{txt}");
        }
    }
}
