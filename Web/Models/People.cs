using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
namespace Web.Models
{
   public sealed class People
   {
      public People()
      {
         Id = 0L;
         Name = string.Empty;
      }

      public People(string name)
      {
         Id = 0;
         Name = name;
      }

      public People(long id, string name)
      {
         Id = id;
         Name = name ?? throw new ArgumentNullException(nameof(name));
      }

      public long Id { get; set; }

      [Required(ErrorMessage = "Digite o nome completo")]      
      [MaxLength(100, ErrorMessage = "Até 100 caracteres")]
      [DisplayName("Nome completo")]
      public string Name { get; set; } = null!;
   }
}