using ApartmentRentals.Data.Repositories;
using ApartmentRentals.Main.Models;
using ApartmentRentals.Main.Repositories;
using ApartmentRentals.Models;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Настраиваем авто маппер
builder.Services.AddAutoMapper(cfg =>
{
    cfg.AddProfile<MappingProfile>();
});

// Регистрируем одиночки-репозитории
// Это сделает класс Одиночкой на всё время работы сервера
builder.Services.AddSingleton<IRepository<Landlord>, LandlordRepository>();
builder.Services.AddSingleton<IRepository<RentalContract>, RentalContractRepository>();
builder.Services.AddSingleton<IRepository<Space>, SpaceRepository>();
builder.Services.AddSingleton<IRepository<Tenant>, TenantRepository>();

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
