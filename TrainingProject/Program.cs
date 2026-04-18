using FluentValidation;
using Microsoft.EntityFrameworkCore;
using TrainingProject.Application.Dto;
using TrainingProject.Application.Interfaces;
using TrainingProject.Application.Services;
using TrainingProject.Application.Validators;
using TrainingProject.Domain.Interfaces;
using TrainingProject.Infrastructure.Persistence;
using TrainingProject.Infrastructure.Persistence.Repositories;
using TrainingProject.Presentation.Middleware;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi

builder.Services.AddOpenApi();

var connectionString = builder.Configuration.GetConnectionString("TrainDb")
    ?? throw new InvalidOperationException("ConnectionString not found");

builder.Services.AddDbContext<TrainDbContext>(options =>
{
    options.UseSqlServer(connectionString, sql =>
    {
        sql.EnableRetryOnFailure();
    });

});

builder.Services.AddScoped<IVehicleRepository, VehicleRepository>();
builder.Services.AddScoped<IVehicleService, VehicleService>();

builder.Services.AddScoped<IValidator<CreateRequestVehicleDto>, CreateRequestVehicleDtoValidator>();


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
