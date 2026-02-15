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

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
