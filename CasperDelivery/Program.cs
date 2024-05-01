using Blazored.Toast;
using CasperDelivery.Areas.Identity;
using CasperDelivery.Data;
using CasperDelivery.Data.Models;
using CasperDelivery.EmailStuff;
using CasperDelivery.Interfaces;
using CasperDelivery.Interfaces.Payment;
using CasperDelivery.Interfaces.Repositories;
using CasperDelivery.Repositories;
using CasperDelivery.Services;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.EntityFrameworkCore;
using Stripe;

var builder = WebApplication.CreateBuilder(args);
var configuration = builder.Configuration;

// Add services to the container.
builder.Services.AddRazorPages();
builder.Services.AddServerSideBlazor();
builder.Services.AddControllers();


builder.Services.AddDefaultIdentity<AppUser>(options => options.SignIn.RequireConfirmedAccount = true)
    .AddEntityFrameworkStores<AppDbContext>();

builder.Services.AddDbContext<AppDbContext>(x =>
{
    x.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));
}, ServiceLifetime.Transient);

//Stripe
StripeConfiguration.ApiKey = configuration["StripeSettings:SecretKey"];
//Services
builder.Services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));
builder.Services.AddScoped<IPaymentService, PaymentService>();
builder.Services.AddTransient<ICartService, CartService>();
builder.Services.AddScoped<IOrdersRepository, OrdersRepository>();
builder.Services.AddScoped<ICartRepository, CartRepository>();
builder.Services.AddScoped<IAddressRepository,AddressRepository>();
builder.Services.AddScoped<AuthenticationStateProvider, RevalidatingIdentityAuthenticationStateProvider<IdentityUser>>();
builder.Services.AddTransient<NavBarService>();
builder.Services.AddTransient<IEmailSender, EmailSender>();
builder.Services.AddBlazoredToast();
builder.Services.Configure<AuthMessageSenderOptions>(builder.Configuration);
builder.Services.AddHttpClient();

builder.Services.AddAuthentication().AddGoogle(googleOptions =>
 {
     googleOptions.ClientId = configuration["Authentication:Google:ClientId"];
     googleOptions.ClientSecret = configuration["Authentication:Google:ClientSecret"];
 });

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();

app.UseStaticFiles();

app.UseRouting();
app.UseAuthorization();

app.MapControllers();
app.MapBlazorHub();
app.MapFallbackToPage("/_Host");

using var scope = app.Services.CreateScope();
var services = scope.ServiceProvider;
var context = services.GetRequiredService<AppDbContext>();
var logger = services.GetRequiredService<ILogger<Program>>();
try
{
    await context.Database.MigrateAsync();
    await SeedContext.SeedAsync(context);
}
catch (Exception ex)
{
    logger.LogError(ex, "An error ocurred during migration");
}

app.Run();