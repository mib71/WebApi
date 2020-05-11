using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Threading.Tasks;

namespace WebApi.Areas.Api
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly UserManager<IdentityUser> userManager;

        private readonly IConfiguration configuration;

        public AuthController(UserManager<IdentityUser> userManager, IConfiguration configuration)
        {
            this.userManager = userManager;
            this.configuration = configuration;
        }

        [HttpPost]
        public async Task<ActionResult> CreateAcces(User user)
        {
            var userName = await userManager.FindByNameAsync(user.userName);

            var isAcces = await userManager.CheckPasswordAsync(userName, user.password);

            if (!isAcces)
            {
                return Unauthorized();
            }
            Token token = GeneratedToken(userName);

            return Ok(token);
        }

        private Token GeneratedToken(IdentityUser userName)
        {

            var signingKey = Convert.FromBase64String(configuration["Token:SigningKey"]);

            var claims = new ClaimsIdentity(new List<Claim>
            {
                 new Claim("UserId", userName.Id),
                 new Claim("UserName", userName.UserName)
            });

            bool isAdminstrator = userManager.IsInRoleAsync(userName, "Administrator").Result;

            if (isAdminstrator)
            {
                claims.AddClaim(new Claim("Admin", "true"));
            }

            var decriptedToken = new SecurityTokenDescriptor
            {
                IssuedAt = DateTime.UtcNow,
                Expires = DateTime.UtcNow.AddMinutes(double.Parse(configuration["Token:ExpirationInMinutes"])),
                Subject = claims,

                SigningCredentials = new SigningCredentials(
                    new SymmetricSecurityKey(signingKey),
                    SecurityAlgorithms.HmacSha256Signature)
            };

            var jwtTokenHandler = new JwtSecurityTokenHandler();

            var jwtSecurityToken = jwtTokenHandler.CreateJwtSecurityToken(decriptedToken);

            Token token = new Token
            {
                token = jwtTokenHandler.WriteToken(jwtSecurityToken)
            };
            return token;
        }
    }

    public class Token
    {
        public string token { get; set; }   // Value
    }

    public class User
    {
        public string userName { get; set; }
        public string password { get; set; }
    }
}
