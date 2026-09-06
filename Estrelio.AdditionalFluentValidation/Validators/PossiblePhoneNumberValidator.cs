// -----------------------------------------------------------------------
// <copyright file="PossiblePhoneNumberValidator.cs" company="Estrelio">
// Copyright (c) Estrelio. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------

namespace Estrelio.AdditionalFluentValidation.Validators;

using FluentValidation;
using FluentValidation.Validators;
using PhoneNumbers;

/// <summary>
/// A validator for complete structurally possible phone numbers.
/// </summary>
/// <typeparam name="T">Type of object being validated.</typeparam>
public class PossiblePhoneNumberValidator<T> : PropertyValidator<T, string>
{
    private readonly Func<T, string?>? defaultRegionSelector;

    /// <summary>
    /// Initializes a new instance of the <see cref="PossiblePhoneNumberValidator{T}"/> class.
    /// </summary>
    public PossiblePhoneNumberValidator()
    {
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="PossiblePhoneNumberValidator{T}"/> class.
    /// </summary>
    /// <param name="defaultRegionSelector">Selects an ISO alpha-2 default region from the object being validated.</param>
    public PossiblePhoneNumberValidator(Func<T, string?> defaultRegionSelector)
    {
        ArgumentNullException.ThrowIfNull(defaultRegionSelector);

        this.defaultRegionSelector = defaultRegionSelector;
    }

    /// <inheritdoc />
    public override string Name => nameof(PossiblePhoneNumberValidator<T>);

    /// <inheritdoc />
    public override bool IsValid(ValidationContext<T> context, string value)
    {
        string? defaultRegion = this.GetDefaultRegion(context.InstanceToValidate);

        return this.TryValidatePossiblePhoneNumber(value, defaultRegion);
    }

    /// <inheritdoc />
    protected override string GetDefaultMessageTemplate(string errorCode)
    {
        return "{PropertyName} is not a valid phone number.";
    }

    private string? GetDefaultRegion(T instanceToValidate)
    {
        if (this.defaultRegionSelector is null)
        {
            return null;
        }

        return this.defaultRegionSelector(instanceToValidate);
    }

    private bool TryValidatePossiblePhoneNumber(string value, string? defaultRegion)
    {
        if (string.IsNullOrWhiteSpace(value))
        {
            return false;
        }

        try
        {
            PhoneNumberUtil phoneNumberUtil = PhoneNumberUtil.GetInstance();
            PhoneNumber phoneNumber = phoneNumberUtil.Parse(value, defaultRegion);

            return phoneNumberUtil.IsPossibleNumberWithReason(phoneNumber) == PhoneNumberUtil.ValidationResult.IS_POSSIBLE;
        }
        catch (NumberParseException)
        {
            return false;
        }
    }
}
