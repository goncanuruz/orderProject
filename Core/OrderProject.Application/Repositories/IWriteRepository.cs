using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrderProject.Application.Repositories
{
    public interface IWriteRepository<T> : IRepository<T>
        where T : class
    {
        T Create(T entity, bool SaveChanges = true);
        T Update(T entity);
        void Delete(T entity);
        void SaveChanges();
    }
}
