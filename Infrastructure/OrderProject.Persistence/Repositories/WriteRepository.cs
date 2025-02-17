using OrderProject.Application.Repositories;
using OrderProject.Domain.Entities.Common;
using OrderProject.Persistence.Contexts;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace OrderProject.Persistence.Repositories
{
    public class WriteRepository<T> : IWriteRepository<T> where T : BaseEntity
    {
        private readonly OrderProjectDbContext _context;

        public WriteRepository(OrderProjectDbContext context)
        {
            _context = context;
        }

        public DbSet<T> Table => _context.Set<T>();

        public T Create(T entity, bool SaveChanges = true)
        {
            _context.Set<T>().Add(entity);

            if (SaveChanges) _context.SaveChanges();

            return entity;
        }
        public void Delete(T entity)
        {
            _context.Set<T>().Remove(entity);
            _context.SaveChanges();
        }

        public T Update(T entity)
        {
            _context.Set<T>().Update(entity);
            _context.SaveChanges();

            return entity;
        }
        public virtual void SaveChanges()
        {
            _context.SaveChanges();
        }
    }
}
