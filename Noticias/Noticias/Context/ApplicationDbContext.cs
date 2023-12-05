using Microsoft.EntityFrameworkCore;
using Noticias.Models;

namespace Noticias.Context
{

    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }

        public DbSet<Article> Articles { get; set; }
    }

}