using Microsoft.AspNetCore.SignalR.Client;

namespace web_client.Services;

public class KeyboardService : IKeyboardService
{
    private static HubConnection _connection { get; set; }
    private Task _initialize { get; set; }
    private Task _start { get; set; }
    public List<Note> PlayedNotes { get; set; }

    public KeyboardService()
    {
        PlayedNotes = new();
        _connection = new HubConnectionBuilder()
            .WithUrl("https://localhost:7260/note")
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
                .WithUrl("https://localhost:7260/note")
                .WithAutomaticReconnect()
                .Build();

            SetupReceiver(_connection);

            _start = _connection.StartAsync();
            return _start;
        }

        return _start;
    }

    public void SetupReceiver(HubConnection _connection)
    {
        _connection.On<Note>("Update", (note) =>
        {
            PlayedNotes.Add(note);
            Console.WriteLine($"received note: {note}");
            try
            {
                throw new NotImplementedException();
            }
            catch (Exception ex)
            {
                throw;
            }
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
        catch (Exception ex)
        {
            throw;
        }
    }
}
