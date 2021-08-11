using System.ComponentModel.DataAnnotations.Schema;

namespace file_ingest_db.Model
{
    [Table("Snake")]
    public class SnakeFile : BaseModel
    {
        public SnakeFile()
        {
            this.type = 0;
        }

        public int Length {get;set;}
    }
}