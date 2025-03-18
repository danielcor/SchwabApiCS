namespace SchwabApiCS;

/// <summary>
/// What is saved in the token data file
/// </summary>
public class SchwabTokensData
{
    public string AccessToken { get; set; } = "";
    public string RefreshToken { get; set; } = "";
    public DateTime AccessTokenExpires { get; set; }
    public DateTime RefreshTokenExpires { get; set; }

    public string AppKey { get; set; } = "";
    public string Secret { get; set; } = "";
    public string Redirect_uri { get; set; } = "";
}