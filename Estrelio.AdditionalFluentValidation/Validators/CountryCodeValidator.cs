// -----------------------------------------------------------------------
// <copyright file="CountryCodeValidator.cs" company="Estrelio">
// Copyright (c) Estrelio. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------

namespace Estrelio.AdditionalFluentValidation.Validators;

using System.Globalization;
using FluentValidation;
using FluentValidation.Validators;

/// <summary>
/// The country code validator.
/// </summary>
/// <typeparam name="T">Type of object being validated.</typeparam>
public class CountryCodeValidator<T> : PropertyValidator<T, string>
{
    /// <inheritdoc />
    public override string Name => "CountryCodeValidator";

    /// <inheritdoc />
    public override bool IsValid(ValidationContext<T> context, string value)
    {
        // Check if the value is null or empty
        if (string.IsNullOrEmpty(value))
        {
            return false;
        }

        // Ensure the value is uppercase for case-insensitive validation
        value = value.ToUpperInvariant();

        return value.Length switch
        {
            2 => Validate2LetterCountryCode(value),
            3 => Validate3LetterCountryCode(value),
            _ => false,
        };
    }

    /// <inheritdoc />
    protected override string GetDefaultMessageTemplate(string errorCode)
    {
        return "{PropertyName} is not a valid country code.";
    }

    private static bool Validate3LetterCountryCode(string value)
    {
        try
        {
            CultureInfo[] cultures = CultureInfo.GetCultures(CultureTypes.SpecificCultures);
            bool cultureInfo = cultures.Any(c =>
                new RegionInfo(c.Name).ThreeLetterISORegionName.Equals(value,
                    StringComparison.OrdinalIgnoreCase));
            return cultureInfo;
        }
        catch
        {
            return false;
        }
    }

    private static bool Validate2LetterCountryCode(string value)
    {
        try
        {
            _ = new RegionInfo(value);
            return true;
        }
        catch (ArgumentException)
        {
            return false;
        }
    }
}