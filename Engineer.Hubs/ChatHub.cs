using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.SignalR;

namespace Engineer.Hubs
{
    public class ChatHub : Hub
    {
        

        public ChatHub()
        {
            
        }

        public override Task OnConnectedAsync()
        {
            var connectionId = Context.ConnectionId;

            Clients.Client(connectionId).SendAsync("", CancellationToken.None);

            return base.OnConnectedAsync();
        }
    }
}