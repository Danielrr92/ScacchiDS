using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.ApplicationModels;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using ScacchiDS.Server.Data;
using ScacchiDS.Server.Models;
using ScacchiDS.Server.Services;
using ScacchiDS.Server.WebSockets;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddDbContext<ApplicationDbContext>(options => options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));
builder.Services.AddIdentity<ApplicationUser, IdentityRole>().AddEntityFrameworkStores<ApplicationDbContext>().AddDefaultTokenProviders();
builder.Services.AddScoped<GameService>();
builder.Services.AddControllers();

// Aggiungi il logging integrato (di default � configurato)
builder.Logging.ClearProviders(); // Rimuove i logger di default
builder.Logging.AddConsole();    // Aggiunge il logger sulla console
builder.Logging.AddDebug();      // Aggiunge il logger per il debug
builder.Logging.AddEventSourceLogger(); // Logger per eventi di sistema (opzionale)

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();


builder.Services.AddControllersWithViews();
builder.Services.AddSingleton<WebSocketHandler>();
builder.Services.AddCors(options =>
{
    options.AddDefaultPolicy(policy =>
    {
        policy.WithOrigins("https://localhost:5173")
              .AllowAnyHeader()
              .AllowAnyMethod();
    });
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}


app.UseDefaultFiles();
app.UseStaticFiles();
app.UseHttpsRedirection();
app.UseRouting();
app.UseCors();
app.UseAuthentication();
app.UseAuthorization();
app.UseWebSockets();
app.UseMiddleware<WebSocketMiddleware>();
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Auth}/{action=LoginAdministrator}/{id?}"); // Imposta il routing principale

app.MapControllers();

app.Run();
