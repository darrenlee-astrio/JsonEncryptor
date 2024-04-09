using Application.Services.Abstractions;
using Application.Services.Encryption.Models;
using System.Security;
using System.Security.Cryptography;

namespace Application.Services.Encryption;

public class AesEncryptionService : EncryptionBase, IAesEncryptionService
{
    /// <summary>
    /// Encrypts a plain text using AES encryption with the specified key and key size.
    /// The Initialization Vector (IV) will be generated randomly.
    /// </summary>
    /// <param name="plainText">The plaintext string to encrypt.</param>
    /// <param name="key">The encryption key.</param>
    /// <param name="keySize">The size of the encryption key (in bits).</param>
    /// <param name="iv">The Initialization Vector (IV) to use for encryption.</param>
    /// <returns>The Base64-encoded representation of the encrypted data.</returns>
    public string Encrypt(string plainText, string key, int keySize, out string iv)
    {
        if (string.IsNullOrEmpty(plainText))
        {
            throw new ArgumentNullException(nameof(plainText));
        }

        using (Aes aesAlgorithm = Aes.Create())
        {
            aesAlgorithm.KeySize = keySize;
            aesAlgorithm.Key = Convert.FromBase64String(key);
            aesAlgorithm.GenerateIV();
            iv = Convert.ToBase64String(aesAlgorithm.IV);

            return EncryptInternal(plainText, aesAlgorithm);
        }
    }

    /// <summary>
    /// Encrypts the specified plaintext using AES encryption with the given key size.
    /// The Key and Initialization Vector (IV) will be generated randomly.
    /// </summary>
    /// <param name="plainText">The plaintext to encrypt.</param>
    /// <param name="keySize">The size of the encryption key (in bits).</param>
    /// <param name="key">Output parameter to store the generated encryption key as a Base64-encoded string.</param>
    /// <param name="iv">Output parameter to store the generated Initialization Vector (IV) as a Base64-encoded string.</param>
    /// <returns>The Base64-encoded representation of the encrypted data.</returns>
    public string Encrypt(string plainText, int keySize, out string key, out string iv)
    {
        if (string.IsNullOrEmpty(plainText))
        {
            throw new ArgumentNullException(nameof(plainText));
        }

        using (Aes aesAlgorithm = Aes.Create())
        {
            aesAlgorithm.KeySize = keySize;
            aesAlgorithm.GenerateKey();
            aesAlgorithm.GenerateIV();

            key = Convert.ToBase64String(aesAlgorithm.Key);
            iv = Convert.ToBase64String(aesAlgorithm.IV);

            return EncryptInternal(plainText, aesAlgorithm);
        }
    }

    /// <summary>
    /// Encrypts the specified plaintext using AES encryption with the given key and IV.
    /// </summary>
    /// <param name="plainText">The plaintext to encrypt.</param>
    /// <param name="key">The encryption key as a Base64-encoded string.</param>
    /// <param name="keySize">The size of the encryption key (in bits).</param>
    /// <param name="iv">The Initialization Vector (IV) as a Base64-encoded string.</param>
    /// <returns>The Base64-encoded representation of the encrypted data.</returns>
    public string Encrypt(string plainText, string key, int keySize, string iv)
    {
        if (string.IsNullOrEmpty(plainText))
        {
            throw new ArgumentNullException(nameof(plainText));
        }

        using (Aes aesAlgorithm = Aes.Create())
        {
            aesAlgorithm.KeySize = keySize;
            aesAlgorithm.Key = Convert.FromBase64String(key);
            aesAlgorithm.IV = Convert.FromBase64String(iv);

            return EncryptInternal(plainText, aesAlgorithm);
        }
    }

    /// <summary>
    /// Decrypts the specified encrypted data using AES encryption with the given key and IV.
    /// </summary>
    /// <param name="encryptedData">The encrypted data to decrypt, provided as a Base64-encoded string.</param>
    /// <param name="key">The encryption key as a Base64-encoded string.</param>
    /// <param name="keySize">The size of the encryption key (in bits).</param>
    /// <param name="iv">The Initialization Vector (IV) as a Base64-encoded string.</param>
    /// <returns>The decrypted data as a SecureString.</returns>
    public SecureString Decrypt(string encryptedData, string key, int keySize, string iv)
    {
        if (string.IsNullOrEmpty(encryptedData))
        {
            throw new ArgumentNullException(nameof(encryptedData));
        }

        byte[] encryptedDataInBytes = Convert.FromBase64String(encryptedData);

        using (Aes aesAlgorithm = Aes.Create())
        {
            aesAlgorithm.KeySize = keySize;
            aesAlgorithm.Key = Convert.FromBase64String(key);
            aesAlgorithm.IV = Convert.FromBase64String(iv);

            using (ICryptoTransform decryptor = aesAlgorithm.CreateDecryptor())
            using (MemoryStream msDecrypt = new MemoryStream(encryptedDataInBytes))
            using (CryptoStream csDecrypt = new CryptoStream(msDecrypt, decryptor, CryptoStreamMode.Read))
            using (StreamReader srDecrypt = new StreamReader(csDecrypt))
            {
                return base.CreateSecureString(srDecrypt.ReadToEnd());
            }
        }
    }

    private string EncryptInternal(string plainText, Aes aesAlgorithm)
    {
        using (ICryptoTransform encryptor = aesAlgorithm.CreateEncryptor())
        using (MemoryStream msEncrypt = new MemoryStream())
        using (CryptoStream csEncrypt = new CryptoStream(msEncrypt, encryptor, CryptoStreamMode.Write))
        using (StreamWriter swEncrypt = new StreamWriter(csEncrypt))
        {
            swEncrypt.Write(plainText);
            swEncrypt.Close();
            return Convert.ToBase64String(msEncrypt.ToArray());
        }
    }
}