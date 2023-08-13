using System.Net.WebSockets;
using System.Text;

namespace WebSocketSample;

public class Program
{
    public static async Task<int> Main()
    {
        Console.Title = "Websocket test client";
        Console.WriteLine("Websocket test client");

        var wsUrl = GetInput("\nProvide websocket url (default = wss://localhost:7260/note):", "wss://localhost:7260/note");

        await RunWebSockets(wsUrl);

        Console.ForegroundColor = ConsoleColor.DarkMagenta;
        Console.WriteLine("Complete.");

        return 0;
    }

    private static string GetInput(string prompt, string? defaultResponse = null)
    {
        Console.ForegroundColor = ConsoleColor.DarkYellow;
        Console.WriteLine(prompt);
        Console.ForegroundColor = ConsoleColor.DarkBlue;
        var input = Console.ReadLine();
        if (string.IsNullOrEmpty(input)) input = defaultResponse;
        Console.ResetColor();
        return input!;
    }

    private static async Task RunWebSockets(string url)
    {
        string message;
        var ws = new ClientWebSocket();

        await ws.ConnectAsync(new Uri(url), CancellationToken.None);

        await PerformHandshake(ws);

        while ((message = GetInput("\nEnter payload (or 'exit' to quit):", "{\"type\":1,\"target\":\"Update\",\"arguments\":[5, \"mr.console\"]}")) != "exit")
        {
            Task sending = Process(ws, url, message);
            var receiving = WriteOutput(ws);
            await Task.WhenAll(sending, receiving);
        }

        await ws.CloseOutputAsync(WebSocketCloseStatus.NormalClosure, "", CancellationToken.None);

        return;
    }

    private static async Task PerformHandshake(ClientWebSocket ws)
    {
        var handshake = Encoding.UTF8.GetBytes("{\r\n  \"protocol\":\"json\",\r\n  \"version\":1\r\n}\u001e");
        await ws.SendAsync(new ArraySegment<byte>(handshake), WebSocketMessageType.Text, endOfMessage: true, cancellationToken: CancellationToken.None);
    }

    private static Task Process(ClientWebSocket ws, string url, string message)
    {
        Console.WriteLine("Connected");

        var sending = Task.Run(async () =>
        {

            var bytes = Encoding.UTF8.GetBytes(message + "\u001e");
            await ws.SendAsync(new ArraySegment<byte>(bytes), WebSocketMessageType.Text, endOfMessage: true, cancellationToken: CancellationToken.None);
        });

        return sending;
    }

    private static async Task WriteOutput(ClientWebSocket ws)
    {
        var buffer = new byte[2048];

        while (true)
        {
            var result = await ws.ReceiveAsync(new ArraySegment<byte>(buffer), CancellationToken.None);

            if (result.MessageType == WebSocketMessageType.Text)
            {
                var response = Encoding.UTF8.GetString(buffer, 0, result.Count);
                Console.WriteLine(response);

                if (response.Contains("\"type\":1")) // success response .. lazy
                {
                    break;
                }

            }
            else if (result.MessageType == WebSocketMessageType.Binary)
            {
            }
            else if (result.MessageType == WebSocketMessageType.Close)
            {
                await ws.CloseOutputAsync(WebSocketCloseStatus.NormalClosure, "", CancellationToken.None);
                break;
            }

        }
    }
}
