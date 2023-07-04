using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace Web.Repositories.Base
{
   public interface IAddAsync<T> where T : class, new()
   {
      ValueTask<EntityEntry<T>> AddAsync(T model);
   }

   public interface ICountAsync<T> where T : class, new()
   {
      Task<long> CountAsync();
   }
}
