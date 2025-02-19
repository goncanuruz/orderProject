using OrderProject.Domain.Entities.Common;
using OrderProject.Domain.Entities.Products;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore.Design;
using OrderProject.Domain.Entities.Common;
using OrderProject.Domain.Entities.Products;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security;
using System.Text;
using System.Threading.Tasks;
using OrderProject.Domain.Entities.Orders;

namespace OrderProject.Persistence.Contexts
{
    public class OrderProjectDbContext : DbContext
    {
        public OrderProjectDbContext(DbContextOptions options) : base(options)
        {
        }
        public DbSet<Product> Products { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<OrderDetail> OrderDetails { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // BaseEntity'den türeyen tüm entity'ler için filtre uygula
            foreach (var entityType in modelBuilder.Model.GetEntityTypes())
            {
                // Sadece BaseEntity türetilmiş sınıflar için işlem yap
                if (typeof(BaseEntity).IsAssignableFrom(entityType.ClrType))
                {
                    // Filtreyi ekle
                    var method = typeof(OrderProjectDbContext)
                        .GetMethod(nameof(BaseFilter), System.Reflection.BindingFlags.Instance | System.Reflection.BindingFlags.NonPublic)
                        ?.MakeGenericMethod(entityType.ClrType);

                    method?.Invoke(this, new object[] { modelBuilder });
                }
            }
        }
        public int SaveChangesForSeeder()
        {
            return base.SaveChanges();
        }
        public override int SaveChanges()
        {
            UpdateSoftDeleteStatuses();
            // CreateAudit().Wait();
            return base.SaveChanges();
        }

        private void UpdateSoftDeleteStatuses()
        {
            foreach (var entry in ChangeTracker.Entries())
            {
                if (entry.Entity is not IBaseEntity)
                {
                    return;
                }

                if (entry.Entity is IBaseEntity entity)
                {
                    Guid? userId = null;
                    switch (entry.State)
                    {
                        case EntityState.Added:
                            entity.IsDeleted = false;
                            entity.CreateTime = DateTime.Now;
                            entity.UpdateTime = DateTime.Now;
                            entity.CreatedUserId = userId;
                            entity.UpdatedUserId = userId;
                            break;
                        case EntityState.Deleted:
                            entry.State = EntityState.Modified;
                            entity.IsDeleted = true;
                            entity.UpdateTime = DateTime.Now;
                            entity.UpdatedUserId = userId;
                            break;
                        case EntityState.Modified:
                            // entity.IsDeleted     = false;
                            entity.UpdateTime = DateTime.Now;
                            entity.UpdatedUserId = userId;
                            break;
                    }
                }
            }
        }
        private void BaseFilter<T>(ModelBuilder builder) where T : BaseEntity
        {
            builder.Entity<T>().HasQueryFilter(p => !p.IsDeleted);
        }
    }

}
