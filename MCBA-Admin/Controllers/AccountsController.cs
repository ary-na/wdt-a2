using MCBA_Admin.Models.DataManagers;
using Microsoft.AspNetCore.Mvc;

namespace MCBA_Admin.Controllers;

public class AccountsController : Controller
{
    private readonly AccountsManager _accountsManager;

    public AccountsController(AccountsManager accountsManager) =>
        _accountsManager = accountsManager;

    public async Task<IActionResult> Index() =>
        View(await _accountsManager.GetAll());

    // Filter Transactions by date
    public async Task<IActionResult> Transactions(int accountNumber, DateTime? from, DateTime? to, int page = 1)
    {
        // Set the account number 
        ViewBag.AccountNumber = accountNumber;
        return View(await _accountsManager.GetPagedAccountTransactionsAsync(accountNumber, from, to, page));
    }
}