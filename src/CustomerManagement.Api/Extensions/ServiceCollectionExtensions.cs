﻿using CustomerManagement.Api.Support;
using CustomerManagement.Api.Swagger;
using CustomerManagement.Common.Extensions;
using CustomerManagement.Dto.Support;
using Microsoft.AspNetCore.Mvc;

namespace CustomerManagement.Api.Extensions
{
    /// <summary>
    ///     Helper extensions for populating Service Collection
    /// </summary>
    public static class ServiceCollectionExtensions
    {
        /// <summary>
        ///     Add swagger support and configure appropriately
        /// </summary>
        /// <param name="serviceCollection"></param>
        public static void AddSwaggerAndConfig(this IServiceCollection serviceCollection)
        {
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            serviceCollection.AddEndpointsApiExplorer();
            serviceCollection.AddSwaggerGen(options =>
            {
                options.OperationFilter<SwaggerDefaultValues>();
                options.IncludeXmlComments(ApiAssemblyReference.Assembly.XmlCommentsFilePath());
                options.IncludeXmlComments(DtoAssemblyReference.Assembly.XmlCommentsFilePath());
            });
        }

        /// <summary>
        ///     Add any health checks required
        /// </summary>
        /// <param name="serviceCollection"></param>
        public static void AddHealthChecks(this IServiceCollection serviceCollection)
        {
        }

        public static void AddVersioning(this IServiceCollection services)
        {
            services.AddApiVersioning(options =>
            {
                options.DefaultApiVersion = new ApiVersion(1, 0);
                options.AssumeDefaultVersionWhenUnspecified = true;
                options.ReportApiVersions = true;
            });

            services.AddVersionedApiExplorer(options =>
            {
                options.GroupNameFormat = "'v'VVV";
                options.SubstituteApiVersionInUrl = true;
            });
        }
    }
}