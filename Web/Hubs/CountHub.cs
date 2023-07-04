using Microsoft.AspNetCore.SignalR;
using Microsoft.EntityFrameworkCore;
using Web.Models;
namespace Web.Hubs
{
   public sealed class CountHub : Hub<ICountHub>
   {
      private readonly DataAccessContext _dataAccessContext;

      public CountHub(DataAccessContext dataAccessContext)
      {
         _dataAccessContext = dataAccessContext;
      }

      public override Task OnConnectedAsync()
      {
         return base.OnConnectedAsync();
      }

      public async Task SendCountAsync(long count)
      {
         await Clients.All.SendCountAsync(count);
      }

      public async Task GetCountAsync()
      {
         long count = await _dataAccessContext.Peoples.AsNoTracking().LongCountAsync();
         await SendCountAsync(count);
      }
   }
}
