
using System.Net.Http.Json;
using Xunit;
using FluentAssertions;
using System.Net;
using System.Net.Http.Json;
using Microsoft.AspNetCore.Mvc.Testing;

using ApartmentRentals.Main.DTOs;
using ApartmentRentals.Main.Models;  

using System.Text;
using System.Text.Json;
using ApartmentRentals.Models;

namespace ApartmentRentals.Tests;

public class TenantApiTests : TestsBase<Program>
{
    private readonly HttpClient _client;
    private readonly string _baseUrl = "/api/tenant";

    public TenantApiTests()
    {
        _client = CreateClient();
    }

    // Тест 1: Добавление 100 элементов
    [Fact]
    public async Task Add100Tenants()
    {

        for (int i = 0; i < 100; i++)
        {
            var tenant = new Tenant
            {
                Name = "tenant",
                Email = "tenant@test.com",
                Phone = "+7000000000"
            };

            var content = new StringContent(
                JsonSerializer.Serialize(tenant),
                Encoding.UTF8,
                "application/json");

            var response = await _client.PostAsync(_baseUrl, content);
            Assert.Equal(HttpStatusCode.Created, response.StatusCode);
        }

        // Проверка: все 100 записей добавлены
        var getResponse = await _client.GetAsync(_baseUrl);
        Assert.Equal(HttpStatusCode.OK, getResponse.StatusCode);
    }

    // Тест 2: Добавление 100 000 элементов
    [Fact]
    public async Task Add100000Tenants()
    {

        for (int i = 0; i < 100000; i++)
        {
            var tenant = new Tenant
            {
                Name = "tenant",
                Email = "tenant@test.com",
                Phone = "+7000000000"
            };

            var content = new StringContent(
                JsonSerializer.Serialize(tenant),
                Encoding.UTF8,
                "application/json");

            var response = await _client.PostAsync(_baseUrl, content);
            Assert.Equal(HttpStatusCode.Created, response.StatusCode);
        }
    }

    // Тест 3: Удаление всех элементов
    [Fact]
    public async Task DeleteAllTenants()
    {
        // Удаляем всё
        var deleteResponse = await _client.DeleteAsync(_baseUrl);
        Assert.Equal(HttpStatusCode.OK, deleteResponse.StatusCode);

        // Проверяем, что база пуста
        var getResponse = await _client.GetAsync(_baseUrl);
        Assert.Equal(HttpStatusCode.OK, getResponse.StatusCode);
    }

}