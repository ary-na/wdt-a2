using MCBA_Admin.Models;
using MCBA_Admin.Models.DataManagers;
using MCBA_Admin.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace MCBA_Admin.Controllers;

[AllowAnonymous]
public class LoginController : Controller
{
    private readonly LoginManager _loginManager;

    public LoginController(LoginManager loginManager) => _loginManager = loginManager;

    public IActionResult Login() => View();

    [HttpPost]
    public IActionResult Login(LoginViewModel viewModel)
    {
        if (!_loginManager.IsLoggedIn(viewModel))
        {
            ModelState.AddModelError("LoginFailed", "Login failed, please try again.");
            return View(new LoginViewModel { LoginID = viewModel.LoginID });
        }

        var admin = _loginManager.GetAdmin();

        // Login admin.
        HttpContext.Session.SetInt32(nameof(Admin.AdminID), admin.AdminID);
        HttpContext.Session.SetString(nameof(Admin.Name), admin.Name);

        return RedirectToAction("Index", "Home");
    }

    [Route("Logout")]
    public IActionResult Logout()
    {
        // Logout admin.
        HttpContext.Session.Clear();

        return RedirectToAction("Index", "Home");
    }
}