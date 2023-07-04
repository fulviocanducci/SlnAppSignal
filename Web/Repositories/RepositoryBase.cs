using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Web.Models;
using Web.Repositories.Base;

namespace Web.Repositories
{
   public abstract class RepositoryBase<T> : ICrudBasicAsync<T> where T : class, new()
   {
      private readonly DataAccessContext _context;
      private readonly DbSet<T> _model;

      public RepositoryBase(DataAccessContext context)
      {
         _context = context;
         _model = _context.Set<T>();
      }

      public ValueTask<EntityEntry<T>> AddAsync(T model)
      {
         return _model.AddAsync(model);
      }

      public IAsyncEnumerable<T> AllAsync()
      {
         return _model.AsAsyncEnumerable();
      }

      public async Task<long> CountAsync()
      {
         return await _model.LongCountAsync();
      }

      public async Task<T?> FindAsync(params object?[]? keyValues)
      {
         return await _model.FindAsync(keyValues);
      }

      public EntityEntry<T> Remove(T model)
      {
         return _model.Remove(model);
      }

      public EntityEntry<T> UpdateAsync(T model)
      {
         return _model.Update(model);
      }
   }
}
