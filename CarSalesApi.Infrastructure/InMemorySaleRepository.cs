using CarSalesApi.Domain;

namespace CarSalesApi.Infrastructure
{
    /// <summary>
    /// A repository implementation for managing sales records in memory.
    /// </summary>
    public class InMemorySaleRepository : ISaleRepository
    {
        /// <summary>
        /// Represents an in-memory collection of sale records.
        /// This collection is used to store and manage sale transactions
        /// related to car sales within the repository.
        /// </summary>
        private readonly List<Sale> _sales = new List<Sale>();

        /// <summary>
        /// Adds a new sale record to the repository.
        /// </summary>
        /// <param name="sale">The sale record to be added.</param>
        public void AddSale(Sale sale)
        {
            _sales.Add(sale);
        }

        /// <summary>
        /// Retrieves all sales records stored in the repository.
        /// </summary>
        /// <returns>An enumerable collection of sales records.</returns>
        public IEnumerable<Sale> GetAllSales()
        {
            return _sales;
        }

        /// <summary>
        /// Retrieves all sales records associated with a specific distribution center.
        /// </summary>
        /// <param name="centerId">The unique identifier of the distribution center whose sales are to be retrieved.</param>
        /// <returns>A collection of sales records belonging to the specified distribution center.</returns>
        public IEnumerable<Sale> GetSalesByCenter(int centerId)
        {
            return _sales.Where(s => s.DistributionCenterId == centerId);
        }
    }
}