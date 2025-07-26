namespace ApiBase.Core.src.Domain.Interfaces
{
    public interface IUnitOfWork : IDisposable
    {
        void Persist();
        void RejectChanges();
    }
}
