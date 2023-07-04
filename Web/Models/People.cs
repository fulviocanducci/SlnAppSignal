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
      public string Name { get; set; } = null!;
   }
}