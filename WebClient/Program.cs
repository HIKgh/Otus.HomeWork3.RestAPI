using System;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using WebClient.Application.Services.Implementation;
using WebClient.Application.Services.Interfaces;
using WebClient.Infrastructure.Implementation;
using WebClient.Infrastructure.Interfaces;

namespace WebClient;

static class Program
{
    private const string Url = "https://localhost:5001/customers/";

    private const string ClientName = "customer-web-api";

    private static async Task Main(string[] args)
    {
        var provider = ConfigureServices();
        var menuService = provider.GetRequiredService<IConsoleMenuService>();
        await menuService.Process();
    }

    private static IServiceProvider ConfigureServices()
    {

        var services = new ServiceCollection();
        services.AddHttpClient(ClientName, httpClient =>
        {
            httpClient.BaseAddress = new Uri(Url);
        });
        services.AddSingleton<IConsoleMenuService, ConsoleMenuService>();
        services.AddSingleton<IRandomCustomerService, RandomCustomerService>();
        services.AddSingleton<ICustomerProviderService, CustomerProviderService>();
        services.AddScoped<IRestApiCustomerService, RestApiCustomerService>();

        return services.BuildServiceProvider();
    }
}