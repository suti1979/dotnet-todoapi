using Microsoft.AspNetCore.SignalR;
using TodoApi.app.Interfaces;
using TodoApi.Hubs;

namespace TodoApi.app.Services;

public class ChatService(IHubContext<ChatHub> hubContext) : IChatService
{
    public async Task SendMessage(string user, string message)
    {
        await hubContext.Clients.All.SendAsync("SendMessage", user, message);
    }
    
    public async Task Refresh(string typePlusId)
    {
        await hubContext.Clients.All.SendAsync("RefreshData", typePlusId);
    }
}