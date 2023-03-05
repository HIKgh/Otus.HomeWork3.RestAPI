using System.Threading.Tasks;

namespace WebClient.Application.Services.Interfaces;

public interface ICustomerProviderService
{
    Task GetById();

    Task SaveCustomer();
}