using Microsoft.AspNetCore.Components;
using System.Net.Http.Json;

namespace CarSales.Web.Pages;

public partial class Percentages : ComponentBase {
    private Dictionary<int, Dictionary<string, double>>? data;
    private string[] labels = Array.Empty<string>();
    private double[] sedan = Array.Empty<double>();
    private double[] suv = Array.Empty<double>();

    protected override async Task OnInitializedAsync()
    {
        data = await Http.GetFromJsonAsync<Dictionary<int, Dictionary<string, double>>>("api/sales/percentage/center");
        if (data != null)
        {
            labels = data.Keys.Select(k => $"Centro {k}").ToArray();
            sedan = data.Values.Select(v => v.ContainsKey("Sedan") ? v["Sedan"] : 0).ToArray();
            suv = data.Values.Select(v => v.ContainsKey("SUV") ? v["SUV"] : 0).ToArray();
        }
    }
}