using Microsoft.AspNetCore.SignalR;

namespace TodoApi.Hubs;

public class ChatHub : Hub
{
    public async Task SendMessage(string user, string message)
    {
        await Clients.All.SendAsync("SendMessage", user, message);
    }
    
    public async Task Refresh()
    {
        await Clients.All.SendAsync("RefreshData");
    }
}