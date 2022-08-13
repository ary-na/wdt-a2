using MCBA_Customer.Models.DataManagers;
using MCBA_Customer.ViewModels;
using MCBA_Model.Models;
using MCBA_Model.Models.Types;
using Microsoft.AspNetCore.Mvc;

namespace MCBA_Customer.Controllers;

// Code sourced and adapted from:
// Week 5 Lectorial - HomeController.cs

// https://stackoverflow.com/questions/32072764/how-to-call-one-action-method-from-another-action-methodboth-are-in-the-same-co
// https://stackoverflow.com/questions/879852/display-a-view-from-another-controller-in-asp-net-mvc

public class TransactionsController : Controller
{
    private readonly CustomerManager _customerManager;
    private readonly AccountManager _accountManager;
    private int CustomerID => HttpContext.Session.GetInt32(nameof(Customer.CustomerID)).Value;

    public TransactionsController(CustomerManager customerManager, AccountManager accountManager)
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
        return View("TransactionSuccessful", viewModel);
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
        var success = await _accountManager.WithdrawAsync(viewModel);

        return success ? View("TransactionSuccessful", viewModel) : View("TransactionFailed", viewModel);
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
        var success = await _accountManager.TransferAsync(viewModel);
        return success ? View("TransactionSuccessful", viewModel) : View("TransactionFailed", viewModel);
    }
    
    public async Task<IActionResult> BillPays(int accountNumber, int page = 1)
    {
        ViewBag.AccountNumber = accountNumber;
        return View(await _accountManager.GetBillPaysAsync(accountNumber, page));
    }

    public async Task<IActionResult> BillPay(int accountNumber)
    {
        return View(new BillPayViewModel()
            {
                AccountNumber = accountNumber,
                Account = await _accountManager.GetAccountAsync(accountNumber)
            }
        );
    }

    [HttpPost]
    public async Task<IActionResult> BillPay(BillPayViewModel viewModel)
    {
        // Set view account
        viewModel.Account = await _accountManager.GetAccountAsync(viewModel.AccountNumber);

        // Check model state
        if (!ModelState.IsValid)
            return View(viewModel);

        // Insert transaction
        await _accountManager.BillPayAsync(viewModel);
        return RedirectToAction(nameof(BillPays), new { accountNumber = viewModel.AccountNumber});
    }
    
    public async Task<IActionResult> EditBillPay(int billPayID)
    {
        var billPay = await _accountManager.GetBillPayAsync(billPayID);
        billPay.ScheduleTimeUtc = billPay.ScheduleTimeUtc.ToLocalTime();
        return View(billPay);
    }

    [HttpPost]
    public async Task<IActionResult> EditBillPay(BillPay billPay)
    {
        // Check model state
        if (!ModelState.IsValid)
            return View(billPay);
        
        var success = await _accountManager.UpdateBillPayAsync(billPay);
        return RedirectToAction(nameof(BillPays), new { accountNumber = billPay.AccountNumber});
    }
    
    
    public async Task<IActionResult> DeleteBillPay(int billPayID)
    {
        var billPay = await _accountManager.GetBillPayAsync(billPayID);
        billPay.ScheduleTimeUtc = billPay.ScheduleTimeUtc.ToLocalTime();
        return View(billPay);
    }

    [HttpPost]
    public async Task<IActionResult> DeleteBillPay(BillPay billPay)
    {
        // Check model state
        if (!ModelState.IsValid)
            return View(billPay);
        
        await _accountManager.DeleteBillPayAsync(billPay);
        return RedirectToAction(nameof(BillPays), new { accountNumber = billPay.AccountNumber});
    }
}