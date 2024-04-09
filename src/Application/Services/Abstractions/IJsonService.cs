namespace Application.Services.Abstractions
{
    public interface IJsonService
    {
        bool IsJsonValid(string input);
        string PrettifyJson(string input);
    }
}