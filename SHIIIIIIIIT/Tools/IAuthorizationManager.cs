namespace SHIIIIIIIIT.Tools;

public interface IAuthorizationManager
{
    bool IsAuthorized { get; }

    Task AuthorizeAsync (Guid userID);

    Task LogoutAsync ();
}