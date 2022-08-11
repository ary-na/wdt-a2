using MCBA_Model.Models;
using MCBA_Web_API.Models.DataManagers;
using Microsoft.AspNetCore.Mvc;

namespace MCBA_Web_API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class CustomerController : ControllerBase
{
    private readonly CustomerManager _repo;
    private readonly ILogger<CustomerController> _logger;

    public CustomerController(CustomerManager repo, ILogger<CustomerController> logger)
    {
        _repo = repo;
        _logger = logger;
    }

    [HttpGet]
    public IEnumerable<Customer> Get()
    {
        return _repo.GetAll();
    }

    [HttpGet("{id}")]
    public Customer Get(int id)
    {
        return _repo.Get(id);
    }

    [HttpPut]
    public void Put([FromBody] Customer customer)
    {
        _repo.Update(customer.CustomerID, customer);
    }
}