using System;
using WebClient.Infrastructure.Interfaces;
using WebClient.Infrastructure.Models;

namespace WebClient.Infrastructure.Implementation;

public class RandomCustomerService : IRandomCustomerService
{
    private const string DefaultFirstName = "FirstName";
    
    private const string DefaultLastName = "FirstName";

    private const int MinId = 1;

    private const int MaxId = 15;

    private readonly Random _random;

    public RandomCustomerService()
    {
        _random = new Random();
    }

    public Customer Get()
    {
        var id = _random.Next(MinId, MaxId);
        return new Customer { Id = id, Firstname = $"{DefaultFirstName}_{id}", Lastname = $"{DefaultLastName}_{id}" };
    }
}