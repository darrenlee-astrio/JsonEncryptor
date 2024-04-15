using Application.Abstractions;
using Application.Features.Encryptors;
using Microsoft.Extensions.DependencyInjection;

namespace Application.Setup;

public static class DependencyInjection
{
    public static IServiceCollection AddApplication(this IServiceCollection services)
    {
        services.Scan(scan => scan
            .FromAssemblyOf<IApplicationMarker>()
            .AddClasses(classes => classes
                .Where(type => type.Name.EndsWith("Service")))
            .AsImplementedInterfaces()
            .WithSingletonLifetime());

        services.AddSingleton<IJsonEncryptor, JsonEncryptor>();

        return services;
    }
}
