using System;
using System.Collections.Generic;
using System.Linq;
using file_ingest_db.Model;
using file_ingest_db.Repositories;
using file_ingest_db.Services;
using System.IO;
using System.Security.Cryptography.X509Certificates;
using file_ingest_db.Extensions;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;

namespace file_ingest_db
{
    public class Runner
    {
        private readonly ILogService log;
        private readonly ITigerService tigerService;
        private readonly ISnakeService snakeService;
        private readonly IConfiguration _config;

        private readonly string path;

        public Runner(ILogService log, ITigerService tigerService, ISnakeService snakeService, IConfiguration iConfig)
        {
            this.log = log;
            this.tigerService = tigerService;
            this.snakeService = snakeService;
            _config = iConfig;
            path = this._config.GetValue<string>("LocalPath:Path");
        }

        public void DoAction()
        {
            try 
            {
                this.log.Log("Loading files....");
                var files = LoadFiles();

                var tigers = GetTigersFromList(files);
                var snakes = GetSnakesFromList(files);
                
                this.log.Log("Processing files....");
                this.tigerService.Process(tigers);
                this.snakeService.Process(snakes);

                this.log.Log("Deleting files....");
                files.ForEach(x=> DeleteFile(x.Item1));

                this.log.Log("Finished with the files....");
            }
            catch (Exception e)
            {
                this.log.Log(e.GetExceptionMessages());
            }
        }

        private List<SnakeFile> GetSnakesFromList(List<Tuple<string, string>> files)
        {
            var snakeFIles = files.Where(item => item.Item1.StartsWith("S"));

            var snakes = new List<SnakeFile>();

            snakeFIles.ToList().ForEach(x =>
            {
                snakes.Add(getFromJson(x.Item2));
            });

            SnakeFile getFromJson(string txt)
            {
                return JsonConvert.DeserializeObject<SnakeFile>(txt);
            }

            return snakes;
        }

        private List<TigerFile> GetTigersFromList(List<Tuple<string, string>> files)
        {
            var tigerFiles = files.Where(item => item.Item1.StartsWith("T"));

            var tigers = new List<TigerFile>();

            tigerFiles.ToList().ForEach(x =>
            {
                tigers.Add(x.Item2.FromJson<TigerFile>());
            });

            return tigers;
        }

        private List<Tuple<string, string>> LoadFiles()
        {
            List<Tuple<string, string>> list = new List<Tuple<string, string>>();

            if (Directory.Exists(path))
            {
                string [] fileEntries = Directory.GetFiles(path);

                fileEntries.ToList().ForEach(item =>
                {
                    var file = File.ReadAllText(item);
                    list.Add(
                        new Tuple<string, string>(Path.GetFileName(item), file)
                    );
                });
            }

            return list;
        }

        private void DeleteFile(string file){
            try
            {
                File.Delete($"{path}/{file}");
            }
            catch (System.Exception ex)
            {
                this.log.Log(ex.GetExceptionMessages());
            }
        }
    }
}