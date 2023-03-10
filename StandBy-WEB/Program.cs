using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore;
using standby_data.AutoMapper;
using standby_data.Context;
using standby_data.Interfaces;
using standby_data.Repositories;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews()
    .AddJsonOptions(options => options.JsonSerializerOptions.PropertyNamingPolicy = null);


string? connection = builder.Configuration.GetConnectionString("DataBase");

builder.Services.AddDbContext<standby_orgContext>(options =>
    options.UseSqlServer(connection, b => b.MigrationsAssembly("standby-data")));

//options.UseSqlServer(builder.Configuration.GetConnectionString("DataBase")));

builder.Services.AddControllersWithViews();


// builder.Services.AddAuthorization(options =>
// {
//   options.FallbackPolicy = new AuthorizationPolicyBuilder()
//     .RequireAuthenticatedUser()
//     .Build();
//   // options.AddPolicy("Admin", policy => policy.RequireClaim("Admin"));

// });


builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme).AddCookie(option =>
{
  option.AccessDeniedPath = "/Error/Negado";
  option.LoginPath = "/Login/Index";
  option.Cookie.Name = "StandBy";
  option.Cookie.HttpOnly = true;
  option.Cookie.SameSite = Microsoft.AspNetCore.Http.SameSiteMode.Strict;
  option.Cookie.SecurePolicy = Microsoft.AspNetCore.Http.CookieSecurePolicy.Always;
  option.ExpireTimeSpan = TimeSpan.FromMinutes(60);
  option.SlidingExpiration = true;

  // option.ReturnUrlParameter = CookieAuthenticationDefaults.ReturnUrlParameter;
});


builder.Services.AddScoped<IRepositoryCliente, RepositoryCliente>();
builder.Services.AddScoped<IRepositoryServico, RepositoryServico>();
builder.Services.AddScoped<IRepositoryLogin, RepositoryLogin>();
builder.Services.AddAutoMapper(typeof(MappingModels));

builder.Services.AddSession();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
  app.UseExceptionHandler("/Home/Error");
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();
app.UseSession();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=DashBoard}/{action=Index}/{id?}");

app.Run();