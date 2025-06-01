// -----------------------------------------------------------------------
// <copyright file="GrpcAbstractValidator.cs" company="Estrelio">
// Copyright (c) Estrelio. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------

namespace Estrelio.AdditionalFluentValidation.AbstractValidators;

using FluentValidation;
using FluentValidation.Results;
using Google.Protobuf.WellKnownTypes;
using Google.Rpc;
using Grpc.Core;

/// <summary>
/// Abstract validator for gRPC services.
/// </summary>
/// <typeparam name="T">Type of object being validated.</typeparam>
public abstract class GrpcAbstractValidator<T> : AbstractValidator<T>
{
    /// <inheritdoc />
    protected override void RaiseValidationException(
        ValidationContext<T> context,
        ValidationResult result)
    {
        throw new Google.Rpc.Status()
        {
            Code = (int)Code.InvalidArgument,
            Message = "Bad request",
            Details =
            {
                Any.Pack(new BadRequest()
                {
                    FieldViolations =
                    {
                        new BadRequest.Types.FieldViolation()
                        {
                            Field = context.PropertyPath,
                            Description = result.Errors.FirstOrDefault()?.ErrorMessage ?? "Validation failed",
                        },
                    },
                }),
            },
        }.ToRpcException();
    }
}