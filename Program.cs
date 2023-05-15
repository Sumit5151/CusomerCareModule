using CusomerCareModule.BAL;
using CusomerCareModule.DAL;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

builder.Services.AddDbContext<CustomerCareDbContext>(x => x.UseSqlServer(
    builder.Configuration.GetConnectionString("CCDB"))  
    );

builder.Services.AddScoped<IAccountService,AccountService> ();
builder.Services.AddScoped<ICustomerCareService,CustomerCareService>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
}
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
