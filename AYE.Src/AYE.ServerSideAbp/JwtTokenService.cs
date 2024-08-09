using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;

namespace AYE.Abp.Web;

public class JwtTokenService
{
    private readonly string _secretKey;
    private readonly string _issuer;
    private readonly string _audience;

    // 提供默认值
    private const string DefaultSecretKey = "DefaultSecretKey12345";
    private const string DefaultIssuer = "DefaultIssuer";
    private const string DefaultAudience = "DefaultAudience";

    public JwtTokenService(IConfiguration configuration)
    {
        //_secretKey = configuration["Jwt:SecretKey"] ?? DefaultSecretKey;
        _secretKey = LoginModel.GenerateSecureKey(32);
        _issuer = configuration["Jwt:Issuer"] ?? DefaultIssuer;
        _audience = configuration["Jwt:Audience"] ?? DefaultAudience;
    }

    public string GenerateToken(string userId)
    {
        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_secretKey));
        var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

        var expiryDuration = TimeSpan.FromHours(1); // 设置 JWT 有效期为 1 小时

        var token = new JwtSecurityToken(
            issuer: _issuer,
            audience: _audience,
            claims: new List<Claim> { new Claim(ClaimTypes.Name, userId) },
            expires: DateTime.Now.Add(expiryDuration), // 设置有效期
            signingCredentials: creds
        );

        return new JwtSecurityTokenHandler().WriteToken(token);
    }




}



public class LoginModel
{
    public static string GenerateSecureKey(int length)
    {
        using (var rng = new RNGCryptoServiceProvider())
        {
            var key = new byte[length];
            rng.GetBytes(key);
            return Convert.ToBase64String(key);
        }
    }
    public string UserName { get; set; }
    public string Password { get; set; }
    public string UserId { get;  set; }
    // ... 可能有其他验证字段
}