using GlassLewisChallenge.Domain;
using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Linq;

namespace GlassLewisChallenge.Infraestructure
{
    public static class PrepDB
    {
        public static void PrepPopulation(IApplicationBuilder app)
        {
            using (var serviceScope = app.ApplicationServices.CreateScope())
            {
                SeedData(serviceScope.ServiceProvider.GetService<CompanyContext>());
            }

        }

        public static void SeedData(CompanyContext context)
        {
            Console.WriteLine("Appling Migrations...");
            if (context.Database.ProviderName != "Microsoft.EntityFrameworkCore.InMemory")
            {
                context.Database.Migrate();

                if (!context.Companies.Any())
                {
                    Console.WriteLine("Adding data - seeding...");
                    context.Companies.AddRange(
                        new Company() { Isin = "AR123", Exchange = "AAA", Name = "GlassLewis" },
                        new Company() { Isin = "OI123", Exchange = "BBB", Name = "JulioCompany" }
                    );

                    context.SaveChanges();
                }
                else
                {
                    Console.WriteLine("Already have data - not seeding.");
                }
            }
        }
    }
}
