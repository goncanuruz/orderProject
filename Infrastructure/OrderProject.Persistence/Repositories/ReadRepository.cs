using OrderProject.Application.Repositories;
using OrderProject.Domain.Entities.Common;
using OrderProject.Persistence.Contexts;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace OrderProject.Persistence.Repositories
{
    public class ReadRepository<T> : IReadRepository<T> where T : BaseEntity
    {
        private readonly OrderProjectDbContext _context;

        public ReadRepository(OrderProjectDbContext context)
        {
            _context = context;
        }

        public DbSet<T> Table => _context.Set<T>();

        public IQueryable<T> GetAll(bool ClientFilter = false)
        {
            var query = _context.Set<T>().AsNoTracking().AsQueryable();

            return query;
        }

        public T? GetById(Guid id)
        {
            var entity = _context.Set<T>().FirstOrDefault(x => x.Id == id);

            return entity;
        }

        public T? GetSingleAsync(Expression<Func<T, bool>> method)
        {
            var entity = _context.Set<T>().FirstOrDefault(method);
            return entity;
        }

        public IQueryable<T> GetWhere(Expression<Func<T, bool>> method)
        {
            var query = _context.Set<T>().AsNoTracking().AsQueryable().Where(method);
            return query;
        }
    }
}
