using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using Swashbuckle.AspNetCore.Annotations;

namespace JWTTokenLogin.Controllers
{
  [Route("api/[controller]")]
  [ApiController]
  public class AuthController : ControllerBase
  {
    [SwaggerOperation(Summary = "Get the Token to access the service", Tags = new[] { "Token" })]
    [ProducesResponseType(typeof(string), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [HttpPost("token")]
    public IActionResult Token()
    {
      var header = Request.Headers["Authorization"];

      if (header.ToString().StartsWith("Basic"))
      {
        var credValue = header.ToString().Substring("Basic ".Length).Trim();
        var userNameAndPasswordEnc = Encoding.UTF8.GetString(Convert.FromBase64String(credValue));
        var userNameAndPassword = userNameAndPasswordEnc.Split(":");

        if (userNameAndPassword[0] == "Admin" && userNameAndPassword[1] == "Admin")
        {
          var claimsData = new[] { new Claim(ClaimTypes.Name, userNameAndPassword[0]) };
          var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("jhgayugwdjshdhjgdyugwd"));
          var signInCred = new SigningCredentials(key, SecurityAlgorithms.HmacSha256Signature);

          var token = new JwtSecurityToken
              (
              issuer: "lqueirozjr.com",
              audience: "lqueirozjr.com",
              expires: DateTime.Now.AddMinutes(60),
              claims: claimsData,
              signingCredentials: signInCred
              );

          var tokenString = new JwtSecurityTokenHandler().WriteToken(token);

          return Ok(tokenString);
        }
      }

      return BadRequest();
    }
  }
}
