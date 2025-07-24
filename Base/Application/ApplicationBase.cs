using Base.DTOs;
using Base.Entities;
using Microsoft.EntityFrameworkCore;
using Base.Repository;
using Base.Infrastructure.Exceptions;
using Base.Infrastructure.Extensions;

namespace Base.Application
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

        public async Task<RetConView<T>> GetAllAsync(QueryParams queryParams)
        {
            var query = _repository.Query();

            query = QueryHelper.ApplyFilters(query, queryParams.Filters ?? new());
            query = QueryHelper.ApplySorting(query, queryParams.OrderBy, queryParams.Direction);

            var total = await query.CountAsync();

            var content = await query
            .Skip((queryParams.Page - 1) * queryParams.Limit)
            .Take(queryParams.Limit)
            .ToListAsync();

            return new RetConView<T>
            {
                Total = total,
                Content = content
            };
        }

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
