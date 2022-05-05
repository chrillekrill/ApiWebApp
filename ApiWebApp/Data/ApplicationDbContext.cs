using Microsoft.EntityFrameworkCore;

namespace ApiWebApp.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
                
        }

        public DbSet<Advert> Adverts { get; set; }
    }
}
