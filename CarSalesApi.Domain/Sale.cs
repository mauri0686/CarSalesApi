namespace CarSalesApi.Domain
{
    public class Sale
    {
        public Guid Id { get; set; }
        public CarModel Car { get; set; }
        public int DistributionCenterId { get; set; }
        public DateTime SaleDate { get; set; }
        public decimal SalePrice { get; set; }
    }
}