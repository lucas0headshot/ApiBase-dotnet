using ApiBase.Core.src.Domain.Entities;
using ApiBase.Core.src.Domain.Interfaces;

namespace ApiBase.Core.src.Application
{
    public class ApplicationBase<TEntity, TRepository> where TEntity : IdentifierGuid, new() where TRepository : IRepositoryBase<TEntity>
    {
        protected IUnitOfWork unitOfWork { get; set; }
        protected TRepository Repository { get; set; }

        protected ApplicationBase(IUnitOfWork _unitOfWork, TRepository repository)
        {
            unitOfWork = _unitOfWork;
            Repository = repository;
        }

        protected void Persist()
        {
            unitOfWork.Persist();
        }

        protected void RejectChanges()
        {
            unitOfWork.RejectChanges();
        }
    }
}
