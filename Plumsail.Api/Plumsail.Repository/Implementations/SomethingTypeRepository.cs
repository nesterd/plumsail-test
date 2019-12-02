using Plumsail.Domain;
using Plumsail.Repository.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Plumsail.Repository.Implementations
{
    public class SomethingTypeRepository : BaseRepository<SomethingType>, ISomethingTypeRepository
    {
        public SomethingTypeRepository(AppDbContext db) 
            : base(db)
        {
        }
    }
}
