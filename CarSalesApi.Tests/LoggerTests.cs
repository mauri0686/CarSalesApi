using CarSalesApi.Application;
using CarSalesApi.Domain;
using CarSalesApi.Infrastructure;
using Microsoft.Extensions.Logging;
using Moq;
using System.Diagnostics;

namespace CarSalesApi.Tests
{
    public class LoggerTests
    {
        private readonly Mock<ILogger<SaleService>> _loggerMock = new Mock<ILogger<SaleService>>();
        private readonly Mock<ISaleRepository> _repositoryMock = new Mock<ISaleRepository>();
        private readonly SaleService _service;

        public LoggerTests()
        {
            _service = new SaleService(_repositoryMock.Object, _loggerMock.Object);
        }

        [Fact]
        public void CreateSale_LogsExecutionTime()
        {
            // Arrange
            var request = new CreateSaleRequest { CarType = CarType.Sedan, DistributionCenterId = 1 };

            // Act
            _service.CreateSale(request);

            // Assert
            _loggerMock.Verify(
                logger => logger.Log(
                    LogLevel.Information,
                    It.IsAny<EventId>(),
                    It.Is<It.IsAnyType>((v, t) => v.ToString().Contains("CreateSale") && v.ToString().Contains("executed in")),
                    It.IsAny<Exception>(),
                    It.IsAny<Func<It.IsAnyType, Exception?, string>>()),
                Times.Once);
        }

        [Fact]
        public void GetTotalSalesVolume_LogsExecutionTime()
        {
            // Act
            _service.GetTotalSalesVolume();

            // Assert
            _loggerMock.Verify(
                logger => logger.Log(
                    LogLevel.Information,
                    It.IsAny<EventId>(),
                    It.Is<It.IsAnyType>((v, t) => v.ToString().Contains("GetTotalSalesVolume") && v.ToString().Contains("executed in")),
                    It.IsAny<Exception>(),
                    It.IsAny<Func<It.IsAnyType, Exception?, string>>()),
                Times.Once);
        }

        [Fact]
        public void GetSalesVolumeByCenter_LogsExecutionTime()
        {
            // Act
            _service.GetSalesVolumeByCenter(1);

            // Assert
            _loggerMock.Verify(
                logger => logger.Log(
                    LogLevel.Information,
                    It.IsAny<EventId>(),
                    It.Is<It.IsAnyType>((v, t) => v.ToString().Contains("GetSalesVolumeByCenter") && v.ToString().Contains("executed in")),
                    It.IsAny<Exception>(),
                    It.IsAny<Func<It.IsAnyType, Exception?, string>>()),
                Times.Once);
        }

        [Fact]
        public void GetPercentageByCenter_LogsExecutionTime()
        {
            // Act
            _service.GetPercentageByCenter();

            // Assert
            _loggerMock.Verify(
                logger => logger.Log(
                    LogLevel.Information,
                    It.IsAny<EventId>(),
                    It.Is<It.IsAnyType>((v, t) => v.ToString().Contains("GetPercentageByCenter") && v.ToString().Contains("executed in")),
                    It.IsAny<Exception>(),
                    It.IsAny<Func<It.IsAnyType, Exception?, string>>()),
                Times.Once);
        }
    }
}
