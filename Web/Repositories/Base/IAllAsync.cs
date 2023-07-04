namespace Web.Repositories.Base
{
   public interface IAllAsync<T>
   {
      IAsyncEnumerable<T> AllAsync();
      IAsyncEnumerable<T> AllAsync(Func<IQueryable<T>, IOrderedQueryable<T>> orderByBuilder);
   }
}
