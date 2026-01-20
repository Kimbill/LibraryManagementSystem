using Microsoft.OpenApi.Models;

namespace LawPavillion.LibraryManagement.Api.Extensions
{
    public static class SwaggerrExtension
    {
        public static void AddSwaggerConfig(this IServiceCollection services)
        {
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo
                {
                    Title = "LawPavillionLibraryManagement",
                    Version = "v1"
                });

                // This for locking the API with roles inside the controller  
                c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme()
                {
                    Name = "Authorization",
                    Type = SecuritySchemeType.ApiKey,
                    Scheme = "Bearer",
                    BearerFormat = "JWT",
                    In = ParameterLocation.Header,
                    Description = "Enter 'Bearer' [space] and then your valid token in the input below.\r\n\rExample: \"Bearer ioqnqf8uqnwifqiwunwfudifijdfdlkjsdnfajldjnfj"
                });
                c.AddSecurityRequirement(new OpenApiSecurityRequirement
                {
                       {
                           new OpenApiSecurityScheme
                           {
                               Reference = new OpenApiReference
                               {
                                   Type = ReferenceType.SecurityScheme,
                                   Id = "Bearer"
                               }
                           },
                           Array.Empty<string>()
                       }
                });
            });
        }
    }
}
