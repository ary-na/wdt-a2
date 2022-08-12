using MCBA_Model.Models;
using MCBA_Web_API.Models.DataManagers;
using Microsoft.AspNetCore.Mvc;

namespace MCBA_Web_API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class LoginController : ControllerBase
{
    private readonly LoginManager _repo;
    private readonly ILogger<LoginController> _logger;

    public LoginController(LoginManager repo, ILogger<LoginController> logger)
    {
        _repo = repo;
        _logger = logger;
    }

    [HttpGet]
    public IEnumerable<Login> Get()
    {
        return _repo.GetAll();
    }

    [HttpGet("{id}")]
    public Login Get(string? id)
    {
        return _repo.Get(id);
    }

    [HttpPut]
    public void Put([FromBody] Login login)
    {
        _repo.UpdateAccountState(login.LoginID, login.LoginState);
    }
}