using MCBA_Customer.Data;
using MCBA_Customer.Models;
using MCBA_Customer.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SimpleHashing;

namespace MCBA_Customer.Controllers;

[AllowAnonymous]
public class LoginController : Controller
{
    private readonly McbaContext _context;

    public LoginController(McbaContext context) => _context = context;

    public IActionResult Login() => View();

    [HttpPost]
    public async Task<IActionResult> Login(LoginViewModel viewModel)
    {
        var login = await _context.Logins.FindAsync(viewModel.LoginID);
        if (login == null || string.IsNullOrEmpty(viewModel.Password) || !PBKDF2.Verify(login.PasswordHash, viewModel.Password))
        {
            ModelState.AddModelError("LoginFailed", "Login failed, please try again.");
            return View(new LoginViewModel { LoginID = viewModel.LoginID });
        }

        // Login customer.
        HttpContext.Session.SetInt32(nameof(Customer.CustomerID), login.CustomerID);
        HttpContext.Session.SetString(nameof(Customer.Name), login.Customer.Name);

        return RedirectToAction("Index", "Customer");
    }

    [Route("Logout")]
    public IActionResult Logout()
    {
        // Logout customer.
        HttpContext.Session.Clear();

        return RedirectToAction("Index", "Home");
    }
}