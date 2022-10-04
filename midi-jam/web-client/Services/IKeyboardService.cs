using Microsoft.AspNetCore.SignalR.Client;

namespace web_client.Services;

public interface IKeyboardService
{
    Task SendNote(Note note);
    void SetupReceiver(HubConnection connection);

    List<Note> PlayedNotes { get; set; }
}
