using MCBA_Customer.Data;
using MCBA_Customer.Models;
using MCBA_Customer.Models.DataManagers;
using Microsoft.AspNetCore.Mvc;

namespace MCBA_Customer.Controllers;

public class CustomerController : Controller
{
    private readonly McbaContext _context;
    private readonly AccountManager _accountManager;
    private int CustomerID => HttpContext.Session.GetInt32(nameof(Customer.CustomerID)).Value;

    public CustomerController(McbaContext context, AccountManager accountManager)
    {
        _context = context;
        _accountManager = accountManager;
    }

    public async Task<IActionResult> Index()
    {
        var customer = await _context.Customers.FindAsync(CustomerID);

        foreach (var account in customer.Accounts)
            account.Balance = await _accountManager.GetAccountBalanceAsync(account.AccountNumber);

        return View(customer);
    }
}