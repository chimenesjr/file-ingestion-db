using System;
using System.Collections.Generic;
using System.Linq;
using file_ingest_db.Extensions;
using file_ingest_db.Model;
using file_ingest_db.Repositories;

namespace file_ingest_db.Services
{
    public class TigerService : ITigerService
    {
        private readonly ITigerRepository repository;
        private readonly ILogService log;

        public TigerService(ITigerRepository repository, ILogService log)
        {
            this.repository = repository;
            this.log = log;
        }
        void ITigerService.Process(List<TigerFile> tigers)
        {
            tigers.ForEach(x => {
                try
                {
                    this.repository.Insert(x);
                    this.log.Log(
                        $"INGESTED | TIGER | {x.Id}"
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
