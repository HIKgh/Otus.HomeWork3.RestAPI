using System;
using System.Threading.Tasks;
using WebClient.Application.Services.Interfaces;

namespace WebClient.Application.Services.Implementation;

public class ConsoleMenuService : IConsoleMenuService
{
    private const byte MenuItemsCount = 2;

    private readonly ICustomerProviderService _customerProviderService;

    public ConsoleMenuService(ICustomerProviderService customerProviderService)
    {
        _customerProviderService = customerProviderService;
    }

    public async Task Process()
    {
        int result;
        do
        {
            Console.Clear();
            Console.WriteLine($"Выберите номер операции 0..{MenuItemsCount}");
            Console.WriteLine("0 Выход");
            Console.WriteLine("1 Получить данные клиента по Id");
            Console.WriteLine("2 Добавить данные клиента");
            if (int.TryParse(Console.ReadLine(), out result) && result is >= 1 and <= MenuItemsCount)
            {
                try
                {
                    switch (result)
                    {
                        case 1:
                            await _customerProviderService.GetById();
                            break;
                        case 2:
                            await _customerProviderService.SaveCustomer();
                            break;
                    }
                }
                catch (Exception exception)
                {
                    Console.WriteLine($"Произошла ошибка {exception.Message}");
                    Console.ReadKey();
                }
                Console.ReadKey();
            }
        }
        while (result != 0);
    }
}