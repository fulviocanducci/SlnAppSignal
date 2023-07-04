using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
namespace Web.Models.Mappings
{
   public class PeopleMapping : IEntityTypeConfiguration<People>
   {
      public void Configure(EntityTypeBuilder<People> builder)
      {
         builder.ToTable("Peoples");
         builder.HasKey(x => x.Id);
         builder.Property(x => x.Id).UseIdentityColumn();
         builder.Property(x => x.Name).HasMaxLength(100).HasColumnType("nvarchar(100)").UseCollation("Latin1_General_CI_AI").IsRequired();
      }
   }
}
