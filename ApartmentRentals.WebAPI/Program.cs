using ApartmentRentals.Data.Repositories;
using ApartmentRentals.Data.Models;
using ApartmentRentals.Models;

using SpaceStoreApi.Services;
using ApartmentRentals.WebAPI.Services;

[assembly: System.Runtime.CompilerServices.InternalsVisibleTo("ApartmentRentals.Tests")]

    var builder = WebApplication.CreateBuilder(args);

    // Add services to the container.
    builder.Services.Configure<MongoDbContext>(
        builder.Configuration.GetSection("SpaceStoreDatabase"));

    builder.Services.AddSingleton<SpaceService>();
    builder.Services.AddSingleton<RentalContractService>();
    builder.Services.AddSingleton<TenantService>();
    builder.Services.AddSingleton<LandlordService>();

    builder.Services.AddControllers()
        .AddJsonOptions(
            options => options.JsonSerializerOptions.PropertyNamingPolicy = null);

    // Add services to the container.

    //builder.Services.AddControllers();
    // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
    builder.Services.AddEndpointsApiExplorer();
    builder.Services.AddSwaggerGen();

    // ����������� ���� ������
    builder.Services.AddAutoMapper(cfg =>
    {
        cfg.AddProfile<MappingProfile>();
    });

    // �� ������� Async � ��������
    builder.Services.AddControllers(options =>
    {
        options.SuppressAsyncSuffixInActionNames = false;
    });

    // ������������ ��������-�����������
    // ��� ������� ����� ��������� �� �� ����� ������ �������
    //builder.Services.AddSingleton<IRepository<Landlord>, LandlordRepository>();
    //builder.Services.AddSingleton<IRepository<RentalContract>, RentalContractRepository>();
    //builder.Services.AddSingleton<IRepository<Space>, SpaceRepository>();
    //builder.Services.AddSingleton<IRepository<Tenant>, TenantRepository>();

    var app = builder.Build();

    // Configure the HTTP request pipeline.
    if (app.Environment.IsDevelopment())
    {
        app.UseSwagger();
       // app.UseSwaggerUI();
     app.UseSwaggerUI(options =>
        {
            options.SwaggerEndpoint("/openapi/v1.json", "v1");
        });
    }

    app.UseHttpsRedirection();

    app.UseAuthorization();

    app.MapControllers();

    app.Run();

public partial class Program { }