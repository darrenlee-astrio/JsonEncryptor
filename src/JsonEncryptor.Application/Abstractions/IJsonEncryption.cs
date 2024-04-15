namespace JsonEncryptor.Application.Abstractions
{
    public interface IJsonEncryption
    {
        string Decrypt(string json, List<string> keysToEncrypt, string aesKey, int aesKeySize, string iv);
        string Encrypt(string json, List<string> keysToDecrypt, string aesKey, int aesKeySize, string iv);
    }
}