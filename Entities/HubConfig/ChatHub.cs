using System.Threading.Tasks;
using Entities.DTOs;
using Microsoft.AspNetCore.SignalR;

namespace Entities.HubConfig
{
    public class ChatHub: Hub
    {
        public async Task Send(UserMessageDto userMessage) =>
            await this.Clients.All.SendAsync("send", userMessage);
    }
}