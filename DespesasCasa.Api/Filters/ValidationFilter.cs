
using System.Text.Json;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

public class ValidationFilter : IActionFilter
{
    private readonly ILogger<ValidationFilter> _logger;

    public ValidationFilter(ILogger<ValidationFilter> logger)
    {
        _logger = logger;
    }

    public void OnActionExecuting(ActionExecutingContext context)
    {
        if (!context.ModelState.IsValid)
        {
            var errors = context.ModelState.Where(v => v.Value?.Errors.Count > 0)
                .SelectMany(v =>
                    v.Value!.Errors.Select(e => new Error() { Code = v.Key.TrimStart('$', '.'), Message = e.ErrorMessage }))
                .ToList();

            _logger.LogError(new Exception(JsonSerializer.Serialize(errors)), $"Validation error in {context.ActionDescriptor.DisplayName}");

            context.Result = new UnprocessableEntityObjectResult(new ErrorViewModel(errors));
        }
    }

    public void OnActionExecuted(ActionExecutedContext context)
    {

    }
}
