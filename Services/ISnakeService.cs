
using System.Collections.Generic;
using file_ingest_db.Model;

namespace file_ingest_db.Services
{
    public interface ISnakeService
    {
        void Process(List<SnakeFile> snakes);
    }
}
