using Newtonsoft.Json;

namespace Application.Abstractions
{
    public interface IJsonService
    {
        bool IsJsonValid(string input);
        string PrettifyJson(string input);
        string? GetValueFromKey(string json, string key, Formatting formatting = Formatting.None);
        string? ReplaceValue(string json, string key, string newValue, Formatting formatting = Formatting.Indented);
    }
}