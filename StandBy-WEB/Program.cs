using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Identity;
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

builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme).AddCookie(option =>
{
  option.LoginPath = "/Login/Index";
  // option.AccessDeniedPath = "/Login/Index";
  // option.Cookie.Name = "StandBy";
  // option.Cookie.HttpOnly = true;
  // option.Cookie.SameSite = Microsoft.AspNetCore.Http.SameSiteMode.Strict;
  // option.Cookie.SecurePolicy = Microsoft.AspNetCore.Http.CookieSecurePolicy.Always;
  option.ExpireTimeSpan = TimeSpan.FromMinutes(30);
  // option.SlidingExpiration = true;
  // option.ReturnUrlParameter = CookieAuthenticationDefaults.ReturnUrlParameter;
});


builder.Services.AddScoped<IRepositoryCliente, RepositoryCliente>();
builder.Services.AddScoped<IRepositoryServico, RepositoryServico>();

// builder.Services.AddIdentity<IdentityUser, IdentityRole>().AddEntityFrameworkStores<standby_orgContext>().AddDefaultTokenProviders();

// builder.Services.AddScoped<SignInManager<IdentityUser>, SignInManager<IdentityUser>>();


// builder.Services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();


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

app.UseAuthentication();
app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Login}/{action=Index}/{id?}");

app.Run();