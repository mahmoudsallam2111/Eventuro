using Microsoft.Extensions.DependencyInjection;

namespace Evently.Common.Infrastructure.Authentication;
internal static class AuthenticationExtensions
{
    public static IServiceCollection AddAuthenticationInternal(this IServiceCollection services)
    {
        services.AddAuthorization();

        services.AddAuthentication().AddBearerToken();

        services.AddHttpContextAccessor();

        services.ConfigureOptions<JwtBearerConfigureOptions>();
        return services;
    }
}
