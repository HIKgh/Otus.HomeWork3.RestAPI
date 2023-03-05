using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text.Json;
using System.Threading.Tasks;
using WebClient.Application.Models;
using WebClient.Application.Services.Interfaces;

namespace WebClient.Application.Services.Implementation;

public class RestApiCustomerService : IRestApiCustomerService
{
    private const string ClientName = "customer-web-api";

    private readonly HttpClient _httpClient;
    
    public RestApiCustomerService(IHttpClientFactory factory)
    {
        _httpClient = factory.CreateClient(ClientName);
    }

    public async Task<CustomerDto?> GetByIdAsync(long id)
    {
        using var response = await _httpClient.GetAsync($"{id}");
        
        if (response.StatusCode == HttpStatusCode.OK)
        {
            var result = await response.Content.ReadAsStringAsync();
            return JsonSerializer.Deserialize<CustomerDto>(result);
        }

        return null;
    }

    public async Task<int> SaveCustomerAsync(CustomerDto customer)
    {
        var content = new StringContent(JsonSerializer.Serialize(customer));
        content.Headers.ContentType = new MediaTypeHeaderValue("application/json");
        using var response = await _httpClient.PostAsync(_httpClient.BaseAddress, content);

        switch (response.StatusCode)
        {
            case HttpStatusCode.OK:
            {
                var result = await response.Content.ReadAsStringAsync();
                return int.Parse(result);
            }
            case HttpStatusCode.Conflict:
                return default;
            default:
                return -1;
        }
    }
}