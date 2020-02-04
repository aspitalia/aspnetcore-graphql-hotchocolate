using HotChocolate;
using HotChocolate.AspNetCore;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using GraphQlAspNetCoreDemo.Models.Queries;
using GraphQlAspNetCoreDemo.Models.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System.Threading.Tasks;
using GraphQlAspNetCoreDemo.Models.Mutations;

namespace GraphQlAspNetCoreDemo
{
    public class Startup
    {
        public IConfiguration Configuration { get; }

        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }
        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            var schemaBuilder = SchemaBuilder
                                    .New()
                                    .AddQueryType<CatalogQueries>()
                                    .AddMutationType<CatalogMutations>()
                                    ;
            services.AddGraphQL(schemaBuilder);

            services.AddDbContextPool<CatalogDbContext>(options => {
                options.UseSqlite(Configuration.GetConnectionString("Default"));
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            app.UseDefaultFiles();
            app.UseStaticFiles();

            const string queryPath = "/api";
            app.UseGraphQL(queryPath);
            const string uiPath = "/playground";
            app.UsePlayground(queryPath, uiPath);
        }
    }
}