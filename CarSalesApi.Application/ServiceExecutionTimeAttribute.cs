using Microsoft.Extensions.Logging;
using System.Diagnostics;
using System.Reflection;

namespace CarSalesApi.Application
{
    /// <summary>
    /// Attribute for measuring and logging execution time of service methods
    /// </summary>
    [AttributeUsage(AttributeTargets.Method)]
    public class ServiceExecutionTimeAttribute : Attribute
    {
    }

    /// <summary>
    /// Interceptor for service execution time measurement
    /// </summary>
    public class ServiceExecutionTimeInterceptor
    {
        private readonly ILogger _logger;

        public ServiceExecutionTimeInterceptor(ILogger logger)
        {
            _logger = logger;
        }

        public T Execute<T>(Func<T> method, string methodName)
        {
            var stopwatch = Stopwatch.StartNew();
            try
            {
                return method();
            }
            finally
            {
                stopwatch.Stop();
                _logger.LogInformation("Service method {MethodName} executed in {ElapsedMilliseconds} ms",
                    methodName, stopwatch.ElapsedMilliseconds);
            }
        }

        public void Execute(Action method, string methodName)
        {
            var stopwatch = Stopwatch.StartNew();
            try
            {
                method();
            }
            finally
            {
                stopwatch.Stop();
                _logger.LogInformation("Service method {MethodName} executed in {ElapsedMilliseconds} ms",
                    methodName, stopwatch.ElapsedMilliseconds);
            }
        }
    }
}
