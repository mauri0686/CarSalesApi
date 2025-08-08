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

        /// <summary>
        /// Executes a given method while measuring its execution time and logging the result.
        /// </summary>
        /// <typeparam name="T">The return type of the method being executed.</typeparam>
        /// <param name="method">The method to execute.</param>
        /// <param name="methodName">The name of the method being executed, typically used for logging purposes.</param>
        /// <returns>The result of the executed method.</returns>
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

        /// <summary>
        /// Executes a given method while measuring its execution time and logging the result.
        /// </summary>
        /// <param name="method">The method to execute.</param>
        /// <param name="methodName">The name of the method being executed, typically used for logging purposes.</param>
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
