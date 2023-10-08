using Microsoft.AspNetCore.Components.Authorization;
using System.Security.Claims;

namespace SHIIIIIIIIT.Tools;

/// <summary>
/// TODO: Подумать, можно ли переделать, а если можно - то как?
/// </summary>
public class CustomAuthStateProvider : AuthenticationStateProvider
{
    private ClaimsIdentity _currentUser = Anon;

    public override Task<AuthenticationState> GetAuthenticationStateAsync()
    {
        ClaimsPrincipal claimsPrincipal = new(_currentUser);

        return Task.FromResult(new AuthenticationState(claimsPrincipal));
    }

    public bool IsUserAuthorized => _currentUser.IsAuthenticated;

    private static ClaimsIdentity Anon => new ();

    public void AuthUser (Guid session, Guid id)
    {
        _currentUser = new ClaimsIdentity(new[] {
                                              new Claim(ClaimTypes.Sid, id.ToString()),
                                              new Claim(ClaimTypes.Name, session.ToString()),
                                          }, "Auth");

        var task = GetAuthenticationStateAsync();
        NotifyAuthenticationStateChanged(task);
    }

    public void Logout ()
    {
        _currentUser = Anon;
        var task = GetAuthenticationStateAsync();
        NotifyAuthenticationStateChanged(task);
    }
}