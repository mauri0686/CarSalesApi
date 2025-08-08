using CarSalesApi.Domain;
using CarSalesApi.Infrastructure;
using Microsoft.Extensions.Logging;

namespace CarSalesApi.Application
{
    /// <summary>
    /// Service responsible for handling sales-related operations in the car sales application.
    /// </summary>
    public class SaleService : ISaleService
    {
        private readonly ISaleRepository _repository;
        private readonly ILogger<SaleService> _logger;
        private readonly ServiceExecutionTimeInterceptor _interceptor;
        private static readonly int[] ValidDistributionCenterIds = [1, 2, 3, 4];

        public SaleService(ISaleRepository repository, ILogger<SaleService> logger)
        {
            _repository = repository;
            _logger = logger;
            _interceptor = new ServiceExecutionTimeInterceptor(logger);
        }

        /// <summary>
        /// Creates a sale record based on the provided sale request details.
        /// </summary>
        /// <param name="request">The request containing details such as the car type and distribution center ID for the sale.</param>
        [ServiceExecutionTime]
        public void CreateSale(CreateSaleRequest request)
        {
            _interceptor.Execute(() => {
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
                _repository.AddSale(sale);
            }, nameof(CreateSale));
        }

        /// <summary>
        /// Calculates the total sales volume across all distribution centers.
        /// </summary>
        [ServiceExecutionTime]
        public decimal GetTotalSalesVolume()
        {
            return _interceptor.Execute(() => {
                var sales = _repository.GetAllSales();
                return sales.Sum(s => s.SalePrice);
            }, nameof(GetTotalSalesVolume));
        }

        /// <summary>
        /// Calculates the total sales volume for a specific distribution center.
        /// </summary>
        [ServiceExecutionTime]
        public decimal GetSalesVolumeByCenter(int centerId)
        {
            return _interceptor.Execute(() => {
                if (!ValidDistributionCenterIds.Contains(centerId))
                    throw new ArgumentException("Centro de distribución inválido.");

                var sales = _repository.GetSalesByCenter(centerId);
                return sales.Sum(s => s.SalePrice);
            }, nameof(GetSalesVolumeByCenter));
        }

        /// <summary>
        /// Calculates the percentage of car sales by car type for each distribution center.
        /// </summary>
        [ServiceExecutionTime]
        public Dictionary<int, Dictionary<CarType, double>> GetPercentageByCenter()
        {
            return _interceptor.Execute(() => {
                var allSales = _repository.GetAllSales();
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
            }, nameof(GetPercentageByCenter));
        }
    }
}
