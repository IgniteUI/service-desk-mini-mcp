using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using IgApplication;
using IgApplication.Services;
using IgniteUI.Blazor.Controls;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) });

// App services
builder.Services.AddScoped<TicketService>();
builder.Services.AddScoped<NotificationService>();

// Register all Ignite UI modules needed for this app
builder.Services.AddIgniteUIBlazor(
    typeof(IgbGridModule),
    typeof(IgbPaginatorModule),
    typeof(IgbCategoryChartModule),
    typeof(IgbDataChartCoreModule),
    typeof(IgbDataChartCategoryModule),
    typeof(IgbPieChartModule),
    typeof(IgbItemLegendModule),
    typeof(IgbDialogModule),
    typeof(IgbSnackbarModule),
    typeof(IgbButtonModule),
    typeof(IgbButtonGroupModule),
    typeof(IgbInputModule),
    typeof(IgbSelectModule),
    typeof(IgbDatePickerModule),
    typeof(IgbCardModule),
    typeof(IgbBadgeModule),
    typeof(IgbChipModule),
    typeof(IgbIconModule),
    typeof(IgbIconButtonModule),
    typeof(IgbNavbarModule),
    typeof(IgbNavDrawerModule),
    typeof(IgbNavDrawerItemModule),
    typeof(IgbNavDrawerHeaderItemModule),
    typeof(IgbListModule),
    typeof(IgbListItemModule),
    typeof(IgbLinearProgressModule),
    typeof(IgbCircularProgressModule)
);

await builder.Build().RunAsync();

