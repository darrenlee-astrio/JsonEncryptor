﻿using FluentAssertions;
using JsonEncryptor.Application.Abstractions;
using JsonEncryptor.Application.Features.Encryptors;
using JsonEncryptor.Application.Tests.Unit.Constants;
using NSubstitute;
using System.Security;

namespace JsonEncryptor.Application.Tests.Unit.Features.Encryptors;

public class JsonEncryptionTests
{
    private readonly IJsonEncryption _jsonEncryptor;
    private readonly IJsonService _jsonService;
    private readonly IAesEncryptionService _aesEncryptionService;
    private readonly string AesKey = AesConstants.Key;
    private readonly int AesKeySize = AesConstants.KeySize;
    private readonly string Iv = AesConstants.IV;

    public JsonEncryptionTests()
    {
        _jsonService = Substitute.For<IJsonService>();
        _aesEncryptionService = Substitute.For<IAesEncryptionService>();
        _jsonEncryptor = new JsonEncryption(_jsonService, _aesEncryptionService);
    }

    private SecureString GetSecureString(string value)
    {
        var secureString = new SecureString();
        foreach (char c in value)
        {
            secureString.AppendChar(c);
        }
        secureString.MakeReadOnly();
        return secureString;
    }

    [Fact]
    public void Encrypt_ShouldReturnEncryptedJson_WhenInputIsValid()
    {
        // Arrange
        var keyToEncrypt = "key1";
        var value = "MyValue";
        var json = $"{{\"{keyToEncrypt}\":\"{value}\",\"key2\":\"value2\"}}";
        var encryptedValue = "MyAesEncryptedValue";
        var encryptedJson = $"{{\"{keyToEncrypt}\":\"{encryptedValue}\",\"key2\":\"value2\"}}";
        var keysToEncrypt = new List<string> { keyToEncrypt };
        _jsonService.GetValueFromKey<string>(json, keyToEncrypt).Returns(value);
        _aesEncryptionService.Encrypt(value, AesKey, AesKeySize, Iv).Returns(encryptedValue);
        _jsonService.ReplaceValue(json, keyToEncrypt, encryptedValue).Returns(encryptedJson);

        // Act
        var actualEncryptedJson = _jsonEncryptor.Encrypt(json, keysToEncrypt, AesKey, AesKeySize, Iv);

        // Assert
        actualEncryptedJson.Should().Be(encryptedJson);
    }

    [Fact]
    public void Encrypt_ShouldReturnInputJson_WhenKeysToEncryptIsNull()
    {
        // Arrange
        var keyToEncrypt = "key1";
        var value = "MyValue";
        var json = $"{{\"{keyToEncrypt}\":\"{value}\",\"key2\":\"value2\"}}";
        List<string> keysToEncrypt = null!;

        // Act
        var actualEncryptedJson = _jsonEncryptor.Encrypt(json, keysToEncrypt, AesKey, AesKeySize, Iv);

        // Assert
        actualEncryptedJson.Should().Be(json);
    }

    [Fact]
    public void Encrypt_ShouldReturnInputJson_WhenThereAreNoKeysToEncrypt()
    {
        // Arrange
        var keyToEncrypt = "key1";
        var value = "MyValue";
        var json = $"{{\"{keyToEncrypt}\":\"{value}\",\"key2\":\"value2\"}}";
        var keysToEncrypt = new List<string>();

        // Act
        var actualEncryptedJson = _jsonEncryptor.Encrypt(json, keysToEncrypt, AesKey, AesKeySize, Iv);

        // Assert
        actualEncryptedJson.Should().Be(json);
    }

    [Theory]
    [InlineData("")]
    [InlineData(null)]
    public void Encrypt_ShouldReturnEmptyString_WhenInputJsonIsNullOrEmpty(string json)
    {
        // Arrange
        var keysToEncrypt = new List<string>
        {
            "key1"
        };

        // Act
        var actualDecryptedJson = _jsonEncryptor.Encrypt(json, keysToEncrypt, AesKey, AesKeySize, Iv);

        // Assert
        actualDecryptedJson.Should().Be(string.Empty);
    }

