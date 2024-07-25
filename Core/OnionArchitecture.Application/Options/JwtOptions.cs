namespace OnionArchitecture.Application.Options;

public class JwtOptions
{
    public const string OptionKey = "JwtOptions";

    public string Audience { get; set; }
    public string Issuer { get; set; }
    public string SecurityKey { get; set; }
    public int AccessTokenExpireTimeSecond { get; set; }
    public int RefreshTokenExpireTimeSecond { get; set; }

}