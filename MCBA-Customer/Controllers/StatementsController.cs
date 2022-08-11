using MCBA_Customer.DataManagers;
using MCBA_Model.Models;
using Microsoft.AspNetCore.Mvc;

namespace MCBA_Customer.Controllers;

public class StatementsController : Controller
{
    private readonly CustomerManager _customerManager;
    private readonly AccountManager _accountManager;
    private int CustomerID => HttpContext.Session.GetInt32(nameof(Customer.CustomerID)).Value;

    public StatementsController(CustomerManager customerManager, AccountManager accountManager)
    {
        _customerManager = customerManager;
        _accountManager = accountManager;
    }

    public async Task<IActionResult> Index()
    {
        var customer = await _customerManager.GetCustomerAsync(CustomerID);
        foreach (var account in customer.Accounts)
            account.Balance = await _accountManager.GetAccountBalanceAsync(account.AccountNumber);
        return View(customer);
    }

    public async Task<IActionResult> Statement(int accountNumber, int page = 1)
    {
        return View(await _accountManager.GetTransactionsAsync(accountNumber, page));
    }
}