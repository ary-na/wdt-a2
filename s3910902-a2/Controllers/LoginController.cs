using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using s3910902_a2.Data;
using s3910902_a2.Models;
using s3910902_a2.ViewModels;
using SimpleHashing;

namespace s3910902_a2.Controllers;

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

    [Route("LogoutNow")]
    public IActionResult Logout()
    {
        // Logout customer.
        HttpContext.Session.Clear();

        return RedirectToAction("Index", "Home");
    }
}