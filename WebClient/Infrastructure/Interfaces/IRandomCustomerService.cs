using WebClient.Infrastructure.Models;

namespace WebClient.Infrastructure.Interfaces;

public interface IRandomCustomerService
{
    Customer Get();
}