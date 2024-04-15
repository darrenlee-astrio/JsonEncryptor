using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using WinForm.Forms;

namespace WinForm.Setup;

public static class DependencyInjection
{
    public static IServiceCollection AddWinForm(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddSingleton(configuration);
        services.AddScoped<MainForm>();

        return services;
    }
}
