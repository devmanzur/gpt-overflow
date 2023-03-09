using FluentValidation.Results;
using GPTOverflow.Core.CrossCuttingConcerns.Utils;

namespace GPTOverflow.API.Modules.CrossCuttingConcerns.Models;

/// <summary>
/// All endpoints/ api responses should Ideally use this one as it provides a uniform interface for all responses
/// </summary>
/// <typeparam name="T"></typeparam>
public class Envelope<T>
{
    // ReSharper disable once MemberCanBeProtected.Global
    protected internal Envelope(T body, string? errorMessage, List<ValidationFailure>? errors = null)
    {
        Body = body;
        Errors = ErrorFormatterUtils.ToProblemDetailFormat(errors);
        IsSuccess = errorMessage == null;
    }

    public T Body { get; set; }
    public Dictionary<string, List<string>>? Errors { get; set; }
    public bool IsSuccess { get; set; }
}

public class Envelope : Envelope<string>
{

    private Envelope(string? errorMessage, List<ValidationFailure>? errors = null)
        : base(errorMessage ?? string.Empty, errorMessage,errors)
    {
    }

    public static Envelope<T> Ok<T>(T result)
    {
        return new Envelope<T>(result, null);
    }

    public static Envelope Ok()
    {
        return new Envelope(null);
    }

    public static Envelope Error(string errorMessage, List<ValidationFailure>? errors = null)
    {
        return new Envelope(errorMessage,errors);
    }
}