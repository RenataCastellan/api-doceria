using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace api_doceria.Services;

public class TokenService
{
    private readonly IConfiguration _config;

    public TokenService(IConfiguration config)
    {
        _config = config;
    }

    public string GerarTokenAdmin(int idAdmin, string login)
    {
        var claims = new List<Claim>
        {
            new Claim(ClaimTypes.NameIdentifier, idAdmin.ToString()),
            new Claim(ClaimTypes.Name, login),
            new Claim(ClaimTypes.Role, "Administrador")
        };

        return GerarToken(claims);
    }

    public string GerarTokenCliente(int idCliente, string nome)
    {
        var claims = new List<Claim>
        {
            new Claim(ClaimTypes.NameIdentifier, idCliente.ToString()),
            new Claim(ClaimTypes.Name, nome),
            new Claim(ClaimTypes.Role, "Cliente")
        };

        return GerarToken(claims);
    }

    private string GerarToken(List<Claim> claims)
    {
        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["Jwt:Key"]!));
        var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

        var token = new JwtSecurityToken(
            issuer: _config["Jwt:Issuer"],
            audience: _config["Jwt:Audience"],
            claims: claims,
            expires: DateTime.Now.AddHours(2),
            signingCredentials: credentials
        );

        return new JwtSecurityTokenHandler().WriteToken(token);
    }
}