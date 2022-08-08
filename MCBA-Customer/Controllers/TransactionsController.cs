using MCBA_Customer.Data;
using MCBA_Customer.Models;
using MCBA_Customer.Models.DataManagers;
using MCBA_Customer.Models.Types;
using MCBA_Customer.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace MCBA_Customer.Controllers;

// Code sourced and adapted from:
// Week 5 Lectorial - HomeController.cs

public class TransactionsController : Controller
{
    private readonly McbaContext _context;
    private readonly AccountManager _accountManager;
    private int CustomerID => HttpContext.Session.GetInt32(nameof(Customer.CustomerID)).Value;

    public TransactionsController(McbaContext context, AccountManager accountManager)
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

    public async Task<IActionResult> Deposit(int accountNumber)
    {
        return View(new DepositViewModel
            {
                AccountNumber = accountNumber,
                TransactionType = TransactionType.Deposit,
                Account = await _accountManager.GetAccountAsync(accountNumber)
            }
        );
    }

    [HttpPost]
    public async Task<IActionResult> Deposit(DepositViewModel viewModel)
    {
        // Set view account
        viewModel.Account = await _accountManager.GetAccountAsync(viewModel.AccountNumber);
        viewModel.TransactionType = TransactionType.Deposit;

        // Check model state
        if (!ModelState.IsValid)
            return View(viewModel);

        // Insert transaction
        await _accountManager.DepositAsync(viewModel);
        return RedirectToAction(nameof(Index));
    }

    public async Task<IActionResult> Withdraw(int accountNumber)
    {
        return View(new WithdrawViewModel
            {
                AccountNumber = accountNumber,
                TransactionType = TransactionType.Withdraw,
                Account = await _accountManager.GetAccountAsync(accountNumber)
            }
        );
    }

    [HttpPost]
    public async Task<IActionResult> Withdraw(WithdrawViewModel viewModel)
    {
        // Set view account
        viewModel.Account = await _accountManager.GetAccountAsync(viewModel.AccountNumber);
        viewModel.TransactionType = TransactionType.Withdraw;

        // Check model state
        if (!ModelState.IsValid)
            return View(viewModel);

        // Insert transaction
        await _accountManager.WithdrawAsync(viewModel);
        return RedirectToAction(nameof(Index));
    }
    
    public async Task<IActionResult> Transfer(int accountNumber)
    {
        return View(new TransferViewModel
            {
                AccountNumber = accountNumber,
                TransactionType = TransactionType.TransferOut,
                Account = await _accountManager.GetAccountAsync(accountNumber)
            }
        );
    }

    [HttpPost]
    public async Task<IActionResult> Transfer(TransferViewModel viewModel)
    {
        // Set view account
        viewModel.Account = await _accountManager.GetAccountAsync(viewModel.AccountNumber);
        viewModel.TransactionType = TransactionType.TransferOut;

        // Check model state
        if (!ModelState.IsValid)
            return View(viewModel);

        // Insert transaction
        await _accountManager.TransferAsync(viewModel);
        return RedirectToAction(nameof(Index));
    }
}