using AspNetCoreHero.ToastNotification;
using CakeShop.Models;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.EntityFrameworkCore;
using System.Configuration;
using System.Text.Encodings.Web;
using System.Text.Unicode;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

    builder.Services.AddDbContext<CuaHangBanhKemContext>(options => options.UseSqlServer(
    builder.Configuration.GetConnectionString("DefaultConnection")));
//builder.Services.AddDbContext<CuaHangBanhKemContext>(option => option.UseSqlServer(stringConnectdb));
//builder.Services.AddSingleton<HtmlEncoder>(HtmlEncoder.Create(allowedRanges: new[] {UnicodeRange.ReferenceEquals()}));
    builder.Services.AddSession();
    builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
                .AddCookie(p =>
                {
                    p.Cookie.Name = "UserLoginCookie";
                    p.ExpireTimeSpan = TimeSpan.FromDays(1);
                    //p.LoginPath = "/dang-nhap.html";
                    //p.LogoutPath = "/dang-xuat/html";
                    p.AccessDeniedPath = "/not-found.html";
                });

builder.Services.AddControllersWithViews().AddRazorRuntimeCompilation();
    builder.Services.AddNotyf(config => { config.DurationInSeconds = 10; config.IsDismissable = true; config.Position = NotyfPosition.BottomRight; });




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
app.UseSession();
app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();

app.UseEndpoints(endpoints =>
{
    endpoints.MapControllerRoute(
      name: "areas",
      pattern: "{area:exists}/{controller=Home}/{action=Index}/{id?}"
    );
    endpoints.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");
});


app.Run();
