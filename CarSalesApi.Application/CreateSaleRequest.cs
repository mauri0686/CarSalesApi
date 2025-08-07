using CarSalesApi.Domain;

namespace CarSalesApi.Application
{
    /// <summary>
    /// Represents a request to create a new sale in the system.
    /// </summary>
    public class CreateSaleRequest
    {
        /// <summary>
        /// Gets or sets the type of car being sold in a sale request.
        /// This property specifies the category of the car, such as Sedan,
        /// SUV, Offroad, or Sport.
        /// </summary>
        public CarType CarType { get; set; }

        /// <summary>
        /// Gets or sets the identifier of the distribution center associated with the sale.
        /// </summary>
        /// <remarks>
        /// This property is used to associate a sale with a specific distribution center.
        /// It is an integral part of the sale creation process and relevant for tracking and reporting sales data.
        /// </remarks>
        public int DistributionCenterId { get; set; }
    }
}