    [Fact]
    public void Encrypt_ShouldThrowException_WhenKeyNotFound()
    {
        // Arrange
        string json = "{\"name\":\"John Doe\"}";
        List<string> keysToEncrypt = new List<string> { "missingKey" };
        string aesKey = "myAesKey";
        int aesKeySize = 256;
        string iv = "myInitializationVector";

        var jsonService = Substitute.For<IJsonService>();
        jsonService.GetValueFromKey<string>(json, "missingKey").Returns((string?)null);
        var jsonEncryptor = new JsonEncryption(jsonService, _aesEncryptionService);

        // Act
        Action act = () => jsonEncryptor.Encrypt(json, keysToEncrypt, aesKey, aesKeySize, iv);

        // Assert
        act.Should().Throw<KeyNotFoundException>();
    }

    [Fact]
    public void Decrypt_ShouldReturnDecryptedJson_WhenInputIsValid()
    {
        // Arrange
        var keyToDecrypt = "key1";
        var encryptedValue = "MyAesEncryptedValue";
        var encryptedJson = $"{{\"{keyToDecrypt}\":\"{encryptedValue}\",\"key2\":\"value2\"}}";
        var decryptedValue = "MyValue";
        var decryptedJson = $"{{\"{keyToDecrypt}\":\"{decryptedValue}\",\"key2\":\"value2\"}}";
        var keysToDecrypt = new List<string> { keyToDecrypt };
        _jsonService.GetValueFromKey<string>(encryptedJson, keyToDecrypt).Returns(encryptedValue);
        _aesEncryptionService.Decrypt(encryptedValue, AesKey, AesKeySize, Iv).Returns(GetSecureString(decryptedValue));
        _jsonService.ReplaceValue(encryptedJson, keyToDecrypt, decryptedValue).Returns(decryptedJson);

        // Act
        var actualDecryptedJson = _jsonEncryptor.Decrypt(encryptedJson, keysToDecrypt, AesKey, AesKeySize, Iv);

        // Assert
        actualDecryptedJson.Should().Be(decryptedJson);
    }

    [Fact]
    public void Decrypt_ShouldReturnInputJson_WhenKeysToDecryptIsNull()
    {
        // Arrange
        var keysToDecrypt = new List<string>();
        var encryptedJson = "encryptedJson";

        // Act
        var actualDecryptedJson = _jsonEncryptor.Decrypt(encryptedJson, keysToDecrypt, AesKey, AesKeySize, Iv);

        // Assert
        actualDecryptedJson.Should().Be(encryptedJson);
    }

    [Fact]
    public void Decrypt_ShouldReturnInputJson_WhenThereAreNoKeysToDecrypt()
    {
        // Arrange
        var keyToDecrypt = "key1";
        var encryptedValue = "MyAesEncryptedValue";
        var encryptedJson = $"{{\"{keyToDecrypt}\":\"{encryptedValue}\",\"key2\":\"value2\"}}";
        var keysToDecrypt = new List<string>();

        // Act
        var actualDecryptedJson = _jsonEncryptor.Decrypt(encryptedJson, keysToDecrypt, AesKey, AesKeySize, Iv);

        // Assert
        actualDecryptedJson.Should().Be(encryptedJson);
    }

    [Theory]
    [InlineData("")]
    [InlineData(null)]
    public void Decrypt_ShouldReturnEmptyString_WhenInputJsonIsNullOrEmpty(string json)
    {
        // Arrange
        var keysToDecrypt = new List<string> { "Key" };

        // Act
        var actualDecryptedJson = _jsonEncryptor.Decrypt(json, keysToDecrypt, AesKey, AesKeySize, Iv);

        // Assert
        actualDecryptedJson.Should().Be(string.Empty);
    }
}
