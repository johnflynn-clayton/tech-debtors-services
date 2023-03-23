using Cmh.Vmf.Infrastructure.HealthChecks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System;

namespace CMH.MobileHomeTracker.Api
{
    public static class Extensions
    {
        public static bool IsProduction(this IWebHostEnvironment webHostEnvironment)
        {
            // we usually use "PROD"
            return webHostEnvironment.IsEnvironment("PROD")
                // but add this in case it ever changes to the full word
                || webHostEnvironment.IsEnvironment(Environments.Production);
        }

        public static IServiceCollection AddSwagger(this IServiceCollection services, bool isProduction = true)
        {
            return services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo
                {
                    Title = "Template Domain",
                    Description = "Description of the Template Domain",
                    Version = Assembly.GetExecutingAssembly().GetCustomAttribute<AssemblyInformationalVersionAttribute>().InformationalVersion
                });

                var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
                var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);

                c.IncludeXmlComments(xmlPath);
                c.DocumentFilter<SwaggerLivenessDocumentFilter>();
                c.DocumentFilter<SwaggerReadinessDocumentFilter>();

                // Don't include configuration of tokens in production
                if (!isProduction)
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
        }
    }
}
