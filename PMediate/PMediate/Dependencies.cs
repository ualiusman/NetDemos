using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models;
using PMediate.Data;
using PMediate.Features.Consults;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PMediate
{
    public static class Dependencies
    {
        public static void Configure(IServiceCollection services, IConfiguration configuration)
        {
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo {
                    Title = "PMediate",
                    Version = "v1",
                    Description = "",
                    TermsOfService = new Uri(""),
                    Contact = new OpenApiContact()
                    {
                        Email = "",
                        Name = "",
                    },
                    License = new OpenApiLicense()
                    {
                        Name = "MIT",
                        Url = new Uri("")
                    }
                });
                c.CustomSchemaIds(x => x.FullName);
            });

            services.AddCors(option => option.AddPolicy("CorsPolicy",

                builder => builder
                    .WithOrigins("http://localhost:4200")
                    .AllowAnyMethod()
                    .AllowAnyHeader()
                    .SetIsOriginAllowed(isOriginAllowed: _ => true)
                    .AllowCredentials()
            ));

            services.AddHttpContextAccessor();

            services.AddMediatR(typeof(GetConsults));
            services.AddScoped<IAppDbContext, AppDbContext>();

            services.AddDbContext<AppDbContext>(options =>
            {
                options.UseSqlServer(configuration["Data:DefaultConnection:ConnectionString"],
                        builder => builder.MigrationsAssembly("PMediate")
                            .EnableRetryOnFailure())
                    .UseLoggerFactory(AppDbContext.ConsoleLoggerFactory)
                    .EnableSensitiveDataLogging();
            });

            services.AddControllers();

        }
    }
}
