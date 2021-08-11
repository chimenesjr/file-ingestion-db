using file_ingest_db.Model;

namespace file_ingest_db.Repositories
{
    public class TigerRepository : BaseRepository<TigerFile>, ITigerRepository
    {
        public dbContext _context;

        public TigerRepository(dbContext context) : base(context)
        {
            this._context = context;
        }
    }
}
