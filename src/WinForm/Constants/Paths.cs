namespace WinForm.Constants;

public static class Paths
{
    public const string ConfigurationDirectory = "Configurations";
    public const string LogDirectory = "Logs";

    public static string UiStateFilePath = Path.Combine(ConfigurationDirectory, "ui-state.json");
}
