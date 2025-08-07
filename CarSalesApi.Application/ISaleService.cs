using CarSalesApi.Domain;

namespace CarSalesApi.Application
{
    /// <summary>
    /// Defines the contract for a service that manages car sales operations.
    /// </summary>
    public interface ISaleService
    {
        void CreateSale(CreateSaleRequest request);
        decimal GetTotalSalesVolume();
        decimal GetSalesVolumeByCenter(int centerId);
        Dictionary<int, Dictionary<CarType, double>> GetPercentageByCenter();
    }
}