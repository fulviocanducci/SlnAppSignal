using Microsoft.EntityFrameworkCore.ChangeTracking;
using Web.Models;

namespace Web.Repositories.Base
{
   public interface IRemove<T> where T : class, new()
   { 
      EntityEntry<T> Remove(T model);
   }
}
