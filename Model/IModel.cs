using file_ingest_db.Enum;

namespace file_ingest_db.Model
{
    public interface IModel
    {
        FileTypeEnum GetType();
    }
}