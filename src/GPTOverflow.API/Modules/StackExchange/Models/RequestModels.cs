namespace GPTOverflow.API.Modules.StackExchange.Models;

public record PostNewQuestionRequest(string Title,string Description, List<string>? Tags);