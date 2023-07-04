using Microsoft.EntityFrameworkCore;
using Web.Models.Mappings;

namespace Web.Models
{
   public class DataAccessContext : DbContext
   {

      public DataAccessContext(DbContextOptions<DataAccessContext> options) : base(options) { }

      public DbSet<People> Peoples { get; set; }

      protected override void OnModelCreating(ModelBuilder modelBuilder)
      {
         modelBuilder.ApplyConfiguration(new PeopleMapping());
      }
   }
}
