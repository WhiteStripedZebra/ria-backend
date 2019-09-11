using System.Threading.Tasks;
using Engineer.Hubs;
using Engineer.Hubs.Models;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Cors.Infrastructure;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;

namespace Engineer.Api.Controllers.SignalR
{
    [EnableCors]
    public class ChatController : Controller
    {
        private readonly IHubContext<ChatHub> _chatHubContext;

        public ChatController(IHubContext<ChatHub> chatHubContext)
        {
            _chatHubContext = chatHubContext;
        }
    }
}