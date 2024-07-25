namespace OnionArchitecture.Application.DTOs.Token;

public class TokenDto
{
    public TokenDto(string accessToken, string refreshToken)
    {
        AccessToken = accessToken;
        RefreshToken = refreshToken;
    }

    public string AccessToken { get; set; }
    public string RefreshToken { get; set; }
}