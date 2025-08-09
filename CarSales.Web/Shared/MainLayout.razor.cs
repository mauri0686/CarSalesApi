using Microsoft.AspNetCore.Components;
using MudBlazor;

namespace CarSales.Web.Shared
{
    public partial class MainLayout : LayoutComponentBase
    {
        [Inject] public NavigationManager NavigationManager { get; set; } = default!;

        protected bool _isDarkMode;
        protected bool _drawerOpen = true;
        protected string? _search;

        protected MudTheme _theme = new()
        {
            PaletteLight = new PaletteLight
            {
                Primary = Colors.Red.Accent3,
                Secondary = Colors.Gray.Default,
                Background = "#f7f7f9",
                Surface = "#ffffff",
                AppbarBackground = Colors.Red.Darken3,
                AppbarText = Colors.Shades.White
            },
            PaletteDark = new PaletteDark
            {
                Primary = Colors.Red.Lighten1,
                Secondary = Colors.Gray.Default,
                Background = "#121212",
                Surface = "#1e1e1e",
                AppbarBackground = "#1a1a1a",
                AppbarText = Colors.Shades.White
            },
            LayoutProperties = new LayoutProperties
            {
                DrawerWidthLeft = "260px"
            }
        };

        protected void ToggleTheme() => _isDarkMode = !_isDarkMode;
        protected void ToggleDrawer() => _drawerOpen = !_drawerOpen;
        protected void Refresh() => NavigationManager.NavigateTo(NavigationManager.Uri, forceLoad: false);
    }
}
