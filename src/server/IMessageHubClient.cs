using web_client.Services;

namespace server;

public interface IMessageHubClient
{
    Task Update(Note note, string username);
}
