using EsonicModule.Components;
using EsonicModule.Services;
using EsonicModule.Data;
using Microsoft.EntityFrameworkCore;
using System.Globalization;
using Microsoft.AspNetCore.Localization;

var builder = WebApplication.CreateBuilder(args);

// Add DbContext for SAP Data SQL Server
builder.Services.AddDbContext<SAPDataDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("SAPDataDatabase")));

// Add localization services
builder.Services.AddLocalization(options => options.ResourcesPath = "Resources");

// Add AutoMapper
builder.Services.AddAutoMapper(typeof(Program));

// Add services to the container.
builder.Services.AddScoped<IMaterialStockStageService, MaterialStockStageService>();
builder.Services.AddScoped<IPrinterSettingService, PrinterSettingService>();
builder.Services.AddScoped<IZebraTemplateService, ZebraTemplateService>();
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

// Configure supported cultures - Czech as default
var supportedCultures = new[]
{
    new CultureInfo("cs-CZ"), // Czech
    new CultureInfo("en")     // English
};

app.UseRequestLocalization(new RequestLocalizationOptions
{
    DefaultRequestCulture = new RequestCulture("cs-CZ"), // Czech as default
    SupportedCultures = supportedCultures,
    SupportedUICultures = supportedCultures
});

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error", createScopeForErrors: true);
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}
app.UseStatusCodePagesWithReExecute("/not-found");


app.UsePathBase("/EsonicModule");

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAntiforgery();

app.MapStaticAssets();
app.MapRazorComponents<App>()
    .AddInteractiveServerRenderMode();

app.Run();
