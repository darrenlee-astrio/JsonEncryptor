using System.Security;

namespace Application.Services.Abstractions
{
    public interface IAesEncryptionService
    {
        SecureString Decrypt(string encryptedData, string key, int keySize, string iv);
        string Encrypt(string plainText, int keySize, out string key, out string iv);
        string Encrypt(string plainText, string key, int keySize, out string iv);
        string Encrypt(string plainText, string key, int keySize, string iv);
    }
}