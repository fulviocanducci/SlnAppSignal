using Microsoft.AspNetCore.SignalR;

namespace Web.Hubs
{
   public interface ICountHub
   {
      Task SendCountAsync(long count);
      Task GetCountAsync();
   }
}
