using ApiBase.Domain.Entities;
using ApiBase.Domain.Interfaces;

namespace ApiBase.Application.Base
{
    public class ApplicationBase<TEntity, TRepository> where TEntity : EntityGuid, new() where TRepository : IRepositoryBase<TEntity>
    {
        protected IUnitOfWork unitOfWork { get; set; }
        protected TRepository Repository { get; set; }

        protected ApplicationBase(IUnitOfWork _unitOfWork, TRepository repository)
        {
            unitOfWork = _unitOfWork;
            Repository = repository;
        }

        protected void Commit()
        {
            unitOfWork.Commit();
        }

        protected void RollbackChanges()
        {
            unitOfWork.RollbackChanges();
        }
    }
}
