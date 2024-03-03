using Microsoft.AspNetCore.Authentication;
using Microsoft.EntityFrameworkCore;
using TechStore.Data;
using TechStore.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddDbContext<TechStoreDbContext>(options =>
    options.UseMySQL("Server=localhost;Database=TechStore;Uid=root;Pwd=RmisSexybeast12.;"));
builder.Services.AddScoped<TechService>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts(); // Consider configuring HSTS as appropriate for your production environment.
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Tech}/{action=Homepage}/{id?}");

app.Run();