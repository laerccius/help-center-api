using System;
using help_center_api.Models;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Options;
using Microsoft.OpenApi.Models;
using MongoDB.Bson.Serialization;
using SupportTicketsApi.Services;

namespace help_center_api
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
            services.AddControllers().AddNewtonsoftJson(options => options.UseCamelCasing(true));

            ConfigureMongoServices(services);

            CondigureDomainService(services);

            ConfigureSwagger(services);
        }

        private void ConfigureMongoServices(IServiceCollection services)
        {

            services.AddSingleton<IHelpCenterDatabaseSettings>(sp =>
                sp.GetRequiredService<IOptions<HelpCenterDatabaseSettings>>().Value);

            services.Configure<HelpCenterDatabaseSettings>(
                Configuration.GetSection(nameof(HelpCenterDatabaseSettings)));
        }

        private void CondigureDomainService(IServiceCollection services)
        {
            services.AddSingleton<SupportTicketService>();
        }

        private void ConfigureSwagger(IServiceCollection services)
        {
            services.AddSwaggerGen(c =>
          {
              c.SwaggerDoc("v1", new OpenApiInfo { Title = "Help Center", Version = "v1" });
          });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            //app.UseHttpsRedirection();
            app.UseCors(x => x
                           .AllowAnyOrigin()
                           .AllowAnyMethod()
                           .AllowAnyHeader());
            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });

            BsonClassMap.RegisterClassMap<SupportTicket>(map =>
        {
            map.AutoMap();
            map.SetIgnoreExtraElements(true);
            map.MapIdMember(x => x.Id);
            map.MapMember(x => x.UserName).SetIsRequired(true).SetElementName("userName");
            map.MapMember(x => x.Description).SetIsRequired(true).SetElementName("description");

        });
            app.UseSwagger();
            app.UseSwaggerUI(c =>
           {
               c.SwaggerEndpoint("", "My API V1");
           });
        }
    }
}
