using Application.Services.Abstractions;
using Application.Services.Encryption;
using Application.Services.Json;

namespace Application.Features.JsonEncryptor;

public class JsonEncryptor
{
    private readonly IJsonService _jsonService;
    private readonly IAesEncryptionService _aesEncryptionService;

    public JsonEncryptor()
    {
        _jsonService = new JsonService();
        _aesEncryptionService = new AesEncryptionService();
    }

    public string Encrypt(string json, List<string> jsonKeys, int keySize, string? key = null, string? iv = null)
    {
        string encryptedJson = json;

        foreach (var jsonKey in jsonKeys)
        {
            // find each key, gets its value and encrypt

            // replace value with encrypted value
        }

        return encryptedJson;
    }

    public string Decrypt(string json, List<string> jsonKeys, string key, int keySize, string iv)
    {
        string decryptedJson = json;

        foreach (var jsonKey in jsonKeys)
        {
            // find each key, gets its value and decrypt

            // replace value with decrypted value
        }

        return decryptedJson; 
    }
}
