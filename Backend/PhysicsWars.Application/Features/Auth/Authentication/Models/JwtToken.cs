using System.IdentityModel.Tokens.Jwt;
using System.Text;
using Microsoft.IdentityModel.Tokens;

namespace PhysicsWars.Application.Features.Auth.Authentication.Models;

public class JwtToken
{
    public Guid UserId { get; init; }
    public string Username { get; init; }
    public long Expiration { get; init; }

    public JwtToken(Guid userId, string username, TimeSpan lifespan)
    {
        UserId = userId;
        Username = username;
        Expiration = GetExpiration(lifespan);
    }

    public JwtToken(string token)
    {
        var parts = token.Split('.');
        if (parts.Length != 3)
        {
            throw new SecurityTokenException("Invalid token");
        }

        var payload = parts[1];
        var payloadJson = Encoding.UTF8.GetString(Base64Url.Decode(payload));
        var payloadData = JsonConvert.DeserializeObject<Dictionary<string, object>>(payloadJson);

        UserId = Guid.Parse(payloadData["sub"].ToString());
        Username = payloadData["username"].ToString();
        Expires = long.Parse(payloadData["exp"].ToString());
    }

    public bool IsExpired()
    {
        return DateTimeOffset.UtcNow.ToUnixTimeSeconds() > Expires;
    }

    public string GetToken()
    {
        var header = new JwtHeader(new SigningCredentials(new SymmetricSecurityKey(Encoding.UTF8.GetBytes("secret")), SecurityAlgorithms.HmacSha256));
        var payload = new JwtPayload
        {
            { "sub", UserId },
            { "username", Username },
            { "exp", Expiration }
        };

        var token = new JwtSecurityToken(header, payload);
        return new JwtSecurityTokenHandler().WriteToken(token);
    }

    private long GetExpiration(TimeSpan lifespan)
    {
        return (long)lifespan.TotalSeconds + DateTimeOffset.UtcNow.ToUnixTimeSeconds();
    }
}