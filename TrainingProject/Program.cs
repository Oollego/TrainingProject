using Microsoft.EntityFrameworkCore;
using TrainingProject.Application.Interfaces;
using TrainingProject.Application.Services;
using TrainingProject.Domain.Interfaces;
using TrainingProject.Infrastructure.Persistence;
using TrainingProject.Infrastructure.Persistence.Repositories;
using TrainingProject.Presentation.Middleware;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();

builder.Services.AddDbContext<TrainDbContext>(options => options.UseSqlServer(builder.Configuration.GetConnectionString("TrainDb")));

builder.Services.AddScoped<IVehicleRepository, VehicleRepository>();
builder.Services.AddScoped<IVehicleService, VehicleService>();

var app = builder.Build();
app.UseMiddleware<ExceptionMiddleware>();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();



app.Run();
