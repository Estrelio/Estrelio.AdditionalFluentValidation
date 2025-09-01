// -----------------------------------------------------------------------
// <copyright file="GuidValidator.cs" company="Estrelio">
// Copyright (c) Estrelio. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------

namespace Estrelio.AdditionalFluentValidation.Validators;

using FluentValidation;
using FluentValidation.Validators;

/// <summary>
/// Provides validation to check whether a given string value represents a valid GUID.
/// </summary>
/// <typeparam name="T">The type of the object being validated.</typeparam>
public class GuidValidator<T> : PropertyValidator<T, string>
{
    /// <inheritdoc />
    public override string Name => "GuidValidator";

    /// <inheritdoc />
    public override bool IsValid(ValidationContext<T> context, string value)
    {
        if (string.IsNullOrWhiteSpace(value))
        {
            return false;
        }

        return Guid.TryParse(value, out _);
    }

    /// <inheritdoc />
    protected override string GetDefaultMessageTemplate(string errorCode)
    {
        return "{PropertyName} is not a valid GUID.";
    }
}