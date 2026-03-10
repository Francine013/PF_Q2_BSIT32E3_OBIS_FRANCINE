using Microsoft.EntityFrameworkCore;

namespace PokedexApi.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }

        // Notice that the context contains no DbSet properties.
        // This means the application does not know what tables exist.
        // The DbContext simply represents a database connection.
    }
}
