using System;
using System.Threading;
using System.Threading.Tasks;
using Engineer.Domain.Repositories;
using Engineer.Hubs.Helpers;
using Engineer.Hubs.Models;
using Microsoft.AspNetCore.SignalR;

namespace Engineer.Hubs
{
    public class ChatHub : Hub
    {
        private readonly ITodoRepository _todoRepository;

        public ChatHub(ITodoRepository todoRepository)
        {
            _todoRepository = todoRepository;
        }

        public override Task OnConnectedAsync()
        {
            var connectionId = Context.ConnectionId;

            return base.OnConnectedAsync();
        }

        public override Task OnDisconnectedAsync(
            Exception exception)
        {
            var connectionId = Context.ConnectionId;

            return base.OnDisconnectedAsync(exception);
        }

        public async Task SendMessage(
            ChatHubMessageDTO chatHubDto)
        {
            await Clients.All.SendCoreAsync(ChatFunction.ReceiveMessage,
                new object[] {chatHubDto},
                CancellationToken.None);
        }

        public async Task ReceiveOrders()
        {
            await Clients.Caller.SendCoreAsync(ChatFunction.ReceiveOrders,
                new object[]
                    {await _todoRepository.GetTasksAsync()},
                CancellationToken.None);
        }
    }
}