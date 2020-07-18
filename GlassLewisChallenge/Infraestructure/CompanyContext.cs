using GlassLewisChallange.Domain;
using Microsoft.EntityFrameworkCore;


namespace GlassLewisChallange.Infraestructure
{
    public class CompanyContext: DbContext
    {
        public CompanyContext(DbContextOptions<CompanyContext> options) : base(options)
        {

        }

        public DbSet<Company> Companies { get; set; }
    }
}
