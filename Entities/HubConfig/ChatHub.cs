using System;
using System.Threading.Tasks;
using Entities.DTOs;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.SignalR;

namespace Entities.HubConfig
{

    public interface IChatHub 
    {
        Task Send(UserMessageDto userMessage);
        Task Notify(string message);
    }

    [Authorize]
    public class ChatHub: Hub<IChatHub>
    {
        private string _username;
       
        public async Task Send(UserMessageDto userMessage) =>
            await Clients.All.Send(userMessage);

        /*public override async Task OnConnectedAsync()
        {
            await Clients.Others.Notify($"{Context.User.Identity.Name} join to the chat");
            _username = Context.User.Identity.Name;
            await base.OnConnectedAsync();
        }*/

        /*public override async Task OnDisconnectedAsync(Exception exception)
        {
            await Clients.Others.Notify($"{_username} leave the chat");
            await base.OnConnectedAsync();
        }*/
    }
}