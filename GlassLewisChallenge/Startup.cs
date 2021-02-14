using GlassLewisChallenge.Authentication;
using GlassLewisChallenge.Filters;
using GlassLewisChallenge.Infraestructure;
using GlassLewisChallenge.Interfaces;
using GlassLewisChallenge.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Diagnostics.HealthChecks;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Linq;
using System.Text;

namespace GlassLewisChallenge
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddTransient<IRepository, Repository>();
            services.AddTransient<ICompanyService, CompanyService>();
            services.AddTransient<ITokenManager, TokenManager>();
            services.AddTransient<IValidatorService, ValidatorService>();

            var server = Configuration["DBServer"] ?? "localhost";
            var port = Configuration["DBPort"] ?? "1433";
            var user = Configuration["DBUser"] ?? "SA";
            var password = Configuration["DBPassword"] ?? "Pa55w0rd2019";
            var database = Configuration["Database"] ?? "Colours";
            var connectionString = $"Server={server},{port};Initial Catalog={database};User Id={user};Password={password}";

            services.AddHealthChecks()
                .AddCheck("Company-check", new SqlHealthCheck(connectionString), HealthStatus.Unhealthy, new string[] { "companydb" });

            services.AddDbContext<CompanyContext>(options =>
                 options.UseSqlServer(connectionString,
                 opt =>
                 {
                     opt.EnableRetryOnFailure(maxRetryCount: 10, maxRetryDelay: TimeSpan.FromSeconds(10),
                       errorNumbersToAdd: null); 
                     opt.MigrationsAssembly("GlassLewisChallenge");
                 }).UseQueryTrackingBehavior(QueryTrackingBehavior.NoTracking)
        );
            var appSettingsSection = Configuration.GetSection("AppSettings");
            services.Configure<AppSettings>(appSettingsSection);
            var appSettings = appSettingsSection.Get<AppSettings>();

           /* services.AddAuthentication(x =>
            {
                x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                x.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
                x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
            .AddJwtBearer(x =>
            {
                x.SaveToken = true;
                x.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(appSettings.Secret)),
                    ValidateIssuer = false,
                    ValidateAudience = false,
                    RequireExpirationTime = false,
                    ValidateLifetime = true
                };
            });*/

            services.AddMvc(options => options.Filters.Add(new HttpResponseExceptionFilter())).SetCompatibilityVersion(CompatibilityVersion.Version_2_2);

            services.AddSwaggerDocument
               (s => s.Title = "GlassLewis API Documentation!!!"

               );

        }

        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseExceptionHandler("/error-local-development");
            }
            else
            {
                app.UseExceptionHandler("/error");
            }

            app.UseCors(x => x
                .AllowAnyOrigin()
                .AllowAnyMethod()
                .AllowAnyHeader());

            app.UseAuthentication();

            app.UseMvc();

            app.UseSwagger(config => config.PostProcess = (document, request) =>
            {
                if (request.Headers.ContainsKey("X-External-Host"))
                {
                    document.Host = request.Headers["X-External-Host"].First().ToString();

                    document.BasePath = request.Headers["X-External-Path"].First().ToString();
                }
            });

            app.UseSwaggerUi3(config => config.TransformToExternalPath = (internalUiRoute, request) =>
            {
                var externalPath = request.Headers.ContainsKey("X-External-Path") ? request.Headers["X-External-Path"].First().ToString() : "";
                return externalPath + internalUiRoute;
            });


            PrepDB.PrepPopulation(app);
        }
    }
}
