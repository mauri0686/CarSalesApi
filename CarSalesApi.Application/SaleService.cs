using CarSalesApi.Domain;
using CarSalesApi.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;

namespace CarSalesApi.Application
{
    public class SaleService : ISaleService
    {
        private readonly InMemorySaleRepository _repository;

        public SaleService(InMemorySaleRepository repository)
        {
            _repository = repository;
        }

        public void CreateSale(CreateSaleRequest request)
        {
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
        }

        public decimal GetTotalSalesVolume()
        {
            var sales = _repository.GetAllSales();
            return sales.Sum(s => s.SalePrice);
        }

        public decimal GetSalesVolumeByCenter(int centerId)
        {
            var sales = _repository.GetSalesByCenter(centerId);
            return sales.Sum(s => s.SalePrice);
        }

        public Dictionary<int, Dictionary<CarType, double>> GetPercentageByCenter()
        {
            var allSales = _repository.GetAllSales();
            var totalSales = allSales.Count();

            if (totalSales == 0)
            {
                return new Dictionary<int, Dictionary<CarType, double>>();
            }

            var salesByCenter = allSales.GroupBy(s => s.DistributionCenterId);
            var result = new Dictionary<int, Dictionary<CarType, double>>();

            foreach (var centerGroup in salesByCenter)
            {
                var centerId = centerGroup.Key;
                var salesInCenter = centerGroup.ToList();
                var salesByType = salesInCenter.GroupBy(s => s.Car.Type)
                    .ToDictionary(g => g.Key, g => (double)g.Count() / totalSales * 100);
                result[centerId] = salesByType;
            }

            return result;
        }
    }
}