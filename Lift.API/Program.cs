using Lift.API.Data;
using Lift.API.Services;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddScoped<ElevatorService>();
builder.Services.AddHostedService<ElevatorBackgroundService>();


builder.Services.AddDbContext<ElevatorDbContext>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection")));

var app = builder.Build();

using (var scope = app.Services.CreateScope())
{
    var dbContext = scope.ServiceProvider.GetRequiredService<ElevatorDbContext>();
    dbContext.Database.Migrate();
}


app.UseSwagger();
app.UseSwaggerUI();
app.MapControllers();
app.Run();
