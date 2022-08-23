namespace Company.Example.Application
{
    using Company.Example.Application.Internal.Behaviors;
    using FluentValidation;
    using MediatR;
    using Microsoft.Extensions.DependencyInjection;
    using System.Reflection;

    public static class DependencyInjection
    {
        public static IServiceCollection AddApplicationConfiguration(
            this IServiceCollection services)
        {
            // Registers MediatR.
            services.AddMediatR(Assembly.GetExecutingAssembly());

            // Registers Fluent Validation.
            services.AddValidatorsFromAssemblies(new[] { Assembly.GetExecutingAssembly() }, includeInternalTypes: true);

            // Configuring our behavior.
            services.AddTransient(typeof(IPipelineBehavior<,>), typeof(ValidationBehavior<,>));

            // Configure AutoMapper.
            services.AddAutoMapper(Assembly.GetExecutingAssembly());

            return services;
        }
    }
}
