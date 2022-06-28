using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Repos
{
    public interface IRepository<T> where T : class
    {
        Task<T> Create(T item);
        IQueryable<T> Read();
        Task<bool> Update(T item);
        Task<bool> Delete(int id);
    }
}
