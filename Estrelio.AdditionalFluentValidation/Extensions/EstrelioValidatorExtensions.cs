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
}