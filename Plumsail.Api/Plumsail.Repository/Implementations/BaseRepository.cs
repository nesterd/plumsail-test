using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Plumsail.Repository.Implementations
{
    public abstract class BaseRepository<T> where T : class
    {
        protected AppDbContext Db;

        public BaseRepository(AppDbContext db)
        {
            Db = db;
        }

        public async Task<List<T>> GetAll()
        {
            var query = GetAllIncludedQuery();

            return await query.ToListAsync();
        }

        protected virtual IQueryable<T> GetAllIncludedQuery() =>
            Db.Set<T>().AsQueryable();
            
    }
}
