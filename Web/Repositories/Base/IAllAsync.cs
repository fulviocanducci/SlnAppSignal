namespace Web.Repositories.Base
{
   public interface IAllAsync<T>
   {
      IAsyncEnumerable<T> AllAsync();
   }
}
