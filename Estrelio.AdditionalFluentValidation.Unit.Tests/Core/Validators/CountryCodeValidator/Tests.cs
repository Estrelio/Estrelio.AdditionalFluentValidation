// -----------------------------------------------------------------------
// <copyright file="Tests.cs" company="Estrelio">
// Copyright (c) Estrelio. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------

namespace Estrelio.AdditionalFluentValidation.Unit.Tests.Core.Validators.CountryCodeValidator;

using Estrelio.AdditionalFluentValidation.Validators;
using FluentValidation.TestHelper;

public class Tests(UnitApp app) : AppTestBase<UnitApp>(app)
{
    [Fact]
    public void Name_ReturnsCorrectly()
    {
        // Arrange
        CountryCodeValidator<UnitApp.RequestDto> validator = new();

        // Act
        string result = validator.Name;

        // Assert
        result.ShouldBe("CountryCodeValidator");
    }

    [Theory]
    [InlineData("MY")]
    [InlineData("US")]
    [InlineData("GB")]
    [InlineData("CA")]
    [InlineData("AU")]
    [InlineData("DE")]
    [InlineData("FR")]
    [InlineData("JP")]
    public void IsValid_ReturnsTrue_ValidTwoLetterCountryCodes(string countryCode)
    {
        // Arrange
        UnitApp.Validator validator = new();
        UnitApp.RequestDto requestDto = new()
        {
            CountryCode = countryCode,
        };

        // Act
        TestValidationResult<UnitApp.RequestDto>? result = validator.TestValidate(requestDto);

        // Assert
        result.ShouldNotHaveValidationErrorFor(r => r.CountryCode);
    }

    [Theory]
    [InlineData("MYS")]
    [InlineData("USA")]
    [InlineData("GBR")]
    [InlineData("CAN")]
    [InlineData("AUS")]
    [InlineData("DEU")]
    [InlineData("FRA")]
    [InlineData("JPN")]
    public void IsValid_ReturnsTrue_ValidThreeLetterCountryCodes(string countryCode)
    {
        // Arrange
        UnitApp.Validator validator = new();
        UnitApp.RequestDto requestDto = new()
        {
            CountryCode = countryCode,
        };

        // Act
        TestValidationResult<UnitApp.RequestDto>? result = validator.TestValidate(requestDto);

        // Assert
        result.ShouldNotHaveValidationErrorFor(r => r.CountryCode);
    }

    [Theory]
    [InlineData("us")]
    [InlineData("my")]
    [InlineData("usa")]
    [InlineData("mys")]
    [InlineData("Gb")]
    [InlineData("CaN")]
    [InlineData("dEu")]
    public void IsValid_ReturnsTrue_CaseInsensitive(string countryCode)
    {
        // Arrange
        UnitApp.Validator validator = new();
        UnitApp.RequestDto requestDto = new()
        {
            CountryCode = countryCode,
        };

        // Act
        TestValidationResult<UnitApp.RequestDto>? result = validator.TestValidate(requestDto);

        // Assert
        result.ShouldNotHaveValidationErrorFor(r => r.CountryCode);
    }

    [Fact]
    public void IsValid_ReturnsFalse_CountryCodeIsEmpty()
    {
        // Arrange
        UnitApp.Validator validator = new();
        UnitApp.RequestDto requestDto = new()
        {
            CountryCode = string.Empty,
        };

        // Act
        TestValidationResult<UnitApp.RequestDto>? result = validator.TestValidate(requestDto);

        // Assert
        result.ShouldHaveValidationErrorFor(r => r.CountryCode)
            .WithErrorMessage("Country Code is not a valid country code.");
    }

    [Fact]
    public void IsValid_ReturnsFalse_CountryCodeIsWhitespace()
    {
        // Arrange
        UnitApp.Validator validator = new();
        UnitApp.RequestDto requestDto = new()
        {
            CountryCode = "   ",
        };

        // Act
        TestValidationResult<UnitApp.RequestDto>? result = validator.TestValidate(requestDto);

        // Assert
        result.ShouldHaveValidationErrorFor(r => r.CountryCode)
            .WithErrorMessage("Country Code is not a valid country code.");
    }

    [Theory]
    [InlineData("X")]
    [InlineData("ZZ")]
    [InlineData("ABC")]
    [InlineData("XYZ")]
    [InlineData("ABCD")]
    [InlineData("123")]
    [InlineData("12")]
    [InlineData("AB123")]
    [InlineData("!@#")]
    public void IsValid_ReturnsFalse_InvalidCountryCodes(string countryCode)
    {
        // Arrange
        UnitApp.Validator validator = new();
        UnitApp.RequestDto requestDto = new()
        {
            CountryCode = countryCode,
        };

        // Act
        TestValidationResult<UnitApp.RequestDto>? result = validator.TestValidate(requestDto);

        // Assert
        result.ShouldHaveValidationErrorFor(r => r.CountryCode)
            .WithErrorMessage("Country Code is not a valid country code.");
    }
}