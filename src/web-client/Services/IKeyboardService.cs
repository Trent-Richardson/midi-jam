using static web_client.Services.KeyboardService;

namespace web_client.Services;

public interface IKeyboardService
{
    Task SendNote(Note note);
    void SetupReceiver(CallbackDefinition callback);

    List<Note> PlayedNotes { get; set; }
}
