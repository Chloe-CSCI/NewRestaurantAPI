using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using NewRestaurantAPI.Data;
using NewRestaurantAPI.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection") ?? throw new InvalidOperationException("Connection string 'DefaultConnection' not found.");

builder.Services.AddDbContext<ApplicationDbContext>(options =>

    options.UseSqlServer(
        builder.Configuration.GetConnectionString("DefaultConnection")));



builder.Services.AddDatabaseDeveloperPageExceptionFilter();

builder.Services.AddDefaultIdentity<IdentityUser>(options => options.SignIn.RequireConfirmedAccount = false)
   .AddEntityFrameworkStores<ApplicationDbContext>();




builder.Services.AddControllersWithViews();
builder.Services.AddScoped<ICustomerRepository, DbCustomerRepository>();
builder.Services.AddScoped<IFoodRepository, DbFoodRepository>();
builder.Services.AddScoped<ICustomerFoodRepository, DbCustomerFoodRepository>();
builder.Services.AddScoped<ITransactionsRepository, DbTransactionsRepository>();
//builder.Services.AddScoped<IUserRepository, DbUserRepository>();



var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseMigrationsEndPoint();
}
else
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();


app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");
app.MapRazorPages();

app.Run();
