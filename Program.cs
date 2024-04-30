using Microsoft.AspNetCore.Authentication;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using TechStore.Data;
using TechStore.Models.Entities;
using TechStore.Services;

var builder = WebApplication.CreateBuilder(args);

// Configure session state
builder.Services.AddSession(options =>
{
    // Set a short timeout for easy testing.
    options.IdleTimeout = TimeSpan.FromMinutes(30);
    options.Cookie.HttpOnly = true;
    options.Cookie.IsEssential = true;
});

builder.Services.AddDbContext<TechStoreDbContext>(options =>
    options.UseMySQL("Server=mysql-210770ab-techstore.b.aivencloud.com;Database=techstore;Uid=avnadmin;Pwd=AVNS_ECNjUML_9rCSuGwr_PA;Port=15039"));

// Setting up ASP.NET Core Identity
builder.Services.AddIdentity<ApplicationUser, ApplicationRole>(options => options.SignIn.RequireConfirmedAccount = true)
    .AddEntityFrameworkStores<TechStoreDbContext>()
    .AddDefaultTokenProviders();

// Optionally configure the application cookie settings if needed
builder.Services.ConfigureApplicationCookie(options =>
{
    options.LoginPath = "/Account/Login";
    options.LogoutPath = "/Account/Logout";
    // Additional options can be set here
});

// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddScoped<TechService>();
builder.Services.AddTransient<ISenderEmail, EmailSender>();

builder.Logging.AddDebug();
builder.Logging.AddConsole();

var app = builder.Build();

// Enable session state
app.UseSession();

// Initialize roles
using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;
    try
    {
        await RoleService.CreateRoles(services);
    }
    catch (Exception ex)
    {
        var logger = services.GetRequiredService<ILogger<Program>>();
        logger.LogError(ex, "An error occurred while creating roles.");
    }
}

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
