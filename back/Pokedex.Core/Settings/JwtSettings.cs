namespace Pokedex.Core.Settings;

public class JwtSettings
{
    public string Issuer { get; set; } = string.Empty;
    public string CommonValidIn { get; set; } = string.Empty;
    public int ExpirationHours { get; set; }
    
    public List<string> Audiences()
    {
        return new List<string> { CommonValidIn };
    }
}