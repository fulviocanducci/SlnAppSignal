using Canducci.Pagination;
using Canducci.Pagination.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using System.Linq.Expressions;
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

      public IAsyncEnumerable<T> AllAsync(Func<IQueryable<T>, IOrderedQueryable<T>> orderByBuilder)
      {
         return orderByBuilder.Invoke(_model).AsNoTrackingWithIdentityResolution().AsAsyncEnumerable();
      }

      public async Task<bool> AnyAsync(Expression<Func<T, bool>> where)
      {
         return await _model.AsNoTracking().AnyAsync(where);
      }

      public async Task<long> CountAsync()
      {
         return await _model.LongCountAsync();
      }

      public async Task<T?> FindAsync(params object?[]? keyValues)
      {
         return await _model.FindAsync(keyValues);
      }

      public async Task<IPaginated<T>> PaginationAsync(int? current, Func<IQueryable<T>, IOrderedQueryable<T>>? orderBy = null, Func<IQueryable<T>, IQueryable<T>>? where = null)
      {
         var entity = _model.AsNoTrackingWithIdentityResolution();
         if (orderBy != null)
         {
            entity = orderBy(entity);
         }
         if (where != null)
         {
            entity = where(entity);
         }
         return await entity.ToPaginatedAsync(current ?? 1, 10);
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
