namespace Company.Example.Presentation.Api
{
    using Company.Example.Application.Internal.Exceptions;
    using Hellang.Middleware.ProblemDetails;
    using Microsoft.AspNetCore.Http;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Routing;
    using Microsoft.Extensions.DependencyInjection;
    using Microsoft.Extensions.Hosting;
    using Newtonsoft.Json;
    using Newtonsoft.Json.Serialization;
    using System.Reflection;

    public static class DependencyInjection
    {
        public static IServiceCollection AddPresentationConfiguration(
            this IServiceCollection services,
            IHostEnvironment environment)
        {
            services.AddAutoMapper(Assembly.GetExecutingAssembly());

            Action<RouteOptions> routeOptions = options => options.LowercaseUrls = true;

            Action<ProblemDetailsOptions> problemDetailsOptions = options => SetProblemDetailsOptions(options, environment);

            Action<MvcNewtonsoftJsonOptions> newtonsoftOptions = options =>
            {
                options.SerializerSettings.ContractResolver = new CamelCasePropertyNamesContractResolver();
                options.SerializerSettings.MissingMemberHandling = MissingMemberHandling.Ignore;
                options.SerializerSettings.NullValueHandling = NullValueHandling.Ignore;
                options.SerializerSettings.ReferenceLoopHandling = ReferenceLoopHandling.Ignore;
            };

            services
                .AddRouting(routeOptions)
                .AddProblemDetails(problemDetailsOptions)
                .AddControllers()
                .AddNewtonsoftJson(newtonsoftOptions);

            services.AddSwaggerGen();

            return services;
        }

        private static void SetProblemDetailsOptions(ProblemDetailsOptions options, IHostEnvironment enviroment)
        {
            Type[] knownExceptionTypes = new Type[] { typeof(ServiceValidationException) };

            options.IncludeExceptionDetails = (_, exception) =>
                enviroment.IsDevelopment() &&
                !knownExceptionTypes.Contains(exception.GetType());

            options.Map<ServiceValidationException>(exception =>
                new ValidationProblemDetails(exception.Errors)
                {
                    Title = exception.Title,
                    Detail = exception.Detail,
                    Status = StatusCodes.Status400BadRequest
                });
        }
    }
}