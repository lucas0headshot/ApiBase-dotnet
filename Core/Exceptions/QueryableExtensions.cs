using Microsoft.EntityFrameworkCore;

namespace Core.Exceptions
{
    public static class QueryableExtensions
    {
        public static async Task<T> FirstOrThrowAsync<T>(
        this IQueryable<T> query, string entityName, object key)
        {
            var result = await query.FirstOrDefaultAsync();
            if (result == null)
                throw new EntityNotFoundException(entityName, key);
            return result;
        }
    }
}
