using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Text;

namespace TheHomingPigeon.Middlewares
{
    public class JwtValidateController : Controller
    {
        private readonly string secretKey;
        private readonly string issuer;
        private readonly string audience;

        public JwtValidateController(IConfiguration configuration)
        {
            secretKey = configuration["Jwt:Key"];
            issuer = configuration["Jwt:Issuer"];
            audience = configuration["Jwt:Audience"];
        }

        public async Task<IActionResult> HandleTokenValidation(string token)
        {
            if (string.IsNullOrEmpty(token))
            {
                var tokenNotProvided = new
                {
                    success = false,
                    message = "Internal server error",
                    status = 401,
                    detail = "Token is required",
                    result = ""
                };
                return Unauthorized(tokenNotProvided);
            }

            bool isValidToken = await ValidateToken(token);

            if (!isValidToken)
            {
                var tokenNotValid = new
                {
                    success = false,
                    message = "Internal server error",
                    status = 401,
                    detail = "Your session has expired",
                    result = ""
                };
                return Unauthorized(tokenNotValid);
            }

            return null;
        }

        public Task<bool> ValidateToken(string token)
        {
            try
            {
                var tokenHandler = new JwtSecurityTokenHandler();
                var key = Encoding.ASCII.GetBytes(secretKey);

                tokenHandler.ValidateToken(token, new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(key),
                    ValidateIssuer = true,
                    ValidIssuer = issuer,
                    ValidateAudience = true,
                    ValidAudience = audience,
                    ClockSkew = TimeSpan.Zero
                }, out SecurityToken validatedToken);

                return Task.FromResult(true);
            }
            catch (Exception)
            {
                return Task.FromResult(false);
            }
        }
    }
}
