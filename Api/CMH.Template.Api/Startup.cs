using Cmh.Vmf.Infrastructure.AspNet.Extensions;
using Cmh.Vmf.Infrastructure.Data;
using Cmh.Vmf.Infrastructure.Domain;
using Cmh.Vmf.Infrastructure.HealthChecks;
using CMH.MobileHomeTracker.Data;
using CMH.MobileHomeTracker.Domain.Services;
using CMH.MobileHomeTracker.Infrastructure.Validation;
using CMH.VMF.Logging;
using CMH.VMF.Security.JwtCore;
using FluentValidation;
using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Diagnostics.HealthChecks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Diagnostics.HealthChecks;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerUI;
using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;

namespace CMH.MobileHomeTracker.Api
{
    public class Startup
    {
        public Startup(IConfiguration configuration, IWebHostEnvironment webHostEnvironment)
        {
            Configuration = configuration;
            WebHostEnvironment = webHostEnvironment;
        }

        public IConfiguration Configuration { get; }
        public IWebHostEnvironment WebHostEnvironment { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            // Add health check services to the container
            services.AddHealthChecks()
                // Add an api check to check that we can connect to them by hitting their /health/live endpoint
                //  .AddApi("someother_api", TimeSpan.FromSeconds(1), HealthStatus.Unhealthy, Configuration["ClientBaseUri:SomeOtherApi"]);
                // Add a SqlConnection health check to check that we can connect to a database by connecting and executing a simple query
                .AddSqlConnection("MobileHomeTracker", TimeSpan.FromSeconds(1), HealthStatus.Unhealthy, Configuration["ConnectionStrings:MobileHomeTracker"]);

            services
                .AddOptions()
                .Configure<AuthorizationSettings>(r => Configuration.GetSection(nameof(AuthorizationSettings)).Bind(r));

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo
                {
                    Title = "CMH.MobileHomeTracker Domain",
                    Description = "Domain used for Tracking Mobile Homes",
                    Version = Assembly.GetExecutingAssembly().GetCustomAttribute<AssemblyInformationalVersionAttribute>().InformationalVersion
                });

                var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
                var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);

                c.IncludeXmlComments(xmlPath);
                c.DocumentFilter<SwaggerLivenessDocumentFilter>();
                c.DocumentFilter<SwaggerReadinessDocumentFilter>();

                // Don't include configuration of tokens in production
                if (!WebHostEnvironment.IsProduction())
                {
                    // Add ability to paste in a token and use it in swagger
                    // Note: User must prefix token manually: "Bearer {{token}}"
                    c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme()
                    {
                        In = ParameterLocation.Header,
                        Scheme = "Bearer",
                        BearerFormat = "JWT",
                        Description = "Add your token with the Bearer prefix",
                        Name = "Authorization",
                        Type = SecuritySchemeType.ApiKey
                    });

                    c.AddSecurityRequirement(new OpenApiSecurityRequirement()
                    {
                        {
                            new OpenApiSecurityScheme
                            {
                                Reference = new OpenApiReference
                                {
                                    Type = ReferenceType.SecurityScheme,
                                    Id = "Bearer"
                                },
                                Scheme = "oauth2",
                                Name = "Bearer",
                                In = ParameterLocation.Header,
                            },
                            new List<string>()
                        }
                    });
                }
            });

            services.AddControllers();

            services.AddMvc()
                .AddFluentValidation(fv =>
                {
                    fv.ValidatorOptions.CascadeMode = CascadeMode.Stop;
                    fv.RegisterValidatorsFromAssemblyContaining<HomeValidator>(lifetime: ServiceLifetime.Singleton);
                });

            services.AddDomainServicesFromAssemblyContaining<HomeService>();
            services.AddRepositoriesFromAssemblyContaining<HomeRepository>();
            services.AddMappersFromAssemblyContaining<Infrastructure.Mapping.HomeMapper>();
            services.AddMappersFromAssemblyContaining<Data.Mapping.HomeMapper>();

            services.AddSingleton<IIdGenerator<Guid>, GuidIdGenerator>();
            services.AddDbConnectionFactory(Configuration.GetConnectionString("MobileHomeTracker"));
            services.AddAdaptersFromAssemblyContaining<Adapters.GitHubAdapter>();

            services.AddLogging();

            services.AddJwtAuthHelperService(Configuration);
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
            app.UseAuthentication();
            app.UseAuthorization();

            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                //Using a relative path to allow Swagger to work in both IIS Express and Local IIS
                c.SwaggerEndpoint("v1/swagger.json", "Mobile Home Tracker Domain V1");

                // Disable "Try it out" in production
                if (WebHostEnvironment.IsProduction())
                {
                    c.SupportedSubmitMethods(new SubmitMethod[] { });
                }
            });

            // Custom VMF Middleware to be executed on controller actions. Ignore healthcheck endpoints.
            // Keep this as the first middleware used to preserve activity and exception handling behavior
            app.UseWhen(x => !x.Request.Path.StartsWithSegments("/health"), mvcBuilder =>
            {
                mvcBuilder.UseActivityTraceLogging();
                mvcBuilder.UseExceptionMiddleware((ex, details, logger) =>
                {
                    switch (ex)
                    {
                        case KeyNotFoundException _:
                            details.StatusCode = StatusCodes.Status400BadRequest;
                            break;
                        case System.Data.SqlClient.SqlException _:
                            details.StatusCode = StatusCodes.Status400BadRequest;
                            details.Message = ex.Message;
                            break;
                    }
                });
            });

            // Add any additional middleware after this point

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();

                endpoints.MapHealthChecks("/health/ready", new HealthCheckOptions()
                {
                    // Grab only the checks registered as readiness checks
                    Predicate = check => check.IsReadinessCheck(),
                    ResponseWriter = JsonResponseWriter.WriteReadinessResponse
                });

                endpoints.MapHealthChecks("/health/live", new HealthCheckOptions()
                {
                    // Exclude all checks and return a 200-Ok.
                    Predicate = _ => false,
                    ResponseWriter = JsonResponseWriter.WriteLivenessResponse
                });
            });
        }
    }
}
