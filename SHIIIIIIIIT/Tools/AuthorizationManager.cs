using Microsoft.AspNetCore.Components.Authorization;

namespace SHIIIIIIIIT.Tools;

public class AuthorizationManager : IAuthorizationManager
{
    private readonly SessionManager _sessionManager;
    private readonly AuthenticationStateProvider _authenticationStateProvider;

    public AuthorizationManager (SessionManager sessionManager, AuthenticationStateProvider authenticationStateProvider)
    {
        _sessionManager = sessionManager;
        _authenticationStateProvider = authenticationStateProvider;
    }

    public bool IsAuthorized => ((CustomAuthStateProvider)_authenticationStateProvider).IsUserAuthorized;

    public async Task AuthorizeAsync (Guid userID)
    {
        var session = await _sessionManager.GenerateSessionAsync(userID);
        ((CustomAuthStateProvider)_authenticationStateProvider).AuthUser(session, userID);
    }

    public async Task LogoutAsync ()
    {
        ((CustomAuthStateProvider)_authenticationStateProvider).Logout ();
        await _sessionManager.LogoutAsync ();
    }
}