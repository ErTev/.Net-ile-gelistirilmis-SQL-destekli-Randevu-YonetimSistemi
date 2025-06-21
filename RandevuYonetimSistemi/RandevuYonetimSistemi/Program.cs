using Microsoft.EntityFrameworkCore;
using RandevuYonetimSistemi.Data;
using RandevuYonetimSistemi.Services;
using System.Net.Mail;

var builder = WebApplication.CreateBuilder(args);


builder.Services.AddControllersWithViews();

// Session servisini ekle
builder.Services.AddSession(options =>
{
    options.IdleTimeout = TimeSpan.FromMinutes(30); // Session süresi
    options.Cookie.HttpOnly = true;
    options.Cookie.IsEssential = true;
});

builder.Services.AddDbContext<RandevuDbContext>
    (o => o.UseSqlServer(builder.Configuration.GetConnectionString("defaultconnection")));

//Mail settings modelimizi konfigrasyon ayarlarý ile eþleþtirdik 
builder.Services.Configure<EmailSettings>(builder.Configuration.GetSection("EmailSettings"));
builder.Services.AddScoped<EmailService>();


var app = builder.Build();

if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();

app.UseRouting();

// Session middleware eklendi
app.UseSession();

app.UseAuthorization();

app.MapStaticAssets();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}")
    .WithStaticAssets();

app.Run();
