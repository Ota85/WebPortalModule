using EsonicModule.Components;
using EsonicModule.Services;
using EsonicModule.Data;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add DbContext for SQL Server
builder.Services.AddDbContext<ZebraDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("ZebraDatabase")));

// Add DbContext for SAP Data SQL Server
builder.Services.AddDbContext<SAPDataDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("SAPDataDatabase")));

// Add services to the container.
builder.Services.AddScoped<IBarcodeService, BarcodeService>();
builder.Services.AddRazorComponents()
    .AddInteractiveServerComponents();

// Add HttpClient for API calls
var apiBaseUrl = builder.Configuration["ApiSettings:BaseUrl"] ?? "http://localhost:5107";
builder.Services.AddHttpClient<DataService>(client =>
{
    client.BaseAddress = new Uri(apiBaseUrl);
})
.ConfigurePrimaryHttpMessageHandler(() =>
{
    var handler = new HttpClientHandler();
    
    // Only disable certificate validation in development
    if (builder.Environment.IsDevelopment())
    {
        handler.ServerCertificateCustomValidationCallback = HttpClientHandler.DangerousAcceptAnyServerCertificateValidator;
    }
    
    return handler;
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error", createScopeForErrors: true);
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}
app.UseStatusCodePagesWithReExecute("/not-found");
app.UseHttpsRedirection();

app.UseAntiforgery();

app.MapStaticAssets();
app.MapRazorComponents<App>()
    .AddInteractiveServerRenderMode();

app.Run();
