using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Web.Hubs;
using Web.Models;
using Web.Repositories;

namespace Web
{
   public class Program
   {
      public static void Main(string[] args)
      {
         var builder = WebApplication.CreateBuilder(args);

         builder.Services.AddControllersWithViews();
         builder.Services.AddSignalR();
         builder.Services.Configure<RouteOptions>(options =>
         {
            options.LowercaseUrls = true;
            options.LowercaseQueryStrings = true;
         });
         builder.Services.AddDbContext<DataAccessContext>(options =>
         {
            options.UseSqlServer(builder.Configuration.GetConnectionString("DataAccessContext"));
         });
         builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();
         builder.Services.AddScoped<RepositoryPeopleBase, RepositoryPeople>();
         
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