using MCBA_Admin.Models.DataManagers;
using Microsoft.AspNetCore.Mvc;

namespace MCBA_Admin.Controllers;

public class AccountsController : Controller
{
    private readonly AccountsManager _accountsManager;

    public AccountsController(AccountsManager accountsManager) => _accountsManager = accountsManager;

    public async Task<IActionResult> Index()
    {
        return View(await _accountsManager.GetAll());
    }
    
    public async Task<IActionResult> Transactions(int accountNumber, DateTime? from, DateTime? to)
    {
        return View(await _accountsManager.GetAccountTransactions(accountNumber, from, to));
    }
}