using Web.Models;

namespace Web.Repositories
{
   public class UnitOfWork : IUnitOfWork
   {
      private readonly DataAccessContext _context;

      public UnitOfWork(DataAccessContext context)
      {
         _context = context;
      }

      public int Commit()
      {
         return _context.SaveChanges();
      }

      public int Commit(bool acceptAllChangesOnSuccess)
      {
         return _context.SaveChanges(acceptAllChangesOnSuccess);
      }

      public async Task<int> CommitAsync(CancellationToken cancellationToken = default)
      {
         return await _context.SaveChangesAsync(cancellationToken);
      }
   }
}
