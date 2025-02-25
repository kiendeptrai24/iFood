using iFood.Data;
using iFood.Helpers;
using iFood.Interfaces;
using iFood.Models;
using iFood.Models.Momo;
using iFood.Service;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Repository.Interfaces;

var builder = WebApplication.CreateBuilder(args);
//Momo API Payment
builder.Services.Configure<MomoOptionModel>(builder.Configuration.GetSection("MomoAPI"));
builder.Services.AddScoped<IMomoService, MomoService>();

// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddScoped<IMomoRepository,MomoRepository>();
builder.Services.AddScoped<IProductRepository,ProductRepository>();
builder.Services.AddScoped<ICartRepository,CartRepository>();
builder.Services.AddScoped<IPhotoService,PhotoService>();

builder.Services.Configure<CloudinarySetting>(builder.Configuration.GetSection("CloudinarySettings"));



builder.Services.AddDbContext<ApplicationDBContext>( options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString( "DefaultConnection" ) );
});
builder.Services.AddIdentity<AppUser,IdentityRole>().AddEntityFrameworkStores<ApplicationDBContext>();
builder.Services.AddMemoryCache();
builder.Services.AddSession();
builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
.AddCookie();


var app = builder.Build();
if (args.Length == 1 && args[0].ToLower() == "seeddata")
{
    //await Seed.SeedUsersAndRolesAsync(app);
    Seed.SeedData(app);
}
// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseRouting();

app.UseAuthorization();

app.MapStaticAssets();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}")
    .WithStaticAssets();


app.Run();
