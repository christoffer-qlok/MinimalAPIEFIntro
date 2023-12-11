using Microsoft.EntityFrameworkCore;
using MinimalAPIEFIntro.Models;

namespace MinimalAPIEFIntro.Data
{
    public class ApplicationContext : DbContext
    {
        public DbSet<Joke> Jokes { get; set; }

        public ApplicationContext(DbContextOptions<ApplicationContext> options) : base(options) { }
    }
}
