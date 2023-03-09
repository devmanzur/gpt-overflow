namespace GPTOverflow.API.Modules.CrossCuttingConcerns.Middlewares;

public interface IFactoryMiddleware
{
    Task InvokeAsync(HttpContext context);
}