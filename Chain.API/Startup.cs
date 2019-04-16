using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using Chain.Domain.Context.Handlers;
using Chain.Domain.Context.Repositories;
using Chain.Domain.Context.Services;
using Chain.Infra.Context.DataContexts;
using Chain.Infra.Context.Repositories;
using Chain.Infra.Context.Services;
using Chain.Shared;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using NLog;
using Swashbuckle.AspNetCore.Swagger;

namespace Chain.API
{
    public class Startup
    {
        private readonly IHostingEnvironment _hostingEnvironment;

        public static IConfiguration Configuration { get; set; }

        public Startup(IHostingEnvironment hostingEnvironment)
        {
            LogManager.LoadConfiguration(System.String.Concat(Directory.GetCurrentDirectory(), "/nlog.config"));
            _hostingEnvironment = hostingEnvironment;
        }

        public void ConfigureServices(IServiceCollection services)
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile(_hostingEnvironment.IsProduction() ? "appsettings.json" : $"appsettings.{_hostingEnvironment.EnvironmentName}.json");

            Configuration = builder.Build();

            services.AddMvc();
            services.AddResponseCompression();

            services.AddScoped<ChainContext, ChainContext>();

            services.AddTransient<FlowCommandHandler, FlowCommandHandler>();
            services.AddTransient<GroupCommandHandler, GroupCommandHandler>();
            services.AddTransient<ObjectCommandHandler, ObjectCommandHandler>();
            services.AddTransient<ObjectStatusCommandHandler, ObjectStatusCommandHandler>();
            services.AddTransient<ObjectTypeCommandHandler, ObjectTypeCommandHandler>();
            services.AddTransient<UserCommandHandler, UserCommandHandler>();

            services.AddTransient<IFlowRepository, FlowRepository>();
            services.AddTransient<IGroupRepository, GroupRepository>();
            services.AddTransient<IObjectRepository, ObjectRepository>();
            services.AddTransient<IObjectStatusRepository, ObjectStatusRepository>();
            services.AddTransient<IObjectTypeRepository, ObjectTypeRepository>();
            services.AddTransient<IUserRepository, UserRepository>();

            services.AddTransient<ILoggingService, LoggingService>();

            services.AddDistributedMemoryCache();

            services.AddSwaggerGen(x =>
            {
                x.SwaggerDoc("v1", new Info
                {
                    Version = "v1",
                    Title = "Chain API",
                    Description = "API for Chain Project",
                    TermsOfService = "None",
                    Contact = new Contact
                    {
                        Name = "Fernando Velloso Borges de Mélo Gomes",
                        Email = "fernandovbmgomes@hotmail.com"
                    }
                });

                var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
                var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
                x.IncludeXmlComments(xmlPath);

                x.DescribeAllEnumsAsStrings();
            });

            Settings.ConnectionString = $"{Configuration["connectionString"]}";
            Settings.DetailedLog = bool.Parse($"{Configuration["detailedLog"]}");
        }

        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
                app.UseDeveloperExceptionPage();

            app.UseMvc();
            app.UseResponseCompression();

            app.UseSwagger();

            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "Template - V1");
                c.RoutePrefix = string.Empty;
            });
        }
    }
}
