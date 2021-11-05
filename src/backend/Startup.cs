using System;
using backend.Data;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace backend
{
    internal class Startup
    {
        private readonly IConfiguration _configuration;
        public Startup(IConfiguration configuration) => _configuration = configuration;

        public void ConfigureServices(IServiceCollection services)
        {
            Console.WriteLine(_configuration.GetConnectionString("ApplicationDbContext"));
            services.AddControllers()
                .AddNewtonsoftJson();
            services.AddDbContext<ApplicationDbContext>(builder => 
            {
                builder.UseSqlite(_configuration.GetConnectionString("ApplicationDbContext"));
            });
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            app.UseRouting();
            app.UseEndpoints(epConfig => 
            {
                epConfig.MapControllers();
            });
        }
    }
}