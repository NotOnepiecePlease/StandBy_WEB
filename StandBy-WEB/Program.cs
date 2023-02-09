using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using standby_data.Context;
using standby_data.Interfaces;
using standby_data.Repositories;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews().AddJsonOptions(options => options.JsonSerializerOptions.PropertyNamingPolicy = null);
string? connection = builder.Configuration.GetConnectionString("DataBase");
builder.Services.AddDbContext<standby_orgContext>(options => options.UseSqlServer(connection, b => b.MigrationsAssembly("standby-data")));
//options.UseSqlServer(builder.Configuration.GetConnectionString("DataBase")));

builder.Services.AddControllersWithViews();
builder.Services.AddScoped<IRepositoryCliente, RepositoryCliente>();
builder.Services.AddScoped<IRepositoryServico, RepositoryServico>();
builder.Services.AddSession();

Syncfusion.Licensing.SyncfusionLicenseProvider.RegisterLicense("Mgo+DSMBMAY9C3t2VVhkQlFadVdJXGFWfVJpTGpQdk5xdV9DaVZUTWY/P1ZhSXxQdkdjUX9YcHdVQWhdUk0=");
var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
  app.UseExceptionHandler("/Home/Error");
  //app.UseBrowserLink();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();
app.UseSession();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    //pattern: "{controller=DashBoard}/{action=Index}/{id?}");
    pattern: "{controller=Login}/{action=Index}/{id?}");

app.Run();