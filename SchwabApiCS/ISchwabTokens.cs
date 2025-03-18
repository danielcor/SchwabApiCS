namespace SchwabApiCS;

public abstract class SchwabTokensBase
{
    public virtual SchwabTokensData? tokens { get; set; }
    public bool NeedsReAuthorization => DateTime.Now >= tokens.RefreshTokenExpires;
    public string AccessToken => tokens?.AccessToken ?? throw new Exception("AccessToken isn't set.");
    public bool IsExpired(DateTime expirationDate)
    {
        var now = DateTime.Now;
        return now < tokens.AccessTokenExpires || now > tokens.AccessTokenExpires.AddMinutes(-30);
    }
}