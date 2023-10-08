using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Components.Authorization;

namespace SHIIIIIIIIT.Tools.Extensions;

internal static class ServiceCollectionExtensions
{
    internal static IServiceCollection ConfigureAuthorization (this IServiceCollection services)
    {
        services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
                .AddCookie(opt => opt.Cookie.Name = "sample");

        services.AddAuthorization(options => {

        });

        return services;
    }

    internal static IServiceCollection AddServices (this IServiceCollection services)
    {
        services.AddScoped<AuthenticationStateProvider, CustomAuthStateProvider>();
        services.AddScoped<SessionManager>();

        services.AddScoped(http => new HttpClient() {
            BaseAddress = new("http://localhost:5174")
        });

        services.AddTransient<IAuthorizationManager, AuthorizationManager>();

        return services;
    }
}