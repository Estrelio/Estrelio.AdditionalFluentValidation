// -----------------------------------------------------------------------
// <copyright file="UnitApp.cs" company="Estrelio">
// Copyright (c) Estrelio. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------

namespace Estrelio.AdditionalFluentValidation.Unit.Tests.Core.Validators.CountryCodeValidator;

using Estrelio.AdditionalFluentValidation.Extensions;
using FluentValidation;

public class UnitApp : App
{
    public class Validator : AbstractValidator<RequestDto>
    {
        /// <inheritdoc />
        public Validator()
        {
            this.RuleFor(r => r.CountryCode).CountryCode();
        }
    }

    public record RequestDto
    {
        public string CountryCode { get; init; } = string.Empty;
    }
}