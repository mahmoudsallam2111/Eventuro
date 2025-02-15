using Evently.Common.Domain;
using Evently.Modules.Users.Application.Users.GetUser;
using Evently.Modules.Users.PublicApi;
using MediatR;
using UserResponse = Evently.Modules.Users.PublicApi.UserResponse;



namespace Evently.Modules.Users.Infrastructure.PublicApi;
internal sealed class UserApi(ISender sender) : IUserApi
{
    public async Task<Modules.Users.PublicApi.UserResponse?> GetUserAsync(Guid id, CancellationToken cancellationToken)
    {
        Result<Application.Users.GetUser.UserResponse> result = await sender.Send(new GetUserQuery(id), cancellationToken);
        if (result.IsFailure)
        {
            return null;
        }

        return new UserResponse(
            result.Value.Id,
            result.Value.Email,
            result.Value.FirstName,
            result.Value.LastName
            );
    }
}
