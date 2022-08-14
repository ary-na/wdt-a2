using MCBA_Customer.Models.DataManagers;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace MCBA_Customer.ViewComponents;

// Code sourced and adapted from:
// Week 7 Lectorial - RecentArticlesViewComponent.cs
// https://rmit.instructure.com/courses/102750/files/24426714?wrap=1

public class PayeesViewComponent : ViewComponent
{
    private readonly PayeeManager _payeeManger;

    public PayeesViewComponent(PayeeManager payeeManager)
    {
        _payeeManger = payeeManager;
    }

    public async Task<IViewComponentResult> InvokeAsync()
    {
        var payees = await _payeeManger.GetAllPayees();
        return View(new SelectList(payees, "PayeeID", "Name"));
    }
}