using Newtonsoft.Json;

namespace Application.Abstractions
{
    public interface IJsonService
    {
        bool IsJsonValid(string input);
        string PrettifyJson(string input);
        T? GetValueFromKey<T>(string json, string key);
        string? ReplaceValue(string json, string key, string newValue, Formatting formatting = Formatting.Indented);
    }
}