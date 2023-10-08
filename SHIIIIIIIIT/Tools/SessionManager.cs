using Microsoft.AspNetCore.Components.Server.ProtectedBrowserStorage;

namespace SHIIIIIIIIT.Tools;

/// <summary>
/// TODO: Подумать, можно ли переделать, а если можно - то как?
/// возможно я что-то сломал
/// </summary>
public class SessionManager
{
    private readonly ProtectedBrowserStorage _storage;

    private static readonly Dictionary<string, Guid> _userSesssions = new();

    public SessionManager (ProtectedSessionStorage storage)
    {
        _storage = storage;
    }

    public async Task<Guid> GenerateSessionAsync (Guid idUser)
    {
        var session = Guid.NewGuid();

        _userSesssions.Add(session.ToString(), idUser);
        await _storage.SetAsync("session", session.ToString());

        return session;
    }

    public Guid? GetUserId (Guid session)
    {
        if (!_userSesssions.ContainsKey(session.ToString()))
            return null;

        return _userSesssions[session.ToString()];
    }

    public async Task LogoutAsync ()
    {
        var session = await _storage.GetAsync<string>("session");
        _userSesssions.Remove(session.Value!);
        await _storage.DeleteAsync("session");
    }
}