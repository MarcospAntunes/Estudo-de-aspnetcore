using Microsoft.AspNetCore.Mvc;
using WebApi.Services;
using WebApi.Model;

namespace WebApi.Controllers
{
  [ApiController]
  [Route("api/v1/auth")]
  public class AuthController : Controller
  {

    [HttpPost]
    public IActionResult Auth(string username, string password)
    {
      if (username == "marcos" && password == "1234")
      {
        var token = TokenService.GenerateToken(new Colaboradores("marcos", "1234", "1234", null));

        return Ok(token);
      }

      return BadRequest("Username or password invalid");
    }
  }
}