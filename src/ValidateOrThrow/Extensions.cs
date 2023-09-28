using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using System;

namespace ValidateOrThrow
{
    public static class Extensions
    {
        /// <summary>
        /// Adds a required options class to the service collection using the Options pattern.
        /// <exception cref="InvalidOperationException">The section was not found.</exception>
        /// <exception cref="OptionsValidationException">One or more validations failed.</exception>
        /// </summary>
        /// <param name="services">Extension of IServiceCollection.</param>
        /// <typeparam name="TOptions">Type of Option that will be added</typeparam>
        public static IServiceCollection AddOptionsOrThrow<TOptions>(
            this IServiceCollection services)
            where TOptions : class, IValidatedOption
        {
            var options = Activator.CreateInstance<TOptions>();
            
            if (options == null)
            {
                throw new InvalidOperationException($"Failed to create an instance of {nameof(TOptions)}");
            }
            
            services.AddOptions<TOptions>()
                .BindConfiguration(options.SectionName)
                .Validate<IConfiguration>((_, configuration)
                    => configuration
                        .GetRequiredSection(options.SectionName)
                        .Exists())
                .ValidateDataAnnotations()
                .ValidateOnStart();
            
            return services;
        }
    }
}