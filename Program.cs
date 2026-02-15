using Microsoft.AspNetCore.Mvc;
using RMDProcessingApp.Repositories;
using RMDProcessingApp.Services;

var builder = WebApplication.CreateBuilder(args);

// MVC
builder.Services.AddControllersWithViews();

// Services
builder.Services.AddScoped<IRmdService, RmdService>();

// In‑memory repository for now
builder.Services.AddSingleton<IParticipantRepository, ParticipantRepository>();

builder.Services.AddSingleton<IRmdRepository, RmdRepository>();
builder.Services.AddSingleton<IUserRepository, UserRepository>();

builder.Services.AddSingleton<IRmdProcessingRepository, RmdProcessingRepository>();
builder.Services.AddSingleton<IPaymentRepository, PaymentRepository>();
builder.Services.AddSingleton<IAuditLogRepository, AuditLogRepository>();
builder.Services.AddSingleton<ISystemConfigurationRepository, SystemConfigurationRepository>();
builder.Services.AddSingleton<IAccountRepository, AccountRepository>();
builder.Services.AddScoped<IUniformLifetimeService, UniformLifetimeService>();

builder.Services.AddSession();
var app = builder.Build();

// Pipeline
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();
app.UseAuthorization();
app.UseSession();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Auth}/{action=Login}/{id?}");

app.MapControllers(); // for attribute-routed API controllers as well as MVC

app.Run();
