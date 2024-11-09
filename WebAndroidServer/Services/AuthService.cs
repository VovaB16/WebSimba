using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using BCrypt.Net-Next;

public class AuthService
{
    private readonly string _jwtSecret;
    public AuthService(string jwtSecret)
    {
        _jwtSecret = jwtSecret;
    }

    public string CreatePasswordHash(string password)
    {
        return BCrypt.HashPassword(password);
    }

    public bool VerifyPasswordHash(string password, string passwordHash)
    {
        return BCrypt.Verify(password, passwordHash);
    }

    public string GenerateJwtToken(string userId)
    {
        var tokenHandler = new JwtSecurityTokenHandler();
        var key = Encoding.ASCII.GetBytes(_jwtSecret);
        var tokenDescriptor = new SecurityTokenDescriptor
        {
            Subject = new ClaimsIdentity(new[] { new Claim("id", userId) }),
            Expires = DateTime.UtcNow.AddDays(7),
            SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
        };
        var token = tokenHandler.CreateToken(tokenDescriptor);
        return tokenHandler.WriteToken(token);
    }
}