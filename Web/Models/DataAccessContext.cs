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
         //modelBuilder.UseCollation("Latin1_General_CI_AI");
         modelBuilder.ApplyConfiguration(new PeopleMapping());
      }
   }
}
