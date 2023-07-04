using System.Linq.Expressions;

namespace Web.Repositories.Base
{
   public interface ICrudBasicAsync<T> :
      IAddAsync<T>,
      IUpdateAsync<T>,
      IFindAsync<T>,
      IRemove<T>,
      ICountAsync<T>,
      IAnyAsync<T>,
      IPagination<T>,
      IAllAsync<T> where T : class, new()
   { }
   public interface IPagination<T>
   {
      public IPagination<T> PaginationAsync
      (
         int? current,
         Func<IQueryable<T>, IOrderedQueryable<T>>? orderBy = null,
         Func<IQueryable<T>, IQueryable<T>>? where = null
      );
   }
}
