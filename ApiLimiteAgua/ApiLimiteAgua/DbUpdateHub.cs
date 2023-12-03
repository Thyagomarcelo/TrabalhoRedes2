// Hubs/DbUpdateHub.cs
using Microsoft.AspNetCore.SignalR;
using System.Threading.Tasks;

public class DbUpdateHub : Hub
{
    public async Task SendDbUpdate(string message)
    {
        await Clients.All.SendAsync("ReceiveDbUpdate", message);
    }
}