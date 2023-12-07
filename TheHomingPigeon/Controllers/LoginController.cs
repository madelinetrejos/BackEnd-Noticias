using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using TheHomingPigeon.Data.Interface;
using TheHomingPigeon.Model;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace TheHomingPigeon.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LoginController : ControllerBase
    {
        private readonly ILoginRepository _loginRepository;
        public IConfiguration _configuration;

        public LoginController(ILoginRepository loginRepository, IConfiguration configuration)
        {
            _loginRepository = loginRepository;
            _configuration = configuration;
        }

        [HttpPost]
        public async Task<IActionResult> Login([FromBody] Login login)
        {
            if (login == null)
            {
                return BadRequest();
            }
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }


            var existUser = await _loginRepository.ExistUser(login.username);


            if (existUser == null)
            {
                var responseBadRequest = new
                {
                    status = 401,
                    message = "Usuario no registrado",
                    result = false
                };

                return BadRequest(responseBadRequest);
            }

            //bool isValidPassword = BCrypt.Net.BCrypt.Verify(login.password, existUser.password);

            //if (!isValidPassword)
            //{
            //    var responseBadRequest = new
            //    {
            //        status = 401,
            //        message = "Usuario o contraseña incorrecto",
            //        result = false
            //    };

            //    return BadRequest(responseBadRequest);
            //}

            var jwt = _configuration.GetSection("Jwt").Get<Jwt>();

            var claims = new[]
            {
                new Claim(JwtRegisteredClaimNames.Sub, jwt.Subject),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                new Claim(JwtRegisteredClaimNames.Iat, DateTime.UtcNow.ToString()),
                new Claim("iduser", existUser.iduser.ToString(), ClaimValueTypes.Integer),
                new Claim("username", existUser.username)
            };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwt.Key));
            var singIn = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                jwt.Issuer,
                jwt.Audience,
                claims,
                expires: DateTime.Now.AddMinutes(180),
                signingCredentials: singIn
            );


            var response = new
            {
                iduser = existUser.iduser,
                username = existUser.username,
                result = true,
                token = new JwtSecurityTokenHandler().WriteToken(token)
            };

            return Ok(response);
        }

    }
}
