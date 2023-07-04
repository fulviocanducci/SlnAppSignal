namespace Web.Repositories
{
   public interface IUnitOfWork
   {
      int Commit();
      int Commit(bool acceptAllChangesOnSuccess);
      Task<int> CommitAsync(CancellationToken cancellationToken = default);
   }
}