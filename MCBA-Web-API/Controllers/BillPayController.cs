using MCBA_Model.Models;
using MCBA_Web_API.Models.DataManagers;
using Microsoft.AspNetCore.Mvc;

namespace MCBA_Web_API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class BillPayController : ControllerBase
{
    private readonly BillPayManager _repo;
    private readonly ILogger<BillPayController> _logger;

    public BillPayController(BillPayManager repo, ILogger<BillPayController> logger)
    {
        _repo = repo;
        _logger = logger;
    }

    [HttpGet]
    public IEnumerable<BillPay> Get()
    {
        return _repo.GetAll();
    }

    [HttpGet("{id}")]
    public BillPay Get(int id)
    {
        return _repo.Get(id);
    }

    [HttpPut]
    public void Put([FromBody] BillPay billPay)
    {
        _repo.Update(billPay.BillPayID, billPay);
    }
}