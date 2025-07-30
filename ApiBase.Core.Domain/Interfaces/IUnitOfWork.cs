namespace ApiBase.Core.Domain.Interfaces
{
    public interface IUnitOfWork : IDisposable
    {
        void Commit();
        void RollbackChanges();
    }
}
