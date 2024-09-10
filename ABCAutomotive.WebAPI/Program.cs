using ABCAutomotive.WebAPI.Application.Commands.Handlers;
using ABCAutomotive.WebAPI.Application.Queries.Handlers;
using ABCAutomotive.WebAPI.Application.Services;
using ABCAutomotive.WebAPI.Core.Interfaces;
using ABCAutomotive.WebAPI.Infrastructure.Persistence;
using ABCAutomotive.WebAPI.Infrastructure.Persistence.Repositories;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers()
    .AddJsonOptions(options =>
    {
        options.JsonSerializerOptions.ReferenceHandler = System.Text.Json.Serialization.ReferenceHandler.IgnoreCycles;
        options.JsonSerializerOptions.DefaultIgnoreCondition = System.Text.Json.Serialization.JsonIgnoreCondition.WhenWritingNull;
    });

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// 3. Configure DbContext (SQL Server)
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

// Register the car price adjustment services
builder.Services.AddScoped<RegularCarPriceAdjustmentService>();
builder.Services.AddScoped<SuvPriceAdjustmentService>();
builder.Services.AddScoped<ConvertiblePriceAdjustmentService>();
builder.Services.AddScoped<RedCarPriceAdjustmentService>();

// Register the factory
builder.Services.AddScoped<CarPriceAdjustmentServiceFactory>();

// Register MediatR 
builder.Services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(typeof(CreateSaleCommandHandler).Assembly));
builder.Services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(typeof(GetSaleDetailsQueryHandler).Assembly));
builder.Services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(typeof(GetSalesQueryHandler).Assembly));

// 4. Register Repositories
builder.Services.AddScoped<ICarRepository, CarRepository>();
builder.Services.AddScoped<ISaleRepository, SaleRepository>();

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll",
        policy =>
        {
            policy.AllowAnyOrigin()
                .AllowAnyMethod()
                .AllowAnyHeader();
        });
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(options =>
    {
        options.SwaggerEndpoint("/swagger/v1/swagger.json", "ABC Automotive API V1");
        options.RoutePrefix = string.Empty;
    });
}

// Enable CORS
app.UseCors("AllowAll");


app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();

app.Run();