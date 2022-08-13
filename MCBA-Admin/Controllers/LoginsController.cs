using MCBA_Admin.Models.DataManagers;
using MCBA_Model.Models;
using Microsoft.AspNetCore.Mvc;

namespace MCBA_Admin.Controllers;

public class LoginsController : Controller
{
    private readonly LoginsManager _loginsManager;

    public LoginsController(LoginsManager loginsManager) =>
        _loginsManager = loginsManager;


    public async Task<IActionResult> Index() =>
        View(await _loginsManager.GetAllAsync());


    public async Task<IActionResult> LockCustomerLogin(string? loginID) =>
        View(await _loginsManager.GetAsync(loginID));

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