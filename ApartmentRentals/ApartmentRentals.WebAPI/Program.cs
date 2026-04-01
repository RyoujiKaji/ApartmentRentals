using ApartmentRentals.Data.Repositories;
using ApartmentRentals.Data.Models;
using ApartmentRentals.Models;

using SpaceStoreApi.Services;
using ApartmentRentals.WebAPI.Services;

[assembly: System.Runtime.CompilerServices.InternalsVisibleTo("ApartmentRentals.Tests")]

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

//builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Настраиваем авто маппер
builder.Services.AddAutoMapper(cfg =>
{
    cfg.AddProfile<MappingProfile>();
});

// Не отрезай Async в названии
builder.Services.AddControllers(options =>
{
    options.SuppressAsyncSuffixInActionNames = false;
});

// Add services to the container.
builder.Services.Configure<MongoDbContext>(
    builder.Configuration.GetSection("SpaceStoreDatabase"));

builder.Services.AddSingleton<IRepository<Landlord>, LandlordService>();
builder.Services.AddSingleton<IRepository<RentalContract>, RentalContractService>();
builder.Services.AddSingleton<IRepository<Space>, SpaceService>();
builder.Services.AddSingleton<IRepository<Tenant>, TenantService>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();

public partial class Program { }



