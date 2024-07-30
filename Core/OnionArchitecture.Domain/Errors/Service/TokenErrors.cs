using OnionArchitecture.Domain.Shared;

namespace OnionArchitecture.Domain.Errors.Service;

public class TokenErrors
{
    public static readonly Error InvalidToken = Error.BadRequest("Invalid Jwt Token");
    public static readonly Error UserIdClaimNotFound = Error.BadRequest("Token doesnt contain user id");
}