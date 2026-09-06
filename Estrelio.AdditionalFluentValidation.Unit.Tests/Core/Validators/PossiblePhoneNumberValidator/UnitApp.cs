// -----------------------------------------------------------------------
// <copyright file="UnitApp.cs" company="Estrelio">
// Copyright (c) Estrelio. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------

namespace Estrelio.AdditionalFluentValidation.Unit.Tests.Core.Validators.PossiblePhoneNumberValidator;

using Estrelio.AdditionalFluentValidation.Extensions;
using FluentValidation;

public class UnitApp : App
{
    public class Validator : AbstractValidator<RequestDto>
    {
        /// <inheritdoc />
        public Validator()
        {
            this.RuleFor(r => r.PhoneNumber).PossiblePhoneNumber();
        }
    }

    public class DefaultRegionValidator : AbstractValidator<RequestDto>
    {
        /// <inheritdoc />
        public DefaultRegionValidator()
        {
            this.RuleFor(r => r.PhoneNumber).PossiblePhoneNumber(r => r.DefaultRegion);
        }
    }

    public class StrictValidator : AbstractValidator<RequestDto>
    {
        /// <inheritdoc />
        public StrictValidator()
        {
            this.RuleFor(r => r.PhoneNumber).PhoneNumber();
        }
    }

    public record RequestDto
    {
        public string PhoneNumber { get; init; } = string.Empty;

        public string? DefaultRegion { get; init; }
    }
}
