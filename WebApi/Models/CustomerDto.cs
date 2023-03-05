using System.ComponentModel.DataAnnotations;
using WebApi.Domain.Entities;

namespace WebApi.Models;

public class CustomerDto
{
    public CustomerDto(Customer customer)
    {
        Id = customer.Id;
        Firstname = customer.Firstname;
        Lastname = customer.Lastname;
    }

    public CustomerDto()
    {
    }

    public long Id { get; init; }

    [Required]
    public string Firstname { get; init; }

    [Required]
    public string Lastname { get; init; }
}