using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TheHomingPigeon.Data.Interface;
using TheHomingPigeon.Middlewares;
using TheHomingPigeon.Model;

namespace TheHomingPigeon.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class NoticiasController : ControllerBase
    {
        private readonly INoticiasRepository _noticasRepository;
        private readonly JwtValidateController _jwtValidateController;

        public NoticiasController(INoticiasRepository noticasRepository, JwtValidateController jwtValidateController)
        {
            _noticasRepository = noticasRepository;
            _jwtValidateController = jwtValidateController;
        }

        [HttpPost("CreateNews")]
        public async Task<IActionResult> CreateNews([FromBody] Noticias noticias)
        {
            if (noticias == null)
            {
                return BadRequest();
            }
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            //var token = HttpContext.Request.Headers["Token"].FirstOrDefault()?.Split(" ").Last();

            //var result = await _jwtValidateController.HandleTokenValidation(token);

            //if (result != null)
            //{
            //    return result;
            //}

            await _noticasRepository.InsertNews(noticias);

            var response = new
            {
                success = true,
                status = 200,
                message = "Noticia creada exitosamente",
                result = true
            };

            return Created("Created", response);
        }
    }
}
