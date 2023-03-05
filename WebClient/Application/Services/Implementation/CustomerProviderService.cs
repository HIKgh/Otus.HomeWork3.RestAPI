using System;
using System.Threading.Tasks;
using WebClient.Application.Models;
using WebClient.Application.Services.Interfaces;
using WebClient.Infrastructure.Interfaces;

namespace WebClient.Application.Services.Implementation;

public class CustomerProviderService : ICustomerProviderService
{
    private readonly IRestApiCustomerService _apiService;

    private readonly IRandomCustomerService _randomCustomerService;

    public CustomerProviderService(IRestApiCustomerService apiService, IRandomCustomerService randomCustomerService)
    {
        _apiService = apiService;
        _randomCustomerService = randomCustomerService;
    }

    public async Task GetById()
    {
        Console.WriteLine("Введите идентификатор:");
        var idString = Console.ReadLine();
        if (int.TryParse(idString, out var id))
        {
            var result = await _apiService.GetByIdAsync(id);
            Console.WriteLine(result == null
                ? $"Клиент не найден Id={id}"
                : $"Получен клиент: Id={result.Id}, FirstName={result.Firstname}, LastName={result.Lastname}");
        }
        else
        {
            Console.WriteLine("Некорректный идентификатор");
        }
    }

    public async Task SaveCustomer()
    {
        var customer = _randomCustomerService.Get();
        var customerDto = new CustomerDto { Id = customer.Id, Firstname = customer.Firstname, Lastname = customer.Lastname };
        var result = await _apiService.SaveCustomerAsync(customerDto);
        
        switch (result)
        {
            case > 0:
                Console.WriteLine(
                    $"Добавлен клиент: Id={customerDto.Id}, FirstName={customerDto.Firstname}, LastName={customerDto.Lastname}");
                var newCustomer = await _apiService.GetByIdAsync(result);
                Console.WriteLine(newCustomer == null
                    ? $"Клиент не найден Id={customerDto.Id}"
                    : $"Получен клиент: Id={newCustomer.Id}, FirstName={newCustomer.Firstname}, LastName={newCustomer.Lastname}");
                break;
            case 0:
                Console.WriteLine($"Клиент с Id={customerDto.Id} уже существует");
                break;
        }
    }
}