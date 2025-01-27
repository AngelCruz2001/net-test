using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Configuration;
using Microsoft.EntityFrameworkCore;
using MyApi.Data;
using MyApi.Repositories.Interfaces;
using MyApi.Repositories;
using MyApi.Middleware;
namespace MyApi {
    public class Startup {
        private readonly IConfiguration _configuration;

        public Startup(IConfiguration configuration) {
            _configuration = configuration;
        }

        public void ConfigureServices(IServiceCollection services) {
            services.AddControllers();

            services.AddDbContext<AppDbContext>(options =>
                options.UseSqlServer(_configuration.GetConnectionString("DefaultConnection"))
            );

            // Dependecy Injection
            services.AddScoped<IProductRepository, ProductRepository>();
        }

        public void Configure(IApplicationBuilder app) {
            app.UseExceptionHandler("/error");

            // Routing
            app.UseRouting();

            // Middleware
            app.UseMiddleware<LoggingMiddleware>();

            // Controllers
            app.UseEndpoints(endpoints => {
                endpoints.MapControllers();
            });
        }
    }
};
