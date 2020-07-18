using GlassLewisChallenge.Domain;
using Microsoft.EntityFrameworkCore;


namespace GlassLewisChallenge.Infraestructure
{
    public class CompanyContext: DbContext
    {
        public CompanyContext(DbContextOptions<CompanyContext> options) : base(options)
        {

        }

        public DbSet<Company> Companies { get; set; }
    }
}
