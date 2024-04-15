using Application.Setup;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Serilog;
using Serilog.Formatting.Display;
using Serilog.Sinks.WinForms.Base;
using WinForm.Constants;
using WinForm.Forms;
using WinFormApplication = System.Windows.Forms.Application;

namespace WinForm.Setup;

internal static class Program
{
    [STAThread]
    static void Main()
    {
        ApplicationConfiguration.Initialize();

        var builder = new ConfigurationBuilder()
            .SetBasePath(Directory.GetCurrentDirectory())
            .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true);

        var configuration = builder.Build();

        ConfigureSerilog(configuration);

        var services = new ServiceCollection();

        var serviceProvider = services
            .ConfigureServices(configuration)
            .BuildServiceProvider();

        var form = serviceProvider.GetRequiredService<MainForm>();
        WinFormApplication.Run(form);
    }

    private static void ConfigureSerilog(IConfiguration configuration)
    {
        Log.Logger = new LoggerConfiguration()
            .ReadFrom.Configuration(configuration)
            .WriteToSimpleAndRichTextBox(new MessageTemplateTextFormatter(SerilogConstants.OutputTemplate))
            .CreateLogger();
    }

    private static IServiceCollection ConfigureServices(this IServiceCollection services, IConfiguration configuration)
    {
        services
            .AddWinForm(configuration)
            .AddApplication();

        return services;
    }
}