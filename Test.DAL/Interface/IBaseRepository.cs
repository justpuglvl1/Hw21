using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Test.DAL.Interface
{
    public interface IBaseRepository<T>
    {
        Task<bool> Create(T entity);

        Task<T> Get(int id);
        Task<T> GetByName(string name);

        IQueryable<T> GetAll();

        Task<bool> Delete(T entity);

        Task<T> Update(T entity);
    }
}
