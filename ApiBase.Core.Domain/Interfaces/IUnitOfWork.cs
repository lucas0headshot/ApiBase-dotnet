namespace ApiBase.Core.Domain.Interfaces
{
    public interface IUnitOfWork : IDisposable
    {
        void Persist();
        void RejectChanges();
    }
}
