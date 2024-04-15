using System.Security;

namespace Application.Abstractions
{
    public interface IAesEncryptionService
    {
        string GenerateKey(int keySize = 256);
        string GenerateIv();
        SecureString Decrypt(string encryptedData, string key, int keySize, string iv);
        string Encrypt(string plainText, int keySize, out string key, out string iv);
        string Encrypt(string plainText, string key, int keySize, out string iv);
        string Encrypt(string plainText, string key, int keySize, string iv);
    }
}