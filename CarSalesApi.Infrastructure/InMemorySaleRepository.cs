using CarSalesApi.Domain;

namespace CarSalesApi.Infrastructure
{
    /// <summary>
    /// Represents an in-memory repository for managing sales data.
    /// This class serves as a lightweight implementation of a repository, storing sales
    /// in a collection within memory for quick access and manipulation throughout the application.
    /// </summary>
    public class InMemorySaleRepository
    {
        private readonly List<Sale> _sales = new List<Sale>();

        /// <summary>
        /// Adds a new sale to the in-memory sales repository.
        /// </summary>
        /// <param name="sale">The sale object to be added to the repository. Contains details such as car model, distribution center, sale date, and sale price.</param>
        public void AddSale(Sale sale)
        {
            _sales.Add(sale);
        }

        /// <summary>
        /// Retrieves all sales stored in the in-memory sales repository.
        /// </summary>
        /// <returns>A collection of sale objects containing details such as car model, distribution center, sale date, and sale price.</returns>
        public IEnumerable<Sale> GetAllSales()
        {
            return _sales;
        }

        /// <summary>
        /// Retrieves all sales associated with a specific distribution center from the in-memory sales repository.
        /// </summary>
        /// <param name="centerId">The unique identifier of the distribution center whose sales are to be retrieved.</param>
        /// <returns>A collection of sale objects filtered by the specified distribution center, including details such as car model, sale date, and sale price.</returns>
        public IEnumerable<Sale> GetSalesByCenter(int centerId)
        {
            return _sales.Where(s => s.DistributionCenterId == centerId);
        }
    }
}