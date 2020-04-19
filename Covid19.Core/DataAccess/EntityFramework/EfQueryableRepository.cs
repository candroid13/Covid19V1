using Covid19.Entities.Abstract;
using System.Data.Entity;
using System.Linq;

namespace Covid19.Core.DataAccess.EntityFramework
{
    public class EfQueryableRepository<T> : IQueryableRepository<T> where T : class, IEntity, new()
    {
        private DbContext _context;
        private IDbSet<T> _entities;

        public EfQueryableRepository(DbContext context)
        {
            _context = context;
        }

        public IQueryable<T> Table => this.Entities;

        protected virtual IDbSet<T> Entities
        {
            get { return _entities ?? (_entities = _context.Set<T>()); }
        }
    }
}
