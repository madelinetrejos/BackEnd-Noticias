using Microsoft.AspNetCore.Mvc;
using Noticias.Context;
using Noticias.Models;

namespace Noticias.Controllers
{
  
        [ApiController]
        [Route("api/[controller]")]
        public class ArticlesController : ControllerBase
        {
            private readonly ApplicationDbContext _context;

            public ArticlesController(ApplicationDbContext context)
            {
                _context = context;
            }

            [HttpGet]
            public IActionResult Get()
            {
                var articles = _context.Articles.ToList();
                return Ok(articles);
            }

            [HttpPost]
            public IActionResult Post([FromBody] Article article)
            {
                _context.Articles.Add(article);
                _context.SaveChanges();
                return Ok(article);
            }

            // Implementa otros métodos para actualizar, eliminar, obtener por ID, etc.
        }

    }

