using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models;

namespace User.Infra
{


    public static class DependencyInjectionSwagger
    {

        public static IServiceCollection AddInfraStructureSwagger(this IServiceCollection services)
        {
            // No método ConfigureServices
            services.AddSwaggerGen(c =>
            {
                // Outras configurações do Swagger

                // Adicione a configuração de segurança aqui
                c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
                {
                    In = ParameterLocation.Header,
                    Description = "Copie e cole seu token aqui",
                    Name = "Authorization",
                    Type = SecuritySchemeType.ApiKey
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
            new string[] { }
        }
                });
            });
            return services;
        }


    }
}
