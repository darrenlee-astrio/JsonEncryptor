using Application.Helpers;
using FluentAssertions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Tests.Unit.Helpers;

public class JsonHelperTests
{
    [Theory]
    [InlineData(@"{""name"":""John"",""age"":30,""city"":""New York""}")]
    [InlineData(@"[{""name"":""John"",""age"":30,""city"":""New York""}]")]
    public void IsValidJson_ReturnsTrue_WhenInputIsValid(string input)
    {
        // Arrange

        // Act
        var result = JsonHelper.IsValidJson(input);

        // Assert
        result.Should().BeTrue();
    }

    [Theory]
    [InlineData(null)]
    [InlineData("")]
    public void IsValidJson_ShouldReturnFalse_WhenValueIsNullOrEmpty(string? value)
    {
        // Act

        // Arrange
        var result = JsonHelper.IsValidJson(value!);

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
    public void IsValidJson_ReturnsFalse_WhenInputIsInvalid(string input)
    {
        // Arrange

        // Act
        var result = JsonHelper.IsValidJson(input);

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
        string result = JsonHelper.PrettifyJson(input!);

        // Assert
        result.Should().BeEmpty();
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
        string result = JsonHelper.PrettifyJson(input);

        // Assert
        result.Should().Be(expected);
    }
}
