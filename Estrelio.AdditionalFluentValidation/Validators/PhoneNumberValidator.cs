// -----------------------------------------------------------------------
// <copyright file="PhoneNumberValidator.cs" company="Estrelio">
// Copyright (c) Estrelio. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------

namespace Estrelio.AdditionalFluentValidation.Validators;

using FluentValidation;
using FluentValidation.Validators;
using PhoneNumbers;

/// <summary>
/// The E164 phone number validator.
/// </summary>
/// <typeparam name="T">Type of object being validated.</typeparam>
public class PhoneNumberValidator<T> : PropertyValidator<T, string>
{
    /// <inheritdoc />
    public override string Name => "PhoneNumberValidator";

    /// <inheritdoc />
    public override bool IsValid(ValidationContext<T> context, string value)
    {
        var phoneNumberUtil = PhoneNumberUtil.GetInstance();
        try
        {
            PhoneNumber phoneNumber = phoneNumberUtil.Parse(value, null);
            return phoneNumberUtil.IsValidNumber(phoneNumber);
        }
        catch (NumberParseException)
        {
            return false;
        }
    }

    /// <inheritdoc />
    protected override string GetDefaultMessageTemplate(string errorCode)
    {
        return "{PropertyName} is not a valid phone number.";
    }
}