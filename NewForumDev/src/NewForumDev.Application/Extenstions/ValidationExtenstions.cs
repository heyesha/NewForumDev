using FluentValidation.Results;
using Shared;

namespace NewForumDev.Application.Extenstions;

public static class ValidationExtenstions
{
    public static Error[] ToErrors(this ValidationResult validationResult) =>
        validationResult.Errors.Select(e => Error.Validation(e.ErrorCode, e.ErrorMessage, e.PropertyName)).ToArray();
}