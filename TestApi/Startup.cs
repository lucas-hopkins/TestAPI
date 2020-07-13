using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using MovieStore.Repositories;
using System.Data;
using MySqlConnector;
using System.Data.SqlClient;

namespace MovieStore
{
    public class Startup
    {
        public IConfiguration Configuration { get; private set; }
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;  
        }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            var builder = new SqlConnectionStringBuilder(
                Configuration.GetConnectionString("DefaultConnection"));
                builder.Password = Configuration["DbPassword"];
            string _conn = builder.ConnectionString;
            services.AddControllers();
            services.AddTransient<IDbConnection>((sp) => new MySqlConnection(_conn));
            services.AddTransient<ActorRepository>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
