using Web.Models;

namespace Web.Repositories
{
   public abstract class RepositoryPeopleBase : RepositoryBase<People>, IRepositoryPeople
   {
      public RepositoryPeopleBase(DataAccessContext context) : base(context)
      {
      }
   }
}
