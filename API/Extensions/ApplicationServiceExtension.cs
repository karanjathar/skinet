using API.Errors;
using API.Helpers;
using Core.Interfaces;
using Infrastructure.Data;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.Extensions
{
    public static class ApplicationServiceExtension
    {

        public static IServiceCollection AddApplicationServices(this ServiceCollection services)
        {

            services.AddAutoMapper(typeof(MappingProfiles));
            services.AddScoped<IProductRepository, ProductRepository>();
            services.AddApplicationServices();
            services.Configure<ApiBehaviorOptions>(options =>
            {
                options.InvalidModelStateResponseFactory = actionContext =>
                {
                    var errors = actionContext.ModelState
                        .Where(e => e.Value.Errors.Count > 0)
                        .SelectMany(x => x.Value.Errors)
                        .Select(x => x.ErrorMessage).ToArray();

                    var errorResponse = new ApiValidationErrorResponse
                    {

                        Errors = errors
                    };
                    return new BadRequestObjectResult(errorResponse);
                };

            });

            return services;
        }
    }
}
