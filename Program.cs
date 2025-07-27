using BlazorApp2_Iwankowski.Components;
using BlazorApp2_Iwankowski.Data;
using BlazorApp2_Iwankowski.Services;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddRazorComponents()
    .AddInteractiveServerComponents();

var connectionString = "Data Source=warehouse.db";
builder.Services.AddDbContext<WarehouseDbContext>(options =>
    options.UseSqlite(connectionString));

builder.Services.AddScoped<WarehouseService>();
builder.Services.AddScoped<StorageLocationService>();

var app = builder.Build();

using (var scope = app.Services.CreateScope())
{
    var db = scope.ServiceProvider.GetRequiredService<WarehouseDbContext>();
    db.Database.EnsureCreated();
}

if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseRouting();
app.UseAntiforgery();

app.MapRazorComponents<App>()
    .AddInteractiveServerRenderMode();

app.Run();
