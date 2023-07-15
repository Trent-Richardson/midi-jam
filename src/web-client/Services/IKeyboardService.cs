using static web_client.Services.KeyboardService;

namespace web_client.Services;

public interface IKeyboardService
{
    Task SendNote(Note note, string username);
    void SetupReceiver(CallbackDefinition callback);

    List<(string, Note)> PlayedNotes { get; set; }
}
