namespace SHIIIIIIIIT.Tools;

/// <summary>
/// Интерфейс для упрощения работы с авторизацией
/// </summary>
public interface IAuthorizationManager
{
    bool IsAuthorized { get; }

    Task AuthorizeAsync (Guid userID);

    Task LogoutAsync ();
}