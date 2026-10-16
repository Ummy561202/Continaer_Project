using Continaer_Project.Services;
using Azure.Identity;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddRazorPages();

// One shared in-memory store for the whole app. Swap this line for an EF Core
// implementation of IPropertyService when you want data to survive restarts.
builder.Services.AddSingleton<IPropertyService, InMemoryPropertyService>();
builder.Services.AddApplicationInsightsTelemetry(new Microsoft.ApplicationInsights.AspNetCore.Extensions.ApplicationInsightsServiceOptions
{
    ConnectionString = builder.Configuration["APPLICATIONINSIGHTS_CONNECTION_STRING"]
});

var app = builder.Build();

if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseRouting();
app.UseAuthorization();

app.MapStaticAssets();
app.MapRazorPages().WithStaticAssets();

app.Run();
