﻿namespace Shared;

public record Error
{
    public static Error None = new Error(string.Empty, string.Empty, ErrorType.NONE, null);
    public string Code { get; }
    public string Message { get; }
    public ErrorType Type { get; }
    public string? InvalidField { get; }

    private Error(string code, string message, ErrorType type, string? invalidField = null)
    {
        Code = code;
        Message = message;
        Type = type;
        InvalidField = invalidField;
    }
    
    public static Error NotFound(string? code, string message, Guid? id)
        => new Error(code ?? "record.not.found", message, ErrorType.NOT_FOUND);
    
    public static Error Validation(string? code, string message, string? invalidField) 
        => new Error(code ?? "value.is.invalid", message, ErrorType.VALIDATION, invalidField);
    
    public static Error Conflict(string? code, string message)
        => new Error(code ?? "value.is.conflict", message, ErrorType.CONFLICT);
    
    public static Error Failure(string? code, string message)
        => new Error(code ?? "failure", message, ErrorType.FAILURE);

    public Failure ToFailure() => this;
}

public enum ErrorType
{
    /// <summary>
    /// Неизвестная ошибка
    /// </summary>
    NONE,
    
    /// <summary>
    /// Ошибка с валидацией.
    /// </summary>
    VALIDATION,
    
    /// <summary>
    /// Ошибка ничего не найдено
    /// </summary>
    NOT_FOUND,
    
    /// <summary>
    /// Ошибка сервера.
    /// </summary>
    FAILURE,
    
    /// <summary>
    /// Ошибка конфликт.
    /// </summary>
    CONFLICT,
}