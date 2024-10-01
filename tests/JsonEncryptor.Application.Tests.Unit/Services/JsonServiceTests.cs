using FluentAssertions;
using JsonEncryptor.Application.Abstractions;
using JsonEncryptor.Application.Services.Json;

namespace JsonEncryptor.Application.Tests.Unit.Services;

public class JsonServiceTests
{
    private readonly IJsonService _jsonService;
    public JsonServiceTests()
    {
        _jsonService = new JsonService();
    }

    [Theory]
    [InlineData(@"{""name"":""John"",""age"":30,""city"":""New York""}")]
    [InlineData(@"[{""name"":""John"",""age"":30,""city"":""New York""}]")]
    public void IsJsonValid_ReturnsTrue_WhenInputIsValid(string input)
    {
        // Arrange

        // Act
        var result = _jsonService.IsJsonValid(input);

        // Assert
        result.Should().BeTrue();
    }

    [Theory]
    [InlineData(null)]
    [InlineData("")]
    public void IsJsonValid_ShouldReturnFalse_WhenValueIsNullOrEmpty(string? value)
    {
        // Act

        // Arrange
        var result = _jsonService.IsJsonValid(value!);

        // Assert
        result.Should().BeFalse();
    }

    [Theory]
    [InlineData(" ")]
    [InlineData("invalid json")]
    [InlineData("{name: 'John', age: 30")]
    [InlineData(@"{""name"":""John"",""age"":30,""city"":""New York""")]
    [InlineData(@"""name"":""John"",""age"":30,""city"":""New York""}")]
    [InlineData(@"[{""name"":""John"",""age"":30,""city"":""New York""}")]
    [InlineData(@"{""name"":""John"",""age"":30,""city"":""New York""}]")]
    public void IsJsonValid_ReturnsFalse_WhenInputIsInvalid(string input)
    {
        // Arrange

        // Act
        var result = _jsonService.IsJsonValid(input);

        // Assert
        result.Should().BeFalse();
    }

    [Theory]
    [InlineData(null)]
    [InlineData("")]
    public void PrettifyJson_ReturnsEmptyString_WhenInputIsNullOrEmpty(string? input)
    {
        // Arrange

        // Act
        string result = _jsonService.PrettifyJson(input!);

        // Assert
        result.Should().BeEmpty();
    }

    [Theory]
    [InlineData(" ")]
    [InlineData("invalid json")]
    [InlineData("{name: 'John', age: 30")]
    [InlineData(@"{""name"":""John"",""age"":30,""city"":""New York""")]
    [InlineData(@"""name"":""John"",""age"":30,""city"":""New York""}")]
    [InlineData(@"[{""name"":""John"",""age"":30,""city"":""New York""}")]
    [InlineData(@"{""name"":""John"",""age"":30,""city"":""New York""}]")]
    public void PrettifyJson_ReturnsOriginalJson_WhenInputIsInvalidJson(string? input)
    {
        // Arrange

        // Act
        string result = _jsonService.PrettifyJson(input!);

        // Assert
        result.Should().Be(input);
    }

    [Theory]
    [InlineData("{\"name\":\"John\",\"age\":30,\"city\":\"New York\"}", "{\r\n  \"name\": \"John\",\r\n  \"age\": 30,\r\n  \"city\": \"New York\"\r\n}")]
    [InlineData("[1, 2, 3]", "[\r\n  1,\r\n  2,\r\n  3\r\n]")]
    [InlineData("true", "true")]
    [InlineData("null", "null")]
    public void PrettifyJson_WhenInputIsValid_ReturnsPrettifiedJson(string input, string expected)
    {
        // Arrange

        // Act
        string result = _jsonService.PrettifyJson(input);

        // Assert
        result.Should().Be(expected);
    }

    [Theory]
    [InlineData("{\"name\":\"John\",\"age\":30}", "name", "John")]
    public void GetStringValueFromKey_ShouldReturnStringValue_WhenInputIsValid(string json, string key, string expectedValue)
    {
        // Act
        string? result = _jsonService.GetValueFromKey<string>(json, key);

        // Assert
        result.Should().Be(expectedValue);
    }

    [Theory]
    [InlineData("{\"name\":\"John\",\"age\":30}", "address", null)]
    public void GetStringValueFromKey_ShouldReturnDefault_WhenInputIsInvalid(string json, string key, string expectedValue)
    {
        // Act
        string? result = _jsonService.GetValueFromKey<string>(json, key);

        // Assert
        result.Should().Be(expectedValue);
    }

    [Theory]
    [InlineData("{\"name\":\"John\",\"age\":30}", "age", 30)]
    [InlineData("{\"data\":{\"value\":42}}", "data.value", 42)]
    public void GetIntValueFromKey_ShouldReturnIntValue_WhenInputIsValid(string json, string key, int expectedValue)
    {
        // Act
        int result = _jsonService.GetValueFromKey<int>(json, key);

        // Assert
        result.Should().Be(expectedValue);
    }

    [Theory]
    [InlineData("{\"name\":\"John\",\"age\":30}", "number", null)]
    public void GetIntValueFromKey_ShouldReturnDefault_WhenInputIsInvalid(string json, string key, int? expectedValue)
    {
        // Act
        int? result = _jsonService.GetValueFromKey<int?>(json, key);

        // Assert
        result.Should().Be(expectedValue);
    }

    [Fact]
    public void ReplaceValue_ShouldReturnModifiedJson_WhenKeyExists()
    {
        // Arrange
        string json = "{\"name\": \"John\", \"age\": 30}";
        string key = "age";
        string newValue = "35";

        // Act
        string result = _jsonService.ReplaceValue(json, key, newValue)!;

        // Assert
        result.Should().Be("{\r\n  \"name\": \"John\",\r\n  \"age\": \"35\"\r\n}");
    }

    [Fact]
    public void ReplaceValue_ShouldReturnOriginalJson_WhenKeyDoesNotExist()
    {
        // Arrange
        string json = "{\"name\": \"John\", \"age\": 30}";
        string key = "address";
        string newValue = "123 Main St";

        // Act
        string result = _jsonService.ReplaceValue(json, key, newValue)!;

        // Assert
        result.Should().Be(json);
    }

    [Theory]
    [InlineData(null)]
    [InlineData("")]
    public void ReplaceValue_ShouldReturnEmptyString_WhenJsonIsNullOrEmpty(string json)
    {
        // Arrange
        string key = "age";
        string newValue = "35";

        // Act
        string result = _jsonService.ReplaceValue(json, key, newValue)!;

        // Assert
        result.Should().BeEmpty();
    }

    [Theory]
    [InlineData("{\"a\":{\"b\":{\"c\":\"value\"}}}", "a.b.c", "value")]
    [InlineData("{\"a\":{\"b\":[{\"c\":\"value1\"},{\"c\":\"value2\"}]}}", "a.b[1].c", "value2")]
    public void GetValueFromKey_ValidKey_ShouldReturnValue(string json, string key, string expected)
    {
        // Act
        var result = _jsonService.GetValueFromKey<string>(json, key);

        // Assert
        result.Should().Be(expected);
    }

    [Fact]
    public void GetValueFromKey_InvalidKey_ShouldReturnDefault()
    {
        // Arrange
        var json = "{\"a\":{\"b\":{\"c\":\"value\"}}}";
        var key = "a.b.d";

        // Act
        var result = _jsonService.GetValueFromKey<string>(json, key);

        // Assert
        result.Should().BeNull();
    }

    [Fact]
    public void GetValueFromKey_EmptyJson_ShouldReturnDefault()
    {
        // Arrange
        var json = "";
        var key = "a.b.c";

        // Act
        var result = _jsonService.GetValueFromKey<string>(json, key);

        // Assert
        result.Should().BeNull();
    }

    [Fact]
    public void GetValueFromKey_EmptyKey_ShouldReturnDefault()
    {
        // Arrange
        var json = "{\"a\":{\"b\":{\"c\":\"value\"}}}";
        var key = "";

        // Act
        var result = _jsonService.GetValueFromKey<string>(json, key);

        // Assert
        result.Should().BeNull();
    }

    [Fact]
    public void GetValueFromKey_NonExistentArrayIndex_ShouldReturnDefault()
    {
        // Arrange
        var json = "{\"a\":{\"b\":[{\"c\":\"value1\"}]}}";
        var key = "a.b[2].c"; // Index out of bounds

        // Act
        var result = _jsonService.GetValueFromKey<string>(json, key);

        // Assert
        result.Should().BeNull();
    }

    [Fact]
    public void GetValueFromKey_InvalidArrayKeyFormat_ShouldReturnDefault()
    {
        // Arrange
        var json = "{\"a\":{\"b\":[{\"c\":\"value1\"}]}}";
        var key = "a.b[invalid].c"; // Invalid array index format

        // Act
        var result = _jsonService.GetValueFromKey<string>(json, key);

        // Assert
        result.Should().BeNull();
    }

    [Fact]
    public void GetValueFromKey_ArrayKey_ShouldReturnValue()
    {
        // Arrange
        var json = "{\"a\":{\"b\":[{\"c\":\"value1\"},{\"c\":\"value2\"}]}}";
        var key = "a.b[0].c"; // Valid array access

        // Act
        var result = _jsonService.GetValueFromKey<string>(json, key);

        // Assert
        result.Should().Be("value1");
    }
}
