using System.Threading.Tasks;
using WebClient.Application.Models;

namespace WebClient.Application.Services.Interfaces;

public interface IRestApiCustomerService
{
    Task<CustomerDto?> GetByIdAsync(long id);

    Task<int> SaveCustomerAsync(CustomerDto customer);
}