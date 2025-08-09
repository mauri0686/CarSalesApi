using System.Net.Http.Json;

namespace CarSales.Web.Pages;

public partial class Sales {
    Dictionary<string, int>? sales;

    protected override async Task OnInitializedAsync()
    {
        sales = new Dictionary<string, int>();

        for (int centerId = 1; centerId <= 4; centerId++)
        {
            var count = await Http.GetFromJsonAsync<int>($"api/sales/volume/center/{centerId}");
            sales.Add($"Centro {centerId}", count);
        }
    }
}