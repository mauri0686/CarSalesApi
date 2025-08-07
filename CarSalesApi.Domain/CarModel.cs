namespace CarSalesApi.Domain
{
    /// <summary>
    /// Represents a car model with specific attributes such as type and base price.
    /// </summary>
    public class CarModel
    {
        /// <summary>
        /// Gets or sets the type of the car model.
        /// </summary>
        /// <remarks>
        /// The type determines the category of the car, such as Sedan, SUV, Offroad, or Sport,
        /// and is used to establish characteristics like the base price and other attributes.
        /// </remarks>
        public CarType Type { get; set; }

        /// <summary>
        /// Gets or sets the base price of the car model.
        /// </summary>
        /// <remarks>
        /// The base price is determined based on the car type and serves as the standard price
        /// before any applicable adjustments or discounts.
        /// </remarks>
        public decimal BasePrice { get; set; }

        /// <summary>
        /// Represents a car model with specific attributes such as type and base price.
        /// </summary>
        public CarModel(CarType type)
        {
            Type = type;
            BasePrice = GetBasePrice(type);
        }

        /// <summary>
        /// Determines the base price of a car based on its type.
        /// </summary>
        /// <param name="type">The type of the car for which the base price is being determined.</param>
        /// <returns>The base price of the car as a decimal value.</returns>
        /// <exception cref="ArgumentException">Thrown when an invalid car type is provided.</exception>
        private decimal GetBasePrice(CarType type)
        {
            return type switch
            {
                CarType.Sedan => 8000m,
                CarType.SUV => 9500m,
                CarType.Offroad => 12500m,
                CarType.Sport => 18200m,
                _ => throw new ArgumentException("Invalid car type")
            };
        }

        /// <summary>
        /// Calculates the sale price of the car model based on its type and applicable adjustments.
        /// </summary>
        /// <returns>The sale price of the car as a decimal value.</returns>
        public decimal GetSalePrice()
        {
            return Type == CarType.Sport ? BasePrice * 1.07m : BasePrice;
        }
    }
}