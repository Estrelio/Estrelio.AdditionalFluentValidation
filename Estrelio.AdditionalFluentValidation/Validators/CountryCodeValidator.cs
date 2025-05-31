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

    /// <inheritdoc />
    protected override string GetDefaultMessageTemplate(string errorCode)
    {
        return "{PropertyName} is not a valid country code.";
    }
}