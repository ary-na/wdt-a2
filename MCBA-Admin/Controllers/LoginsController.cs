using MCBA_Admin.Models.DataManagers;
using MCBA_Model.Models;
using Microsoft.AspNetCore.Mvc;

namespace MCBA_Admin.Controllers;

public class LoginsController : Controller
{
    private readonly LoginsManager _loginsManager;
    private readonly ILogger<LoginsController> _logger;

    public LoginsController(LoginsManager loginsManager, ILogger<LoginsController> logger)
    {
        _loginsManager = loginsManager;
        _logger = logger;
    }

    public async Task<IActionResult> Index()
    {
        return View(await _loginsManager.GetAllAsync());
    }

    public async Task<IActionResult> LockCustomerLogin(string? loginID)
    {
        return View(await _loginsManager.GetAsync(loginID));
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> LockCustomerLogin(Login login)
    {
        var isUpdated = await _loginsManager.PutAsync(login);
        if (isUpdated)
            return RedirectToAction(nameof(Index));

        return View(login);
    }
}