using System.Net.Http.Json;
using System.Text.Json;
using Microsoft.AspNetCore.Components;
using MudBlazor;

namespace CarSales.Web.Pages;

public partial class Home : ComponentBase
{
    [Inject] private HttpClient Http { get; set; } = default!;
    [Inject] private ISnackbar Snackbar { get; set; } = default!;

    private int? _carType;
    private int? _centerId;

    private bool _saving = false;
    private bool _loadingPercentages = true;

    private string _totalSalesDisplay = "—";
    private string _rawJson = string.Empty;

    private object _dummyModel = new();

    private List<CenterPercent> _centers = new();

    protected override async Task OnInitializedAsync()
    {
        await Task.WhenAll(LoadTotalAsync(), LoadPercentagesAsync());
    }

    private async Task LoadTotalAsync()
    {
        try
        {
            var total = await Http.GetFromJsonAsync<int>("api/sales/volume/total");
            _totalSalesDisplay = total.ToString();
        }
        catch
        {
            _totalSalesDisplay = "—";
        }
        StateHasChanged();
    }

    private async Task LoadPercentagesAsync()
    {
        _loadingPercentages = true;
        StateHasChanged();

        try
        {
            var dict = await Http.GetFromJsonAsync<Dictionary<int, Dictionary<string, double>>>("api/sales/percentage/center");
            _centers = Transform(dict);
            _rawJson = JsonSerializer.Serialize(dict, new JsonSerializerOptions { WriteIndented = true });
        }
        catch (Exception ex)
        {
            Snackbar.Add("No se pudieron cargar los porcentajes", Severity.Error);
            Console.Error.WriteLine(ex);
            _centers.Clear();
            _rawJson = string.Empty;
        }
        finally
        {
            _loadingPercentages = false;
            StateHasChanged();
        }
    }

    private async Task AddSaleAsync()
    {
        if (_carType is null || _centerId is null) return;

        _saving = true;
        try
        {
            var payload = new { carType = _carType!.Value, distributionCenterId = _centerId!.Value };
            var res = await Http.PostAsJsonAsync("api/sales", payload);
            if (!res.IsSuccessStatusCode)
            {
                var text = await res.Content.ReadAsStringAsync();
                throw new InvalidOperationException(string.IsNullOrWhiteSpace(text) ? res.ReasonPhrase : text);
            }

            Snackbar.Add("¡Venta registrada con éxito!", Severity.Success);
            _carType = null;
            _centerId = null;

            await Task.WhenAll(LoadTotalAsync(), LoadPercentagesAsync());
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine(ex);
            Snackbar.Add("No se pudo registrar la venta", Severity.Error);
        }
        finally
        {
            _saving = false;
        }
    }

    private static List<CenterPercent> Transform(Dictionary<int, Dictionary<string, double>>? src)
    {
        var result = new List<CenterPercent>();
        if (src is null || src.Count == 0) return result;

        // Orden deseado de modelos
        var order = new[] { "Sedan", "SUV", "OffRoad", "Sport" };

        foreach (var kv in src.OrderBy(k => k.Key))
        {
            var centerId = kv.Key;
            var vals = kv.Value ?? new();

            // Asegurar orden y convertir posibles fracciones a %
            var labels = order.Where(k => vals.ContainsKey(k))
                              .Concat(vals.Keys.Where(k => !order.Contains(k)))
                              .ToArray();

            var raw = labels.Select(l => vals.TryGetValue(l, out var v) ? v : 0).ToArray();
            var sum = raw.Sum();
            var isFraction = sum > 0 && sum <= 1.01;
            var data = raw.Select(v => isFraction ? v * 100d : v).ToArray();

            result.Add(new CenterPercent
            {
                CenterId = centerId,
                DisplayName = $"Centro {centerId}",
                Labels = labels,
                Data = data
            });
        }
        return result;
    }

    private class CenterPercent
    {
        public int CenterId { get; set; }
        public string DisplayName { get; set; } = string.Empty;
        public string[] Labels { get; set; } = Array.Empty<string>();
        public double[] Data { get; set; } = Array.Empty<double>();
    }
}
