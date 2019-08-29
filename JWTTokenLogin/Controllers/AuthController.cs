using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;

namespace JWTTokenLogin.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
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
