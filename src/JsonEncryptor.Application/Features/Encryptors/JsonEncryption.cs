using JsonEncryptor.Application.Abstractions;
using JsonEncryptor.Application.Common.Extensions;

namespace JsonEncryptor.Application.Features.Encryptors;

public class JsonEncryption : IJsonEncryption
{
    private readonly IJsonService _jsonService;
    private readonly IAesEncryptionService _aesEncryptionService;

    public JsonEncryption(
        IJsonService jsonService,
        IAesEncryptionService aesEncryptionService)
    {
        _jsonService = jsonService;
        _aesEncryptionService = aesEncryptionService;
    }

    /// <summary>
    /// Encrypts the specified JSON string using AES encryption.
    /// </summary>
    /// <param name="json">The JSON string to encrypt.</param>
    /// <param name="keysToEncrypt">The list of keys whose corresponding values should be encrypted.</param>
    /// <param name="aesKey">The AES encryption key.</param>
    /// <param name="aesKeySize">The size of the AES encryption key.</param>
    /// <param name="iv">The initialization vector (IV) for AES encryption.</param>
    /// <returns>The encrypted JSON string.</returns>
    public string Encrypt(string json, List<string> keysToEncrypt, string aesKey, int aesKeySize, string iv)
    {
        if (string.IsNullOrEmpty(json))
        {
            return string.Empty;
        }

        if (keysToEncrypt is null || keysToEncrypt.Count == 0)
        {
            return json;
        }

        string encryptedJson = json;

        foreach (var key in keysToEncrypt)
        {
            var value = _jsonService.GetValueFromKey<string>(json, key);

            if (value is null)
            {
                continue;
            }

            var encryptedValue = _aesEncryptionService.Encrypt(value, aesKey, aesKeySize, iv);

            encryptedJson = _jsonService.ReplaceValue(encryptedJson, key, encryptedValue)!;
        }

        return encryptedJson;
    }

    /// <summary>
    /// Decrypts the specified JSON string using AES decryption.
    /// </summary>
    /// <param name="json">The JSON string to decrypt.</param>
    /// <param name="keysToDecrypt">The list of keys whose corresponding values should be decrypted.</param>
    /// <param name="aesKey">The AES encryption key.</param>
    /// <param name="aesKeySize">The size of the AES encryption key.</param>
    /// <param name="iv">The initialization vector (IV) for AES encryption.</param>
    /// <returns>The decrypted JSON string.</returns>
    public string Decrypt(string json, List<string> keysToDecrypt, string aesKey, int aesKeySize, string iv)
    {
        if (string.IsNullOrEmpty(json))
        {
            return string.Empty;
        }

        if (keysToDecrypt is null || keysToDecrypt.Count == 0)
        {
            return json;
        }


        string decryptedJson = json;

        foreach (var key in keysToDecrypt)
        {
            var value = _jsonService.GetValueFromKey<string>(json, key);

            if (value is null)
            {
                continue;
            }

            var decryptedValue = _aesEncryptionService.Decrypt(value, aesKey, aesKeySize, iv).ToInsecureString();

            if (decryptedValue is not null)
            {
                decryptedJson = _jsonService.ReplaceValue(decryptedJson, key, decryptedValue)!;
            }
        }

        return decryptedJson;
    }
}
