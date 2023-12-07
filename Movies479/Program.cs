using Business;
using Business.Services;
using DataAccess.Contexts;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddDbContext<Db>(options => options.UseSqlServer("server=(localdb)\\mssqllocaldb;database=Movies479;trusted_connection=true;"));

builder.Services.AddScoped<IMovieService, MovieService>();
builder.Services.AddScoped<IDirectorService, DirectorService>();
builder.Services.AddScoped<IGenreService, GenreService>();
builder.Services.AddScoped<IUserService, UserService>();


// Add services to the container.
builder.Services.AddControllersWithViews();

#region
builder.Services
	.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
	.AddCookie(config =>
	{
		config.LoginPath = "/Account/Login";//giriþ yapýlmadan iþlem yapýlýrsa yönlendir
		config.AccessDeniedPath = "/Account/AccessDenied";//giriþ yaptýktan sonra yetki dýþý iþlem
		config.ExpireTimeSpan = TimeSpan.FromMinutes(15);
		config.SlidingExpiration = true;
	});
#endregion

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

app.UseAuthentication();
app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
