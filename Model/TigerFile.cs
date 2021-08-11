using System.ComponentModel.DataAnnotations.Schema;

namespace file_ingest_db.Model
{
    [Table("Tiger")]
    public class TigerFile : BaseModel
    {
        public TigerFile()
        {
            this.type = 1;
        }

        public bool IsBengalTiger {get;set;}
    }
}
