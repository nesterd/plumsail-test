using Microsoft.EntityFrameworkCore;
using Plumsail.Domain;
using Plumsail.Repository.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Plumsail.Repository.Implementations
{
    public class SomethingRepository : BaseRepository<Something>, ISomethingRepository
    {

        public SomethingRepository(AppDbContext db)
            : base(db)
        {}

        public async Task<int> Add(Something model)
        {
            Db.Somethings.Add(model);
            await Db.SaveChangesAsync();

            return model.Id;
        }

        public async Task Delete(int id)
        {
            Something entity = await Db.Somethings.FindAsync(id);
            Db.Entry(entity).State = EntityState.Deleted;

            await Db.SaveChangesAsync();
        }

        public async Task Edit(Something model)
        {
            Db.Entry(model).State = EntityState.Modified;

            await Db.SaveChangesAsync();
        }

        public async Task<Something> Get(int id)
        {
            var query = GetAllIncludedQuery();

            return await query.FirstOrDefaultAsync(x => x.Id == id);
        }


        protected override IQueryable<Something> GetAllIncludedQuery() =>
             Db.Somethings
                .Include(x => x.Type);

        public async Task<bool> Contains(int id) =>
            await Db.Somethings.AnyAsync(x => x.Id == id);
    }
}
