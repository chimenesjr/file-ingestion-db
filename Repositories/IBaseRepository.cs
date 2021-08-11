
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace file_ingest_db.Repositories
{
    public interface IBaseRepository<TEntity> where TEntity : class
    {
        void Insert(TEntity entity);
        TEntity GetByID(Guid id);
        void Delete(TEntity entityToDelete);
        void Delete(Guid id);
        void Update(TEntity entityToUpdate);
        IEnumerable<TEntity> Get(
            Expression<Func<TEntity, bool>> filter = null,
            Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null,
            string includeProperties = "");
    }
}
