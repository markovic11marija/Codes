
using CodesApp.Infrastructure.Data;
using CodesApp.Infrastructure.Data.Ef;
using CodesApp.Service;
using CodesApp.Service.Interfaces;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using System;

namespace CodesApp.Api
{

    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {


            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "CodesApp.Api", Version = "v1" });
            });

            services.AddControllers();

            services.AddDbContext<CodesContext>(b => b.UseSqlServer(Configuration.GetConnectionString("CodesDbContext")));

            services.AddScoped(typeof(DbContext), typeof(CodesContext));
            services.AddScoped(typeof(IRepository<>), typeof(EntityFrameworkRepository<>));
            services.AddScoped(typeof(IUnitOfWork), typeof(DbContextUnitOfWork));

            services.AddTransient<ICodesService, CodesService>();


  

            services.AddCors();

            //services.AddCors(options=> { options.AddDefaultPolicy(new Microsoft.AspNetCore.Cors.Infrastructure.CorsPolicy()); });

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {

                app.UseDeveloperExceptionPage();
            }

            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "CodesApp.Api");
                c.RoutePrefix = String.Empty;
            });

            //   app.UseHttpsRedirection();

            app.UseRouting();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}

