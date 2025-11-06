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
        // Convert all validation errors to field violations
        List<BadRequest.Types.FieldViolation> fieldViolations = result.Errors.Select(error =>
            new BadRequest.Types.FieldViolation
            {
                Field = error.PropertyName,
                Description = error.ErrorMessage,
            }).ToList();

        // Create BadRequest with all field violations
        var badRequest = new BadRequest
        {
            FieldViolations = { fieldViolations },
        };

        // Create Google.Rpc.Status with BadRequest details
        var status = new Google.Rpc.Status
        {
            Code = (int)Code.InvalidArgument,
            Message = "Validation failed",
            Details =
            {
                Any.Pack(badRequest),
            },
        };

        // Convert to RpcException
        throw status.ToRpcException();
    }
}