using file_ingest_db.Model;

namespace file_ingest_db.Repositories
{
    public class SnakeRepository : BaseRepository<SnakeFile>, ISnakeRepository
    {
        public dbContext _context;

        public SnakeRepository(dbContext context) : base(context)
        {
            this._context = context;
        }
    }
}
