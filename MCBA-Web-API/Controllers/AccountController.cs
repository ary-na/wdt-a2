using MCBA_Model.Models;
using MCBA_Web_API.Models.DataManagers;
using Microsoft.AspNetCore.Mvc;

namespace MCBA_Web_API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class AccountController : ControllerBase
{
    private readonly AccountManager _repo;
    private readonly ILogger<AccountController> _logger;

    public AccountController(AccountManager repo, ILogger<AccountController> logger)
    {
        _repo = repo;
        _logger = logger;
    }

    [HttpGet]
    public IEnumerable<Account> GetAccounts()
    {
        return _repo.GetAll();
    }
    
    [HttpGet("{id}")]
    public IEnumerable<Transaction> GetTransactions(int id)
    {
        return _repo.GetTransactions(id);
    }

    [HttpPut]
    public void Put([FromBody] Account account)
    {
        _repo.Update(account.CustomerID, account);
    }
}