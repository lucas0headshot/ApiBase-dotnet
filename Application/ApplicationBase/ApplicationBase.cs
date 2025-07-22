using Domain.Entities;
using Domain.Repository;
using Infrastructure.Exceptions;
using Microsoft.EntityFrameworkCore;

namespace Application.ApplicationBase
{
    public class ApplicationBase<T> : IApplicationBase<T> where T : EntityGuid
    {
        private readonly IRepositoryBase<T> _repository;
        private readonly DbContext _dbContext;

        public ApplicationBase(IRepositoryBase<T> repository, DbContext dbContext)
        {
            _repository = repository;
            _dbContext = dbContext;
        }

        public async Task<T?> GetAsync(Guid id)
        {
            var entity = await _repository.GetByIdAsync(id);
            if (entity == null)
                throw new EntityNotFoundException(typeof(T).Name, id);
            return entity;
        }

        public Task<IEnumerable<T>> GetAllAsync() =>
            _repository.ListAllAsync();

        public async Task<T> CreateAsync(T entity)
        {
            await _repository.AddAsync(entity);
            await _dbContext.SaveChangesAsync();
            return entity;
        }

        public async Task UpdateAsync(T entity)
        {
            _repository.Update(entity);
            await _dbContext.SaveChangesAsync();
        }

        public async Task DeleteAsync(Guid id)
        {
            var entity = await _repository.GetByIdAsync(id) ?? throw new EntityNotFoundException(typeof(T).Name, id);
            _repository.Delete(entity);
            
            await _dbContext.SaveChangesAsync();
        }
    }
}
