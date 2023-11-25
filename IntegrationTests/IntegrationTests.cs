using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Persistence;

namespace IntegrationTests;

public class IntegrationTests
{
    protected readonly HttpClient TestClient;

    protected IntegrationTests()
    {
            
        var appFactory = new WebApplicationFactory<Program>()
            .WithWebHostBuilder(builder =>
            {
                builder.ConfigureAppConfiguration((hostingContext, config) =>
                {
                    var env = hostingContext.HostingEnvironment;
                    config.AddJsonFile("appsettings.test.json", optional: false, reloadOnChange: true)
                        .AddEnvironmentVariables();
                }).ConfigureServices(services =>
                {
                    services.RemoveAll(typeof(ApplicationDbContext));
                    services.AddDbContext<ApplicationDbContext>(options =>
                    {
                        options.UseInMemoryDatabase("TestDb");
                    });
                });
            });
        
        TestClient = appFactory.CreateClient();
    }
}