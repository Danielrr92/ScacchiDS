using ScacchiDS.Server.WebSockets;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
builder.Services.AddSingleton<WebSocketHandler>();

var app = builder.Build();

app.UseDefaultFiles();
app.UseStaticFiles();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{

}

app.UseHttpsRedirection();

app.UseAuthorization();

app.UseWebSockets();
app.UseMiddleware<WebSocketMiddleware>();

app.MapControllers();

app.MapFallbackToFile("/index.html");

app.Run();
