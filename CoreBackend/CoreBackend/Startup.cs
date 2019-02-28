using CoreBackend.Entity;
using CoreBackend.Filter;
using CoreBackend.Repositories;
using CoreBackend.Repositories.Impl;
using CoreBackend.Services;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Formatters;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json.Serialization;
using NLog.Extensions.Logging;
using Swashbuckle.AspNetCore.Swagger;
using System;
using System.IO;

namespace CoreBackend
{
    public class Startup
    {
        public static IConfiguration Configuration { get; private set; }

        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMvc(options => {
                options.Filters.Add<ExceptionFilter>();
                options.Filters.Add<ActionFilter>();
            }).AddJsonOptions(options => {
                //返回体大小写问题
                //options.SerializerSettings.ContractResolver = new DefaultContractResolver();
                if (options.SerializerSettings.ContractResolver is DefaultContractResolver resolver)
                {
                    resolver.NamingStrategy = null;
                }
            }).AddMvcOptions(options => {
                options.OutputFormatters.Add(new XmlDataContractSerializerOutputFormatter());
            });
#if DEBUG
            services.AddTransient<IMailService, LocalMailService>();
#else
            services.AddTransient<IMailService, CloudMailService>();
#endif
        var connectionString = Configuration["connectionStrings:productionInfoDbConnectionString"];
            services.AddDbContext<MyContext>(o => o.UseSqlServer(connectionString));

            services.AddScoped<IProductRepository, ProductRepository>();

            // Register the Swagger generator, defining 1 or more Swagger documents
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new Info { Title = "MyApi", Version = "v1" });
                // Set the comments path for the Swagger JSON and UI.
                var xmlPath = Path.Combine(AppContext.BaseDirectory, "CoreBackend.XML");
                c.IncludeXmlComments(xmlPath);
            });
        }

        // This method gets called by the runtime. Use this method to confiou bgure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory, MyContext myContext)
        {
            loggerFactory.AddNLog();

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler();
            }

            //swagger配置
            app.UseSwagger(c => { c.RouteTemplate = "swagger/{documentName}/swagger.json"; });
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "CoreBackend");
            });

            //myContext.EnsureSeedDataForContext();

            app.UseMvc();
        }
    }
}
