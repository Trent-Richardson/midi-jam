using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using server;
using web_client.Services;

namespace SignalRDemo.Controllers;

[Route("api/[controller]")]
[ApiController]
public class MidiController : ControllerBase
{
    private readonly IHubContext<MessageHubClient, IMessageHubClient> messageHub;
    public MidiController(IHubContext<MessageHubClient, IMessageHubClient> _messageHub)
    {
        messageHub = _messageHub;
    }

    [HttpPost]
    [Route("note")]
    public IActionResult Get([FromBody] Note note)
    {
        messageHub.Clients.All.Update(note);
        return Ok("Note sent successfully to all users!");
    }
}