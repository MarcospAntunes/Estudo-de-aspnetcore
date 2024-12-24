using System.Text;
using WebApi.Model;
using System.Security.Claims;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;

namespace WebApi.Services
{
  public class TokenService
  {
    public static object GenerateToken(Colaboradores colaboradores)
    {
      var key = Encoding.ASCII.GetBytes(Key.Secret);
      var tokenConfig = new SecurityTokenDescriptor
      {
        Subject = new ClaimsIdentity(new Claim[] {
          new Claim("colaboradorId", colaboradores.id.ToString()),
        }),
        Expires = DateTime.UtcNow.AddHours(3),
        SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature),
      };

      var tokenHandler = new JwtSecurityTokenHandler();
      var token = tokenHandler.CreateToken(tokenConfig);
      var tokenString = tokenHandler.WriteToken(token);

      return new
      {
        token = tokenString
      };

    }
  }
}