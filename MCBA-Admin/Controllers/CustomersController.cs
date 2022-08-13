using MCBA_Admin.Models.DataManagers;
using MCBA_Model.Models;
using Microsoft.AspNetCore.Mvc;

namespace MCBA_Admin.Controllers;

public class CustomersController : Controller
{
    private readonly CustomersManager _customersManager;

    public CustomersController(CustomersManager customersManager) =>
        _customersManager = customersManager;

    public async Task<IActionResult> Index() =>
        View(await _customersManager.GetAllAsync());

    public async Task<IActionResult> EditCustomerProfile(int customerID) =>
        View(await _customersManager.GetAsync(customerID));

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> EditCustomerProfile(Customer customer)
    {
        var isUpdated = await _customersManager.PutAsync(customer);

        if (isUpdated)
            return RedirectToAction(nameof(Index));

        return View(customer);
    }
}