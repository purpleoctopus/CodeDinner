namespace CodeBreakfast.Logic.Services;

public class ConnectionManager
{
    private Dictionary<Guid, string> _connections = new();

    public void AddConnection(string connectionId, Guid userId)
    {
        _connections.Add(userId, connectionId);
    }

    public string? GetConnectionIdByUserId(Guid userId)
    {
        _connections.TryGetValue(userId, out var connectionId);
        return connectionId;
    }

    public void RemoveConnection(Guid userId)
    {
        _connections.Remove(userId);
    }
}