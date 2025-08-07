namespace CarSalesApi.Domain
{
    /// <summary>
    /// Represents a sale transaction for a car within a distribution center.
    /// Contains details such as the car model, distribution center ID, sale date, and sale price.
    /// </summary>
    public class Sale
    {
        /// <summary>
        /// Gets or sets the unique identifier for the sale transaction.
        /// This property is used to differentiate each sale within the system.
        /// </summary>
        public Guid Id { get; set; }

        /// <summary>
        /// Gets or sets the car model associated with the sale transaction.
        /// This property links to the specific car being sold and its model details,
        /// such as type and base price.
        /// </summary>
        public CarModel Car { get; set; }

        /// <summary>
        /// Gets or sets the identifier for the distribution center where the sale occurred.
        /// This property is used to associate a sale with a specific location within the system.
        /// </summary>
        public int DistributionCenterId { get; set; }

        /// <summary>
        /// Gets or sets the date and time when the sale occurred.
        /// This property records the timestamp for when a sale transaction is completed.
        /// </summary>
        public DateTime SaleDate { get; set; }

        /// <summary>
        /// Gets or sets the price at which the car is sold.
        /// This property represents the final sale amount calculated based on the car type and other factors.
        /// </summary>
        public decimal SalePrice { get; set; }
    }
}