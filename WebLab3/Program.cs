using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllersWithViews();

var app = builder.Build();

app.UseStaticFiles();

app.UseDefaultFiles();
app.MapGet("/", async context =>
{
    context.Response.Redirect("/examples/dashboard.html");
});

app.MapControllers();

app.Run();
