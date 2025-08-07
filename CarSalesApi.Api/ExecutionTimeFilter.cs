using Microsoft.AspNetCore.Mvc.Filters;
using System.Diagnostics;

namespace CarSalesApi.API
{
    public class ExecutionTimeFilter(ILogger<ExecutionTimeFilter> logger) : IActionFilter
    {
        private Stopwatch _stopwatch;

        public void OnActionExecuting(ActionExecutingContext context)
        {
            _stopwatch = Stopwatch.StartNew();
        }

        public void OnActionExecuted(ActionExecutedContext context)
        {
            _stopwatch.Stop();
            logger.LogInformation("Action {ActionDescriptorDisplayName} executed in {StopwatchElapsedMilliseconds} ms", context.ActionDescriptor.DisplayName, _stopwatch.ElapsedMilliseconds);
        }
    }
}