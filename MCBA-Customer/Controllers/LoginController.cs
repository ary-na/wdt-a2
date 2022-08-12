using MCBA_Customer.Models.DataManagers;
using MCBA_Customer.ViewModels;
using MCBA_Model.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SimpleHashing;

namespace MCBA_Customer.Controllers;

[AllowAnonymous]
public class LoginController : Controller
{
    private readonly LoginManager _loginManager;

    public LoginController(LoginManager loginManager) => _loginManager = loginManager;

    public IActionResult Login() => View();

    [HttpPost]
    public async Task<IActionResult> Login(LoginViewModel viewModel)
    {
        var login = await _loginManager.GetLoginAsync(viewModel.LoginID);
        if (!await _loginManager.IsLoggedInAsync(viewModel))
        {
            ModelState.AddModelError("LoginFailed", "Login failed, please try again.");
            return View(new LoginViewModel { LoginID = viewModel.LoginID });
        }

        // Login customer.
        HttpContext.Session.SetInt32(nameof(Customer.CustomerID), login.CustomerID);
        HttpContext.Session.SetString(nameof(Customer.Name), login.Customer.Name);

        return RedirectToAction("Index", "Transactions");
    }

    [Route("Logout")]
    public IActionResult Logout()
    {
        // Logout customer.
        HttpContext.Session.Clear();

        return RedirectToAction("Index", "Home");
    }
}