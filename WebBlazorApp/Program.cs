using Abc.BusinessService;
using ABC.Entities.Interfaces;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Components.Server.ProtectedBrowserStorage;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.EntityFrameworkCore;
using WebBlazorApp.Data;
using WebBlazorApp.Filters;
using Abc.Infrastructure;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorPages();
builder.Services.AddServerSideBlazor();

builder.Services.AddAuthenticationCore();
builder.Services.AddScoped<ProtectedSessionStorage>();
builder.Services.AddScoped<AuthenticationStateProvider, BlazorAuthenticationStateProvider>();
builder.Services.AddSingleton<IUserInfoService, UserInfoService>();

builder.Services.RegisterApplicationLayer();
builder.Services.RegisterInfrastructure(builder.Configuration);

builder.Services.AddSingleton<WeatherForecastService>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}
else
{
    app.UseExceptionHandler(new ExceptionHandlerOptions { ExceptionHandler = new CustomExceptionHandler().Invoke });
}

app.UseHttpsRedirection();

app.UseStaticFiles();
app.UseAuthorization();
app.UseRouting();

app.MapBlazorHub();
app.MapFallbackToPage("/_Host");

app.Run();
