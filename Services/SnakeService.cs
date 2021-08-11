using System;
using System.Collections.Generic;
using System.Linq;
using file_ingest_db.Extensions;
using file_ingest_db.Model;
using file_ingest_db.Repositories;

namespace file_ingest_db.Services
{
    public class SnakeService : ISnakeService
    {
        private readonly ISnakeRepository repository;
        private readonly ILogService log;

        public SnakeService(ISnakeRepository repository, ILogService log)
        {
            this.repository = repository;
            this.log = log;
        }
        void ISnakeService.Process(List<SnakeFile> snakes)
        {
            snakes.ForEach(x => {
                try
                {
                    this.repository.Insert(x);
                    this.log.Log(
                        $"INGESTED | SNAKE | {x.Id}"
                    );
                }
                catch (Exception e)
                {
                    this.log.Log(e.GetExceptionMessages());
                }
            });
        }
    }
}
