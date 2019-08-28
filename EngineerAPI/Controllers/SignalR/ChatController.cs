using System.Threading.Tasks;
using Engineer.Hubs;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Cors.Infrastructure;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;

namespace Engineer.Api.Controllers.SignalR
{
    public class ChatController : Controller
    {
        private readonly IHubContext<ChatHub> _chatHubContext;

        public ChatController(IHubContext<ChatHub> chatHubContext)
        {
            _chatHubContext = chatHubContext;
        }

        [HttpPost]
        public async Task<IActionResult> SendMessage([FromBody] string text)
        {

            return Accepted();
        }

    }
}