using CarSalesApi.Domain;

namespace CarSalesApi.Application
{
    /// <summary>
    /// Defines the contract for a service that manages car sales operations.
    /// </summary>
    public interface ISaleService
    {
        /// <summary>
        /// Creates a sale record in the system based on the provided sale request details.
        /// </summary>
        /// <param name="request">The request containing the car type and distribution center ID for the sale.</param>
        void CreateSale(CreateSaleRequest request);

        /// <summary>
        /// Calculates the total sales volume across all distribution centers.
        /// </summary>
        /// <returns>The total sales volume as a decimal value.</returns>
        decimal GetTotalSalesVolume();

        /// <summary>
        /// Calculates the total sales volume for a specific distribution center.
        /// </summary>
        /// <param name="centerId">The unique identifier of the distribution center.</param>
        /// <returns>The total sales volume as a decimal value for the specified distribution center.</returns>
        decimal GetSalesVolumeByCenter(int centerId);

        /// <summary>
        /// Calculates the percentage of car sales by car type for each distribution center.
        /// </summary>
        /// <returns>A dictionary where the key represents the distribution center ID, and the value is another dictionary detailing the percentage of sales for each car type within that center.</returns>
        Dictionary<int, Dictionary<CarType, double>> GetPercentageByCenter();
    }
}