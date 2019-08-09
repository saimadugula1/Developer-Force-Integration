using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Logging.AzureAppServices;
using Microsoft.Extensions.Logging.Console;
using SalesForceLeadLogic;
using Swashbuckle.AspNetCore.Swagger;

namespace SalesForceLeadAPI
{
    public class Startup
    {
        public Startup(IHostingEnvironment environment)
        {
            var configBuilder = new ConfigurationBuilder()
                .SetBasePath(environment.ContentRootPath)
                .AddJsonFile("appSettings.json", optional: false, reloadOnChange: true)
                .AddJsonFile($"appSettings.{environment.EnvironmentName}.json", optional: true, reloadOnChange: true);

            configBuilder.AddEnvironmentVariables();
            Configuration = configBuilder.Build();
        }

        public static IConfiguration Configuration { get; private set; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {

            var config = Configuration;
            var origins = config.GetSection("Origins").Value;
            var authority = config.GetSection("Authority").Value;
            services.AddMvc();
            services.AddScoped<ISalesForceProcessor, SalesForceProcessor>();
            services.AddMvcCore()
               .AddAuthorization()
               .AddJsonFormatters();

            services.AddAuthentication("Bearer")
                            .AddIdentityServerAuthentication(options =>
                            {
                                options.Authority = authority;
                                options.RequireHttpsMetadata = false;
                                options.ApiName = "salesforceleadapi";
                            });
            services.AddCors(o => o.AddPolicy(
                "TaradelOriginsOnlyPolicy",
                builder =>
            {
                builder.WithOrigins(origins)
                            .AllowAnyMethod()
                             .AllowAnyHeader();
            }));
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc(
                    "v1",
                    new Info
                    {
                        Version = "v1",
                        Title = "SalesForce API",
                        Description = "API service to manage the Salesforce Objects",
                        TermsOfService = "None"
                    });
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory)
        {
            app.UseCors("TaradelOriginsOnlyPolicy");
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseAuthentication();
            app.UseMvc();
            loggerFactory.AddConsole(Configuration.GetSection("Logging"));
            loggerFactory.AddDebug();
            //start logging to the console
            var logger = loggerFactory.CreateLogger<ConsoleLogger>();
            logger.LogInformation("Executing Configure()");
            loggerFactory.AddAzureWebAppDiagnostics(
   new AzureAppServicesDiagnosticsSettings
   {
       OutputTemplate = "{Timestamp:yyyy-MM-dd HH:mm:ss zzz} [{Level}] {RequestId}-{SourceContext}: {Message}{NewLine}{Exception}"
   }
 );
            loggerFactory.AddApplicationInsights(app.ApplicationServices, LogLevel.Information);
            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "SalesForce");
            });
        }
    }
}