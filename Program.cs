using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Hosting.StaticWebAssets;
using Portfolio.Data;
using MudBlazor.Services;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Blazored.LocalStorage;


var builder = WebApplication.CreateBuilder(args);
var connectionString = builder.Configuration.GetConnectionString("PortfolioContextConnection") ?? throw new InvalidOperationException("Connection string 'PortfolioContextConnection' not found.");
var serverVersion = ServerVersion.AutoDetect(connectionString);

builder.Services.AddDbContext<PortfolioContext>(options =>
    options.UseMySql(connectionString, serverVersion));

builder.Services.AddDefaultIdentity<IdentityUser>(options => { options.SignIn.RequireConfirmedAccount = true; options.Password.RequiredLength = 4; options.Password.RequireNonAlphanumeric = false; options.Password.RequireDigit = false; options.SignIn.RequireConfirmedEmail = false; options.SignIn.RequireConfirmedPhoneNumber = false;options.SignIn.RequireConfirmedAccount = false; options.Password.RequireUppercase = false; })
    .AddEntityFrameworkStores<PortfolioContext>();

StaticWebAssetsLoader.UseStaticWebAssets(builder.Environment, builder.Configuration);

// Add services to the container.
builder.Services.AddRazorPages();
builder.Services.AddBlazoredLocalStorage();   // local storage
builder.Services.AddBlazoredLocalStorage(config => config.JsonSerializerOptions.WriteIndented = true);  // local storage
builder.Services.AddServerSideBlazor();
builder.Services.AddSingleton<WeatherForecastService>();
builder.Services.AddMudServices();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();

app.UseStaticFiles();

app.UseRouting();

app.MapBlazorHub();
app.MapFallbackToPage("/_Host");
app.UseAuthentication();;

app.Run();