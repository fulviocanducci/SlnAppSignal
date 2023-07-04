using Web.Models;

namespace Web.Repositories
{
   public class RepositoryPeople : RepositoryPeopleBase
   {

      public RepositoryPeople(DataAccessContext context) : base(context) { }
   }
}
