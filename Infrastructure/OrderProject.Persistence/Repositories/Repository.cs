using OrderProject.Application.Repositories;
using OrderProject.Domain.Entities.Common;
using OrderProject.Persistence.Contexts;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrderProject.Persistence.Repositories
{
    public class Repository<T> : IRepository<T> where T : BaseEntity
    {
        private readonly OrderProjectDbContext _context;

        public Repository(OrderProjectDbContext context)
        {
            _context = context;
        }
        public DbSet<T> Table => _context.Set<T>();
    }
}
