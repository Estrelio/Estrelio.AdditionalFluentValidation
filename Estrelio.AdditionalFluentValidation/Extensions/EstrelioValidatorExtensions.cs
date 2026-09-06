// -----------------------------------------------------------------------
// <copyright file="EstrelioValidatorExtensions.cs" company="Estrelio">
// Copyright (c) Estrelio. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------

namespace Estrelio.AdditionalFluentValidation.Extensions;

using Estrelio.AdditionalFluentValidation.Validators;
using FluentValidation;

/// <summary>
/// Validator extensions.
/// </summary>
public static class EstrelioValidatorExtensions
{
    /// <summary>
    /// Defines an E164 phone number validator.
    /// </summary>
    /// <param name="ruleBuilder">The rule builder on which the validator should be defined.</param>
    /// <typeparam name="T">Type of object being validated.</typeparam>
    /// <returns>The rule builder with the validator defined.</returns>
    public static IRuleBuilderOptions<T, string> PhoneNumber<T>(this IRuleBuilder<T, string> ruleBuilder)
    {
        return ruleBuilder.SetValidator(new PhoneNumberValidator<T>());
    }

    /// <summary>
    /// Defines a validator for a complete structurally possible phone number.
    /// </summary>
    /// <param name="ruleBuilder">The rule builder on which the validator should be defined.</param>
    /// <typeparam name="T">Type of object being validated.</typeparam>
    /// <returns>The rule builder with the validator defined.</returns>
    public static IRuleBuilderOptions<T, string> PossiblePhoneNumber<T>(this IRuleBuilder<T, string> ruleBuilder)
    {
        return ruleBuilder.SetValidator(new PossiblePhoneNumberValidator<T>());
    }

    /// <summary>
    /// Defines a validator for a complete structurally possible phone number using a default parsing region.
    /// </summary>
    /// <param name="ruleBuilder">The rule builder on which the validator should be defined.</param>
    /// <param name="defaultRegionSelector">Selects an ISO alpha-2 default region from the object being validated.</param>
    /// <typeparam name="T">Type of object being validated.</typeparam>
    /// <returns>The rule builder with the validator defined.</returns>
    public static IRuleBuilderOptions<T, string> PossiblePhoneNumber<T>(
        this IRuleBuilder<T, string> ruleBuilder,
        Func<T, string?> defaultRegionSelector)
    {
        ArgumentNullException.ThrowIfNull(defaultRegionSelector);

        return ruleBuilder.SetValidator(new PossiblePhoneNumberValidator<T>(defaultRegionSelector));
    }

    /// <summary>
    /// Defines a country code validator.
    /// </summary>
    /// <param name="ruleBuilder">The rule builder on which the validator should be defined.</param>
    /// <typeparam name="T">Type of object being validated.</typeparam>
    /// <returns>The rule builder with the validator defined.</returns>
    public static IRuleBuilderOptions<T, string> CountryCode<T>(this IRuleBuilder<T, string> ruleBuilder)
    {
        return ruleBuilder.SetValidator(new CountryCodeValidator<T>());
    }

    /// <summary>
    /// Defines a GUID validator.
    /// </summary>
    /// <param name="ruleBuilder">The rule builder on which the validator should be defined.</param>
    /// <typeparam name="T">Type of object being validated.</typeparam>
    /// <returns>The rule builder with the validator defined.</returns>
    public static IRuleBuilderOptions<T, string> Guid<T>(this IRuleBuilder<T, string> ruleBuilder)
    {
        return ruleBuilder.SetValidator(new GuidValidator<T>());
    }
}
