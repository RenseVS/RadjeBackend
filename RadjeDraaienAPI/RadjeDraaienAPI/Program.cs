using Microsoft.AspNetCore.Server.Kestrel.Core;
using RadjeDraaienAPI;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddSingleton<SocketService>();
builder.Services.AddSingleton<WheelData>();
builder.WebHost.ConfigureKestrel(options =>
{
    var port = builder.Environment.IsDevelopment() ? 7021 : 80;
    options.ListenAnyIP(port, listenOptions => listenOptions.UseHttps());
});

builder.Services.AddCors(options =>
{
    options.AddDefaultPolicy(
        policy =>
        {
            policy.WithOrigins("http://localhost:8080").AllowAnyMethod().AllowAnyHeader().AllowCredentials(); ;
        });
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseCors();

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();



app.Run();

