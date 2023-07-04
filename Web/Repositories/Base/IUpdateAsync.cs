using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace Web.Repositories.Base
{
   public interface IUpdateAsync<T> where T : class, new()
   {
      EntityEntry<T> UpdateAsync(T model);
   }
}
