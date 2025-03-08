
using EShopping.Core.Infrastructure.Interface;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore.Infrastructure;

using System.ComponentModel.DataAnnotations.Schema;

namespace EShopping.Core.Infrastructure.Implementation
{
    public abstract class BaseApplicationDbContext<T> : DbContext, IDataContext where T : DbContext
    {
        protected readonly string _connectionString;

        public BaseApplicationDbContext()
        {
        }
        public BaseApplicationDbContext(string connectionString)
        {
            _connectionString = connectionString;
        }

        public BaseApplicationDbContext(DbContextOptions<T> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            var entityMethod = typeof(ModelBuilder).GetMethod("Entity", new Type[] { });

            var entityTypes = typeof(T).Assembly
              .GetTypes()
              .Where(x => x.GetCustomAttributes(typeof(TableAttribute), inherit: true)
              .Any());

            foreach (var type in entityTypes)
            {
                entityMethod.MakeGenericMethod(type).Invoke(modelBuilder, new object[] { });
            }

            foreach (var relationship in modelBuilder.Model.GetEntityTypes().SelectMany(e => e.GetForeignKeys()))
            {
                relationship.DeleteBehavior = DeleteBehavior.Restrict;
            }

            base.OnModelCreating(modelBuilder);
        }

        public new DbSet<TEntity> Set<TEntity>() where TEntity : class
        {
            return base.Set<TEntity>();
        }

        public new EntityEntry<TEntity> Entry<TEntity>(TEntity entity) where TEntity : class
        {
            return base.Entry(entity);
        }

        public new EntityEntry Entry(object entity)
        {
            return base.Entry(entity);
        }

        public IEnumerable<EntityEntry> GetChangeTrackerEntries()
        {
            return ChangeTracker.Entries();
        }

        public override int SaveChanges()
        {
            ApplyCommonTask();
            return base.SaveChanges();
        }

        private void ApplyCommonTask()
        {
            List<EntityEntry> copyChangeList = ChangeTracker.Entries().ToList();
            foreach (var entry in copyChangeList)
            {
                if (entry.State == EntityState.Unchanged)
                {
                    continue;
                }
                var entity = entry.Entity as IEntity;
                if (entity == null)
                {
                    continue;
                }
                if (entry.State == EntityState.Deleted)
                {
                    //entity.OnDelete();
                }
                if (entry.State == EntityState.Added)
                {
                    //if (((IEntity)entry.Entity).AutoIdGeneration && entity.Id == 0)
                    //{
                    //    entity.Id = NumberUtilities.GetUniqueNumber();
                    //}
                    entity.CreatedAt = DateTime.UtcNow;
                    entity.UpdatedAt = DateTime.UtcNow;
                    //entity.OnCreate();
                }
                if (entry.State == EntityState.Modified)
                {
                    entity.UpdatedAt = DateTime.UtcNow;
                    //entity.OnUpdate();
                }
            }
        }

        public async Task<int> SaveChangesAsync()
        {
            ApplyCommonTask();
            return await base.SaveChangesAsync();
        }

        public override DatabaseFacade Database
        {
            get
            {
                return base.Database;
            }
        }

    }
}
