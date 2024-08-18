using CustomerManagement.Models;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// TODO: ler o nome da connection string do appsettings.json 
var connectionString =
builder.Configuration.GetConnectionString("CustomerManagement_ConnectionString");

// TODO: registar o serviço da EF 
builder.Services.AddDbContext<CustomerManagement_DBContext>(options =>
options.UseSqlServer(connectionString));

// Add services to the container.
builder.Services.AddControllersWithViews();

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
