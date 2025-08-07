using CarSalesApi.Domain;

namespace CarSalesApi.Application
{
    /// <summary>
    /// Represents a request to create a new sale in the system.
    /// </summary>
    public class CreateSaleRequest
    {
        public CarType CarType { get; set; }
        public int DistributionCenterId { get; set; }
    }
}