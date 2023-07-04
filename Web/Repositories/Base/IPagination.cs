using Canducci.Pagination.Interfaces;

namespace Web.Repositories.Base
{
   public interface IPagination<T> where T: class, new()
   {
      public Task<IPaginated<T>> PaginationAsync
      (
         int? current,
         Func<IQueryable<T>, IOrderedQueryable<T>>? orderBy = null,
         Func<IQueryable<T>, IQueryable<T>>? where = null
      );
   }
}
