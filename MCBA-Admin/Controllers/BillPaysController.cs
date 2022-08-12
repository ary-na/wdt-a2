using MCBA_Admin.Models.DataManagers;
using MCBA_Model.Models;
using Microsoft.AspNetCore.Mvc;

namespace MCBA_Admin.Controllers;

public class BillPaysController : Controller
{
    private readonly BillPaysManager _billPaysManager;

    public BillPaysController(BillPaysManager billPaysManager) => _billPaysManager = billPaysManager;

    public async Task<IActionResult> Index(int page = 1)
    {
        return View(await _billPaysManager.GetPagedBillPaysAsync(page));
    }
    
    public async Task<IActionResult> LockBillPay(int billPayID)
    {
        return View(await _billPaysManager.GetAsync(billPayID));
    }
    
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> LockBillPay(BillPay billPay)
    {
        var isUpdated = await _billPaysManager.PutAsync(billPay);

        if (isUpdated)
            return RedirectToAction(nameof(Index));

        return View(billPay);
    }
}