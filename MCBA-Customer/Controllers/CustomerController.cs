using MCBA_Customer.DataManagers;
using MCBA_Customer.ViewModels;
using MCBA_Model.Models;
using Microsoft.AspNetCore.Mvc;

namespace MCBA_Customer.Controllers;

public class CustomerController : Controller
{
    private readonly CustomerManager _customerManager;
    private readonly LoginManager _loginManager;
    private int CustomerID => HttpContext.Session.GetInt32(nameof(Customer.CustomerID)).Value;

    public CustomerController(CustomerManager customerManager, LoginManager loginManager)
    {
        _customerManager = customerManager;
        _loginManager = loginManager;
    }

    public async Task<IActionResult> Index()
    {
        return View(await _customerManager.GetCustomerAsync(CustomerID));
    }

    public async Task<IActionResult> EditProfile(int customerID)
    {
        return View(new EditProfileViewModel
        {
            CustomerID = customerID
        });
    }

    [HttpPost]
    public async Task<IActionResult> EditProfile(EditProfileViewModel viewModel)
    {
        // Check model state
        if (!ModelState.IsValid)
            return View(viewModel);

        await _customerManager.EditProfileAsync(viewModel);
        return RedirectToAction(nameof(Index));
    }

    public async Task<IActionResult> ChangePassword(int customerID)
    {
        return View(new ChangePasswordViewModel()
            {
                CustomerID = customerID,
            }
        );
    }

    [HttpPost]
    public async Task<IActionResult> ChangePassword(ChangePasswordViewModel viewModel)
    {
        // Check model state
        if (!ModelState.IsValid)
            return View(viewModel);

        await _loginManager.ChangePasswordAsync(viewModel);
        return RedirectToAction(nameof(Index));
    }
}