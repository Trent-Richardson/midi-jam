using web_client.Services;

namespace server;

public interface INoteHubClient
{
    Task Update(Note note, string username);
}
