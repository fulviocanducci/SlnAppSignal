namespace Web.Repositories.Base
{
   public interface IFindAsync<T>
   {
      Task<T?> FindAsync(params object?[]? keyValues);
   }
}
