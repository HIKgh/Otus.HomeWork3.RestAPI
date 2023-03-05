using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebApi.Domain.Entities;
using WebApi.Infrastructure.EntityFramework;
using WebApi.Models;

namespace WebApi.Controllers;

[Route("customers")]
public class CustomerController : Controller
{
    private readonly CustomerDbContext _dbContext;

    public CustomerController(CustomerDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    [ProducesResponseType(typeof(CustomerDto), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [HttpGet("{id:long}")]
    public async Task<IActionResult> GetCustomerAsync([FromRoute] long id)
    {
        var customer = await _dbContext.Customers.FirstOrDefaultAsync(customer => customer.Id == id);
        
        return customer != null
            ? Ok(new CustomerDto(customer))
            : NotFound();
    }

    [ProducesResponseType(typeof(long), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status409Conflict)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    [HttpPost]
    public async Task<IActionResult> CreateCustomerAsync([FromBody] CustomerDto customerDto)
    {
        try
        {
            var currentCustomer = await _dbContext.Customers.FindAsync(customerDto.Id);

            if (currentCustomer != null)
            {
                return Conflict();
            }

            var customer = new Customer
            {
                Id = customerDto.Id,
                Firstname = customerDto.Firstname,
                Lastname = customerDto.Lastname
            };
            await _dbContext.Customers.AddAsync(customer);
            await _dbContext.SaveChangesAsync();

            return Ok(customer.Id);
        }
        catch (Exception exception)
        {
            return StatusCode(StatusCodes.Status500InternalServerError, exception.Message);
        }
    }
}