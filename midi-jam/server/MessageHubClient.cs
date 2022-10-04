using Microsoft.AspNetCore.SignalR;
using web_client.Services;

namespace server;

public class MessageHubClient : Hub<IMessageHubClient>
{
    public async Task Update(Note note)
    {
        Console.WriteLine($"recieved note: {note}");
        await Clients.All.Update(note);
        Console.WriteLine($"sent note: {note}");
    }
}
