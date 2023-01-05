namespace MTR.Web.Shared.Models;

public record UserInfo
{
    public bool IsAuthenticated { get; set; }
    public string Username { get; set; }
    public Dictionary<string, string> ExposedClaims { get; set; } = new();
}
