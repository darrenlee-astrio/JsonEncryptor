using Application.Common.Extensions;
using Application.Services.Encryption;
using FluentAssertions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace Application.Tests.Unit.Services;

public class AesEncryptionServiceTests
{
    private readonly AesEncryptionService _aesEncryptionService;

    public AesEncryptionServiceTests()
    {
        _aesEncryptionService = new AesEncryptionService();    
    }

    [Fact]
    public void EncryptUsingRandomKeyAndIv_ShouldEncryptSuccessfullyAndReturnEncryptedData_WhenPlainTextIsNotNull()
    {
        // Arrange
        string plainText = "Hello, world!";
        int keySize = 256;

        // Act
        var encryptedData = _aesEncryptionService.Encrypt(plainText, keySize, out string key, out string iv);

        // Assert
        var decryptedData = _aesEncryptionService.Decrypt(encryptedData, key, keySize, iv);
        decryptedData.ToInsecureString().Should().Be(plainText);
    }

    [Theory]
    [InlineData("")]
    [InlineData(null)]
    public void EncryptUsingRandomKeyAndIv_ShouldThrowArgumentNullException_WhenPlainTextIsNullOrEmpty(string plainText)
    {
        // Arrange
        int keySize = 256;

        // Act
        Action action = () => _aesEncryptionService.Encrypt(plainText, keySize, out string key, out string iv);

        // Assert
        action.Should().Throw<ArgumentNullException>()
            .And.ParamName.Should().Be(nameof(plainText));
    }

    [Theory]
    [InlineData("NDmLVm0EZJtj76tM37o9NQ==", 128)]
    [InlineData("BD5wDNzQ7BU5rXjCMsJd0kDkjCQoOgbAEB8iB7vLBGI=", 256)]
    public void EncryptUsingProvidedKeyAndRandomIv_ShouldEncryptSuccessfullyAndReturnEncryptedData_WhenPlainTextIsNotNull(string key, int keySize)
    {
        // Arrange
        string plainText = "Hello, world!";

        // Act
        var encryptedData = _aesEncryptionService.Encrypt(plainText, key, keySize, out string iv);

        // Assert
        var decryptedData = _aesEncryptionService.Decrypt(encryptedData, key, keySize, iv);
        decryptedData.ToInsecureString().Should().Be(plainText);
    }

    [Theory]
    [InlineData("")]
    [InlineData(null)]
    public void EncryptUsingProvidedKeyAndRandomIv_ShouldThrowArgumentNullException_WhenPlainTextIsNullOrEmpty(string plainText)
    {
        // Arrange
        int keySize = 256;
        string key = "";

        // Act
        Action action = () => _aesEncryptionService.Encrypt(plainText, key, keySize, out string iv);

        // Assert
        action.Should().Throw<ArgumentNullException>()
            .And.ParamName.Should().Be(nameof(plainText));
    }

    [Theory]
    [InlineData("NDmLVm0EZJtj76tM37o9NQ==", 128, "yP6Wu87HtW9nL+ezmfyC5A==")]
    [InlineData("BD5wDNzQ7BU5rXjCMsJd0kDkjCQoOgbAEB8iB7vLBGI=", 256, "yP6Wu87HtW9nL+ezmfyC5A==")]
    public void EncryptUsingProvidedKeyAndIv_ShouldEncryptSuccessfullyAndReturnEncryptedData_WhenPlainTextIsNotNull(string key, int keySize, string iv)
    {
        // Arrange
        string plainText = "Hello, world!";

        // Act
        var encryptedData = _aesEncryptionService.Encrypt(plainText, key, keySize, iv);

        // Assert
        var decryptedData = _aesEncryptionService.Decrypt(encryptedData, key, keySize, iv);
        decryptedData.ToInsecureString().Should().Be(plainText);
    }

    [Theory]
    [InlineData("")]
    [InlineData(null)]
    public void EncryptUsingProvidedKeyAndIv_ShouldThrowArgumentNullException_WhenPlainTextIsNullOrEmpty(string plainText)
    {
        // Arrange
        int keySize = 256;
        string key = "";
        string iv = "";

        // Act
        Action action = () => _aesEncryptionService.Encrypt(plainText, key, keySize, iv);

        // Assert
        action.Should().Throw<ArgumentNullException>()
            .And.ParamName.Should().Be(nameof(plainText));
    }
}
