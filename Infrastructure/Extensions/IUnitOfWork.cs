namespace Infrastructure.Extensions
{
    public interface IUnitOfWork
    {
        Task<bool> PersistAsync();
    }
}
