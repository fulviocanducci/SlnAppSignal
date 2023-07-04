namespace Web.Repositories.Base
{
   public interface ICrudBasicAsync<T> :
      IAddAsync<T>,
      IUpdateAsync<T>,
      IFindAsync<T>,
      IRemove<T>,
      ICountAsync<T>,
      IAnyAsync<T>,
      IAllAsync<T> where T: class, new()
   { }
}
