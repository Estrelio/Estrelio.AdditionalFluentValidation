// -----------------------------------------------------------------------
// <copyright file="Tests.cs" company="Estrelio">
// Copyright (c) Estrelio. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------

namespace Estrelio.AdditionalFluentValidation.Unit.Tests.Core.Validators.PossiblePhoneNumberValidator;

using Estrelio.AdditionalFluentValidation.Validators;
using FluentValidation.TestHelper;

public class Tests(UnitApp app) : AppTestBase<UnitApp>(app)
{
    [Fact]
    public void Name_ReturnsCorrectly()
    {
        // Arrange
        PossiblePhoneNumberValidator<UnitApp.RequestDto> validator = new();

        // Act
        string result = validator.Name;

        // Assert
        result.ShouldBe(nameof(PossiblePhoneNumberValidator<UnitApp.RequestDto>));
    }

    [Fact]
    public void IsValid_ReturnsTrue_CompletePossibleCountryQualifiedPhoneNumber()
    {
        // Arrange
        UnitApp.Validator validator = new();
        UnitApp.RequestDto requestDto = new()
        {
            PhoneNumber = "+6000000000",
        };

        // Act
        TestValidationResult<UnitApp.RequestDto>? result = validator.TestValidate(requestDto);

        // Assert
        result.ShouldNotHaveValidationErrorFor(r => r.PhoneNumber);
    }

    [Fact]
    public void IsValid_ReturnsTrue_CompletePossibleNationalPhoneNumberWithDefaultRegion()
    {
        // Arrange
        UnitApp.DefaultRegionValidator validator = new();
        UnitApp.RequestDto requestDto = new()
        {
            DefaultRegion = "MY",
            PhoneNumber = "0123456789",
        };

        // Act
        TestValidationResult<UnitApp.RequestDto>? result = validator.TestValidate(requestDto);

        // Assert
        result.ShouldNotHaveValidationErrorFor(r => r.PhoneNumber);
    }

    [Fact]
    public void IsValid_ReturnsFalse_NationalPhoneNumberWithoutDefaultRegion()
    {
        // Arrange
        UnitApp.Validator validator = new();
        UnitApp.RequestDto requestDto = new()
        {
            PhoneNumber = "0123456789",
        };

        // Act
        TestValidationResult<UnitApp.RequestDto>? result = validator.TestValidate(requestDto);

        // Assert
        result.ShouldHaveValidationErrorFor(r => r.PhoneNumber);
    }

    [Theory]
    [InlineData("not-a-phone-number")]
    [InlineData("+601")]
    [InlineData("+60123456789012345678")]
    public void IsValid_ReturnsFalse_MalformedTooShortOrTooLongPhoneNumber(string phoneNumber)
    {
        // Arrange
        UnitApp.Validator validator = new();
        UnitApp.RequestDto requestDto = new()
        {
            PhoneNumber = phoneNumber,
        };

        // Act
        TestValidationResult<UnitApp.RequestDto>? result = validator.TestValidate(requestDto);

        // Assert
        result.ShouldHaveValidationErrorFor(r => r.PhoneNumber);
    }

    [Fact]
    public void IsValid_ReturnsFalse_NationalPhoneNumberHasInvalidDefaultRegion()
    {
        // Arrange
        UnitApp.DefaultRegionValidator validator = new();
        UnitApp.RequestDto requestDto = new()
        {
            DefaultRegion = "ZZ",
            PhoneNumber = "0123456789",
        };

        // Act
        TestValidationResult<UnitApp.RequestDto>? result = validator.TestValidate(requestDto);

        // Assert
        result.ShouldHaveValidationErrorFor(r => r.PhoneNumber);
    }

    [Fact]
    public void IsValid_ReturnsFalse_PossibleLocalOnlyPhoneNumber()
    {
        // Arrange
        UnitApp.DefaultRegionValidator validator = new();
        UnitApp.RequestDto requestDto = new()
        {
            DefaultRegion = "US",
            PhoneNumber = "2530000",
        };

        // Act
        TestValidationResult<UnitApp.RequestDto>? result = validator.TestValidate(requestDto);

        // Assert
        result.ShouldHaveValidationErrorFor(r => r.PhoneNumber);
    }

    [Fact]
    public void PhoneNumber_RemainsStrict()
    {
        // Arrange
        UnitApp.StrictValidator validator = new();
        UnitApp.RequestDto requestDto = new()
        {
            PhoneNumber = "+6000000000",
        };

        // Act
        TestValidationResult<UnitApp.RequestDto>? result = validator.TestValidate(requestDto);

        // Assert
        result.ShouldHaveValidationErrorFor(r => r.PhoneNumber);
    }
}
