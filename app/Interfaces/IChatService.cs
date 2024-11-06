namespace TodoApi.app.Interfaces;

public interface IChatService
{
    Task SendMessage(string user, string message);
    Task Refresh( string typePlusId );
}