
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Noticias.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        [HttpGet("public-access")]
        [AllowAnonymous] // Permite acceso público
        public IActionResult PublicAccess()
        {
            return Ok(new { Message = "Public access" });
        }

        [HttpGet("user-access")]
        [Authorize] // Requiere autenticación (cualquier usuario autenticado)
        public IActionResult UserAccess()
        {
            return Ok(new { Message = "User Authentication Successfully" });
        }
    }
}