using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Web.Hubs;
using Web.Models;

namespace Web
{
   public class Program
   {
      public static void Main(string[] args)
      {
         var builder = WebApplication.CreateBuilder(args);

         builder.Services.AddControllersWithViews();
         builder.Services.AddSignalR();
         builder.Services.AddDbContext<DataAccessContext>(options =>
         {
            options.UseSqlServer(builder.Configuration.GetConnectionString("DataAccessContext"));
         });
         var app = builder.Build();
         if (!app.Environment.IsDevelopment())
         {
            app.UseExceptionHandler("/Home/Error");
            app.UseHsts();
         }
         app.UseHttpsRedirection();
         app.UseStaticFiles();
         app.UseRouting();
         app.UseAuthorization();
         app.MapHub<CountHub>("count-hub");
         app.MapControllerRoute(
             name: "default",
             pattern: "{controller=Home}/{action=Index}/{id?}");
         app.Run();
      }
   }
}