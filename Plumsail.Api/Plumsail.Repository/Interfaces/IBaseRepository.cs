using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Plumsail.Repository.Interfaces
{
    public interface IBaseRepository<T>
    {
        Task<List<T>> GetAll();
    }
}
