using ApiBase.Core.Domain.Entities;
using ApiBase.Core.Domain.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.Reflection;

namespace ApiBase.Core.Infra.UnitOfWork
{
    public class UnitOfWork : IUnitOfWork, IDisposable
    {
        private readonly DbContext _dbContext;
        public UnitOfWork(DbContext dbContext)
        {
            _dbContext = dbContext ?? throw new ArgumentNullException(nameof(dbContext));
        }

        public void Dispose()
        {
            _dbContext.Dispose();
        }

        public void Commit()
        {
            try
            {
                ValidateEntities();
                _dbContext.SaveChanges();
            }
            catch (DbUpdateException ex)
            {
                RollbackChanges();
                throw new Exception("Database update failed: " + ex.GetBaseException().Message, ex);
            }
            catch (ValidationException ex)
            {
                RollbackChanges();
                throw new Exception("Validation failed: " + ex.Message, ex);
            }
        }

        private void ValidateEntities()
        {
            var entitiesToValidate = _dbContext.ChangeTracker.Entries()
                .Where(e => e.State == EntityState.Added || e.State == EntityState.Modified)
                .Select(e => e.Entity);

            foreach (var entity in entitiesToValidate)
            {
                var validationContext = new ValidationContext(entity);
                Validator.ValidateObject(entity, validationContext, validateAllProperties: true);
            }
        }

        public void RollbackChanges()
        {
            var changedEntries = _dbContext.ChangeTracker.Entries().Where(e => e.State != EntityState.Unchanged).ToList();

            foreach (var entry in changedEntries)
            {
                switch (entry.State)
                {
                    case EntityState.Modified:
                        entry.CurrentValues.SetValues(entry.OriginalValues);
                        entry.State = EntityState.Unchanged;
                        break;
                    case EntityState.Added:
                        entry.State = EntityState.Detached;
                        break;
                    case EntityState.Deleted:
                        entry.State = EntityState.Unchanged;
                        break;
                }
            }
        }
    }
}
