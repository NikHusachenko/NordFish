using FluentValidation;
using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.EntityFrameworkCore;
using NordFish.EntityFramework;
using NordFish.EntityFramework.Repository;
using NordFish.Services.UserServices;
using NordFish.Services.UserServices.Models;
using NordFishServices.AuthenticationServices;
using NordFishServices.ProductServices;
using NordFishServices.UserServices;
using NordFishServices.UserServices.Models;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

var services = builder.Services;

services.AddMvc()
    .AddFluentValidation();

string connectionString = builder.Configuration.GetSection("ConnectionStrings:DefaultConnection").Value;
services.AddDbContext<NordFishDbContext>(options => options.UseSqlServer(connectionString));
services.AddTransient(typeof(IGenericRepository<>), typeof(GenericRepository<>));
services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
    .AddCookie(options =>
    {
        options.AccessDeniedPath = "/User/SignIn";
        options.LogoutPath = "/User/SignIn";
        options.LoginPath = "/User/SignIn";
        options.ExpireTimeSpan = TimeSpan.FromDays(3);
    });

services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
services.AddTransient<IAuthenticationServices, AuthenticationServices>();
services.AddTransient<IUserService, UserService>();
services.AddScoped<ICurrentUser, CurrentUser>();
services.AddTransient<IProductServices, ProductServices>();

services.AddTransient<IValidator<SignUpViewModel>, SignUpValidator>();
services.AddTransient<IValidator<SignInViewModel>, SignInValidator>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();
app.UseAuthentication();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
