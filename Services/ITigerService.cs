
using System.Collections.Generic;
using file_ingest_db.Model;

namespace file_ingest_db.Services
{
    public interface ITigerService
    {
        void Process(List<TigerFile> tigers);
    }
}
