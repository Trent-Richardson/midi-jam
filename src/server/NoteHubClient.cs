using Microsoft.AspNetCore.SignalR;
using web_client.Services;

namespace server;

public class NoteHubClient : Hub<INoteHubClient>
{
    public async Task Update(Note note, string username)
    {
        await Clients.All.Update(note, username);
        Console.WriteLine($"{username} sent note: {note}");
    }
}
