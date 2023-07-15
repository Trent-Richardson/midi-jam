using Microsoft.AspNetCore.SignalR.Client;

namespace web_client.Services;

public class KeyboardService : IKeyboardService
{
    private readonly IConfiguration _config;

    private static HubConnection _connection { get; set; }
    private Task _initialize { get; set; }
    private Task _start { get; set; }
    public List<Note> PlayedNotes { get; set; }

    public delegate void CallbackDefinition(Note note);

    public KeyboardService(IConfiguration config)
    {
        _config = config;
        PlayedNotes = new();
        _connection = new HubConnectionBuilder()
            .WithUrl(config.GetValue("HubUrl", "https://localhost:7260/note"))
            .WithAutomaticReconnect()
            .Build();

        _initialize = Initialize();

        _connection.Closed += async (error) =>
        {
            await Task.Delay(new Random().Next(0, 5) * 1000);
            await _connection.StartAsync();
        };
    }

    private Task Initialize()
    {
        if (_connection is null || _connection.State != HubConnectionState.Connected || _start is null)
        {
            _connection = new HubConnectionBuilder()
                .WithUrl(_config.GetValue("HubUrl", "https://localhost:7260/note"))
                .WithAutomaticReconnect()
                .Build();

            _start = _connection.StartAsync();
            return _start;
        }

        return _start;
    }

    public void SetupReceiver(CallbackDefinition callback)
    {
        _connection.On<Note>("Update", (note) =>
        {
            PlayedNotes.Insert(0, note);
            Console.WriteLine($"received note: {note}");

            callback(note);
        });
    }

    public async Task SendNote(Note note)
    {
        await _initialize;
        try
        {
            await _connection.InvokeAsync("Update", note);
            Console.WriteLine($"sent note: {note}");
        }
        catch (Exception)
        {
            throw;
        }
    }
}
