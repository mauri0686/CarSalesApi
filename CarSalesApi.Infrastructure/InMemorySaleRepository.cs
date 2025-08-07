using CarSalesApi.Domain;

namespace CarSalesApi.Infrastructure
{
    public class InMemorySaleRepository
    {
        private readonly List<Sale> _sales = new List<Sale>();

        public void AddSale(Sale sale)
        {
            _sales.Add(sale);
        }

        public IEnumerable<Sale> GetAllSales()
        {
            return _sales;
        }

        public IEnumerable<Sale> GetSalesByCenter(int centerId)
        {
            return _sales.Where(s => s.DistributionCenterId == centerId);
        }
    }
}