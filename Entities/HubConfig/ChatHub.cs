using System;
using System.Threading.Tasks;
using Entities.DTOs;
using Microsoft.AspNetCore.SignalR;

namespace Entities.HubConfig
{

    public interface IChatHub 
    {
        Task Send(UserMessageDto userMessage);
        Task Notify(string message);
    }

    public class ChatHub: Hub<IChatHub>
    {
        public async Task Send(UserMessageDto userMessage) =>
            await Clients.All.Send(userMessage);

        public override async Task OnConnectedAsync()
        {
            await Clients.Others.Notify($"{Context.ConnectionId} join to the chat");
            await base.OnConnectedAsync();
        }

        public override async Task OnDisconnectedAsync(Exception exception)
        {
            await Clients.Others.Notify($"{Context.ConnectionId} leave the chat");
            await base.OnConnectedAsync();
        }
    }
}