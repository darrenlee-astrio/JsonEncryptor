using JsonEncryptor.Application.Abstractions;
using JsonEncryptor.Application.Features.Encryptors;
using Microsoft.Extensions.DependencyInjection;

namespace JsonEncryptor.Application.Setup;

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

        services.AddSingleton<IJsonEncryption, JsonEncryption>();

        return services;
    }
}
