using CarSalesApi.Domain;
using CarSalesApi.Infrastructure;
using Microsoft.Extensions.Logging;
using System.Diagnostics;

namespace CarSalesApi.Application
{
    /// <summary>
    /// Service responsible for handling sales-related operations in the car sales application.
    /// </summary>
    public class SaleService(ISaleRepository repository, ILogger<SaleService> logger) : ISaleService
    {
        private static readonly int[] ValidDistributionCenterIds = [1, 2, 3, 4];

        /// <summary>
        /// Creates a sale record based on the provided sale request details.
        /// </summary>
        /// <param name="request">The request containing details such as the car type and distribution center ID for the sale.</param>
        public void CreateSale(CreateSaleRequest request)
        {
            var stopwatch = Stopwatch.StartNew();
            try
            {
                // Validación de parámetros
                if (!Enum.IsDefined(typeof(CarType), request.CarType))
                    throw new ArgumentException("Tipo de vehículo inválido.");

                if (!ValidDistributionCenterIds.Contains(request.DistributionCenterId))
                    throw new ArgumentException("Centro de distribución inválido.");

                var car = new CarModel(request.CarType);
                var sale = new Sale
                {
                    Id = Guid.NewGuid(),
                    Car = car,
                    DistributionCenterId = request.DistributionCenterId,
                    SaleDate = DateTime.UtcNow,
                    SalePrice = car.GetSalePrice()
                };
                repository.AddSale(sale);
            }
            finally
            {
                stopwatch.Stop();
                logger.LogInformation("Service method {MethodName} executed in {ElapsedMilliseconds} ms", 
                    nameof(CreateSale), stopwatch.ElapsedMilliseconds);
            }
        }

        /// <summary>
        /// Calculates the total sales volume across all distribution centers.
        /// </summary>
        public decimal GetTotalSalesVolume()
        {
            var stopwatch = Stopwatch.StartNew();
            try
            {
                var sales = repository.GetAllSales();
                return sales.Sum(s => s.SalePrice);
            }
            finally
            {
                stopwatch.Stop();
                logger.LogInformation("Service method {MethodName} executed in {ElapsedMilliseconds} ms", 
                    nameof(GetTotalSalesVolume), stopwatch.ElapsedMilliseconds);
            }
        }

        /// <summary>
        /// Calculates the total sales volume for a specific distribution center.
        /// </summary>
        public decimal GetSalesVolumeByCenter(int centerId)
        {
            var stopwatch = Stopwatch.StartNew();
            try
            {
                if (!ValidDistributionCenterIds.Contains(centerId))
                    throw new ArgumentException("Centro de distribución inválido.");

                var sales = repository.GetSalesByCenter(centerId);
                return sales.Sum(s => s.SalePrice);
            }
            finally
            {
                stopwatch.Stop();
                logger.LogInformation("Service method {MethodName} executed in {ElapsedMilliseconds} ms", 
                    nameof(GetSalesVolumeByCenter), stopwatch.ElapsedMilliseconds);
            }
        }

        /// <summary>
        /// Calculates the percentage of car sales by car type for each distribution center.
        /// </summary>
        public Dictionary<int, Dictionary<CarType, double>> GetPercentageByCenter()
        {
            var stopwatch = Stopwatch.StartNew();
            try
            {
                var allSales = repository.GetAllSales();
                var totalSales = allSales.Count();

                if (totalSales == 0)
                    return new Dictionary<int, Dictionary<CarType, double>>();

                var salesByCenter = allSales.GroupBy(s => s.DistributionCenterId);
                var result = new Dictionary<int, Dictionary<CarType, double>>();

                foreach (var centerGroup in salesByCenter)
                {
                    var centerId = centerGroup.Key;
                    var salesInCenter = centerGroup.ToList();
                    var salesByType = salesInCenter
                        .GroupBy(s => s.Car.Type)
                        .ToDictionary(
                            g => g.Key,
                            g => (double)g.Count() / totalSales * 100
                        );

                    result[centerId] = salesByType;
                }

                return result;
            }
            finally
            {
                stopwatch.Stop();
                logger.LogInformation("Service method {MethodName} executed in {ElapsedMilliseconds} ms", 
                    nameof(GetPercentageByCenter), stopwatch.ElapsedMilliseconds);
            }
        }
    }
}
