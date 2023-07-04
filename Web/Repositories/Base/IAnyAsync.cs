using System.Linq.Expressions;

namespace Web.Repositories.Base
{
   public interface IAnyAsync<T>
   {
      Task<bool> AnyAsync(Expression<Func<T, bool>> where);
   }
}
