// TestsBase.cs
using System.Net.Http.Json;
using Xunit;
using FluentAssertions;
using System.Net;
using System.Net.Http.Json;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.AspNetCore.Hosting; 
using ApartmentRentals.Main.DTOs; 
using ApartmentRentals.Main.Models;
namespace ApartmentRentals.Tests;

public class TestsBase<T> : WebApplicationFactory<T> where T : class
{
    protected override void ConfigureWebHost(IWebHostBuilder builder)
    {
        builder.ConfigureServices(services =>
        {
        });
    }
}