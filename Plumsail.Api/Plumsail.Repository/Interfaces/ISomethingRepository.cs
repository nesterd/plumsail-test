using Plumsail.Domain;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Plumsail.Repository.Interfaces
{
    public interface ISomethingRepository : IBaseRepository<Something>
    { 
        Task<Something> Get(int id);
        Task<int> Add(Something model);
        Task Edit(Something model);
        Task Delete(int id);
        Task<bool> Contains(int id);
    }
}
