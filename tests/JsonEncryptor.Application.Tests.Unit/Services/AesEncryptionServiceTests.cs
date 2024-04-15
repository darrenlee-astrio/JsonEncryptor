using FluentAssertions;
using JsonEncryptor.Application.Abstractions;
using JsonEncryptor.Application.Common.Extensions;
using JsonEncryptor.Application.Services.Encryption;

namespace JsonEncryptor.Application.Tests.Unit.Services;

public class AesEncryptionServiceTests
{
    private readonly IAesEncryptionService _aesEncryptionService;

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

    [Fact]
    public void GenerateKey_ShouldReturnValidBase64EncodedKey()
    {
        // Arrange
        const int keySize = 256;

        // Act
        var key = _aesEncryptionService.GenerateKey(keySize);

        // Assert
        key.Should().NotBeNullOrEmpty();
        key.Length.Should().BeGreaterThan(0);

        byte[] decodedKey;
        Action decodeAction = () => decodedKey = Convert.FromBase64String(key);
        decodeAction.Should().NotThrow();
    }

    [Fact]
    public void GenerateIv_ShouldReturnValidBase64EncodedIv()
    {
        // Arrange

        // Act
        var iv = _aesEncryptionService.GenerateIv();

        // Assert
        iv.Should().NotBeNullOrEmpty();
        iv.Length.Should().BeGreaterThan(0);

        byte[] decodedIv;
        Action decodeAction = () => decodedIv = Convert.FromBase64String(iv);
        decodeAction.Should().NotThrow();
    }
}
