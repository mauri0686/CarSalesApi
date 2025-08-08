using CarSalesApi.Application;
using CarSalesApi.Domain;
using CarSalesApi.Infrastructure;
using Microsoft.Extensions.Logging;
using Moq;

namespace CarSalesApi.Tests
{
    public class SaleServiceTests
    {
        private readonly InMemorySaleRepository _repository;
        private readonly Mock<ILogger<SaleService>> _loggerMock = new Mock<ILogger<SaleService>>();
        private readonly SaleService _service;

        public SaleServiceTests()
        {
            _repository = new InMemorySaleRepository();
            _service = new SaleService(_repository, _loggerMock.Object);;
        }

        [Fact]
        public void CreateSale_AddsSaleToRepository()
        {
            var request = new CreateSaleRequest { CarType = CarType.Sedan, DistributionCenterId = 1 };
            _service.CreateSale(request);

            var sales = _repository.GetAllSales();
            Assert.Single(sales);
            Assert.Equal(CarType.Sedan, sales.First().Car.Type);
        }

        [Fact]
        public void GetTotalSalesVolume_ReturnsCorrectTotal()
        {
            _service.CreateSale(new CreateSaleRequest { CarType = CarType.Sedan, DistributionCenterId = 1 });
            _service.CreateSale(new CreateSaleRequest { CarType = CarType.Sport, DistributionCenterId = 1 });

            var total = _service.GetTotalSalesVolume();
            var expected = 8000m + (18200m * 1.07m);
            Assert.Equal(expected, total);
        }

        [Fact]
        public void GetPercentageByCenter_ReturnsCorrectPercentages()
        {
            _service.CreateSale(new CreateSaleRequest { CarType = CarType.Sedan, DistributionCenterId = 1 });
            _service.CreateSale(new CreateSaleRequest { CarType = CarType.SUV, DistributionCenterId = 1 });
            _service.CreateSale(new CreateSaleRequest { CarType = CarType.Sedan, DistributionCenterId = 2 });

            var result = _service.GetPercentageByCenter();

            Assert.Equal(2, result.Count);
            Assert.Equal(33.333, result[1][CarType.Sedan], 3);
            Assert.Equal(33.333, result[1][CarType.SUV], 3);
            Assert.Equal(33.333, result[2][CarType.Sedan], 3);
        }
    }
}