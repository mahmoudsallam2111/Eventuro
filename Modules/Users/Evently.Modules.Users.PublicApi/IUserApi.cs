namespace Evently.Modules.Users.PublicApi;

/// <summary>
/// api that are callable by other modules
/// </summary>
public interface IUserApi
{
    Task<UserResponse?> GetUserAsync(Guid id , CancellationToken cancellationToken);
}
