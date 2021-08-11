using file_ingest_db.Annotations;
using file_ingest_db.Enum;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace file_ingest_db.Model
{
    [NotMapped]
    public class BaseModel : IModel
    {
        [Required]
        public Guid Id { get; set; }

        [MaxLength(100)]
        public string ContextName { get; set; }

        [DataType(DataType.DateTime)]
        [DateMinValue]
        public DateTime Created { get; set; }

        public int time_to_hold { get; set; }

        [Required]
        internal int type { get; set;}

        public DateTime Finish { get; set; }

        FileTypeEnum IModel.GetType()
        {
            return (FileTypeEnum)this.type;
        }
    }
}