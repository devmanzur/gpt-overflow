using FluentValidation.Results;

namespace GPTOverflow.Core.CrossCuttingConcerns.Utils;

public static class ErrorFormatterUtils
{
    public const string DefaultErrorParamKey = "Request";

    //generate problem detail style errors from serialized error string
    public static Dictionary<string, List<string>>? ToProblemDetailFormat(List<ValidationFailure>? errors)
    {
        if (errors==null || !errors.Any())
        {
            return null;
        }

        var detailedError = new Dictionary<string, List<string>>();

        foreach (var error in errors)
        {
            var key = error.PropertyName??DefaultErrorParamKey;
            
            if (detailedError.ContainsKey(key))
            {
                detailedError[key].Add(error.ErrorMessage);
            }
            else
            {
                detailedError[key] = new List<string>()
                {
                    error.ErrorMessage
                };
            }
        }

        return detailedError;
    }
}