using CarSalesApi.Domain;

namespace CarSalesApi.Infrastructure
{
    /// <summary>
    /// Interface that defines the repository for managing sales records.
    /// Provides methods to add, retrieve, and filter sales data.
    /// </summary>
    public interface ISaleRepository
    {
        /// <summary>
        /// Adds a new sale record to the repository.
        /// </summary>
        /// <param name="sale">The sale record to be added, containing details such as car model, distribution center ID, sale date, and sale price.</param>
        void AddSale(Sale sale);

        /// <summary>
        /// Retrieves all sales records stored in the repository.
        /// </summary>
        /// <returns>An enumerable collection of all sales records.</returns>
        IEnumerable<Sale> GetAllSales();

        /// <summary>
        /// Retrieves all sales records associated with a specific distribution center.
        /// </summary>
        /// <param name="centerId">The unique identifier of the distribution center whose sales data is to be retrieved.</param>
        /// <returns>A collection of sales records associated with the specified distribution center.</returns>
        IEnumerable<Sale> GetSalesByCenter(int centerId);
    }
}