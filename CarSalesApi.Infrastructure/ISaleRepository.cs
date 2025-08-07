using CarSalesApi.Domain;

namespace CarSalesApi.Infrastructure
{
    public interface ISaleRepository
    {
        void AddSale(Sale sale);
        IEnumerable<Sale> GetAllSales();
        IEnumerable<Sale> GetSalesByCenter(int centerId);
    }
}