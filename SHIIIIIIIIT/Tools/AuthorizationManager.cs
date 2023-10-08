using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Authorization;

namespace SHIIIIIIIIT.Tools;

public class AuthorizationManager : IAuthorizationManager
{
    private readonly NavigationManager _navigation;

    private readonly SessionManager _sessionManager;
    private readonly AuthenticationStateProvider _authenticationStateProvider;

    public AuthorizationManager (SessionManager sessionManager, AuthenticationStateProvider authenticationStateProvider, NavigationManager navigation)
    {
        _sessionManager = sessionManager;
        _authenticationStateProvider = authenticationStateProvider;
        _navigation = navigation;
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

        _navigation.NavigateTo("/");
    }
}