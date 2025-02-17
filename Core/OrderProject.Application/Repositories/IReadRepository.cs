using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace OrderProject.Application.Repositories
{
    public interface IReadRepository<T>:IRepository<T>
        where T:class
    {
        IQueryable<T> GetAll(bool ClientFilter = false);
        IQueryable<T> GetWhere(Expression<Func<T,bool>> method);
        T? GetSingleAsync(Expression<Func<T, bool>> method);
        T? GetById(Guid id);
    }
}
