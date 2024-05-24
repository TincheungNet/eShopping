﻿using Catalog.Application.Handlers;
using Microsoft.AspNetCore.Diagnostics.HealthChecks;
using Microsoft.Extensions.Diagnostics.HealthChecks;
using Microsoft.OpenApi.Models;
using System.Reflection;
using System;
using Microsoft.Extensions.DependencyInjection;
using Amazon.Auth.AccessControlPolicy;
using SharpCompress.Archives;
using static System.Runtime.InteropServices.JavaScript.JSType;
using Catalog.Core.Repositories;
using Catalog.Infrastructure.Data;
using Catalog.Infrastructure.Repositories;
using HealthChecks.UI.Client;

namespace Catalog.API;

public class Startup
{
    public IConfiguration Configuration;
    public Startup(IConfiguration configuration)
    {
        Configuration= configuration;
    }

    public void ConfigureServices(IServiceCollection services)
    {
        services.AddControllers();
        services.AddApiVersioning();
        services.AddHealthChecks()
       .AddMongoDb(Configuration["DatabaseSettings:ConnectionString"], "Catalog  Mongo Db Health Check",
           HealthStatus.Degraded);

        services.AddSwaggerGen(c => { c.SwaggerDoc("v1", new OpenApiInfo { Title = "Catalog.API", Version = "v1" }); });
        //DI
        services.AddAutoMapper(typeof(Startup));        
        services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(typeof(CreateProductHandler).GetTypeInfo().Assembly));

        //services.AddScoped<ICorrelationIdGenerator, CorrelationIdGenerator>();
        services.AddScoped<ICatalogContext, CatalogContext>();
        services.AddScoped<IProductRepository, ProductRepository>();
        services.AddScoped<IBrandRepository, ProductRepository>();
        services.AddScoped<ITypesRepository, ProductRepository>();
        //services.AddControllers();

    }

    public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
    {
        if (env.IsDevelopment())
        {
            app.UseDeveloperExceptionPage();
            app.UseSwagger();
            app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "Catalog.API v1"));
        }

        app.UseRouting();
        app.UseStaticFiles();
        app.UseAuthentication();
        app.UseEndpoints(endpoints =>
        {
            endpoints.MapControllers();
            endpoints.MapHealthChecks("/health", new HealthCheckOptions()
            {
                Predicate = _ => true,
                ResponseWriter = UIResponseWriter.WriteHealthCheckUIResponse
            });

        });

    }
}