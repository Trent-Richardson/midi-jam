using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using web_client.Services;

namespace server.Controllers;

[Route("api/[controller]")]
[ApiController]
public class MidiController : ControllerBase
{
    private readonly IHubContext<NoteHubClient, INoteHubClient> messageHub;
    public MidiController(IHubContext<NoteHubClient, INoteHubClient> _messageHub)
    {
        messageHub = _messageHub;
    }

    [HttpPost]
    [Route("note")]
    public IActionResult Get([FromBody] Note note, string username)
    {
        messageHub.Clients.All.Update(note, username);
        return Ok("Note sent successfully to all users!");
    }
}