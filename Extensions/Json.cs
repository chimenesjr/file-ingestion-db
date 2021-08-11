using System;
using System.Collections.Generic;
using System.Text;
using file_ingest_db.Model;
using Newtonsoft.Json;

namespace file_ingest_db.Extensions
{
    public static class Json
    {
        public static T FromJson<T>(this string obj)
        {
            return JsonConvert.DeserializeObject<T>(obj);
        }
    }
}
