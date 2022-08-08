using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using s3910902_a2.Models;

namespace s3910902_a2.Filters;

// Code sourced and adapted from:
// https://docs.microsoft.com/en-us/dotnet/api/system.attributeusageattribute?view=net-6.0

[AttributeUsage(AttributeTargets.Class)]
public class AuthoriseCustomerAttribute : Attribute, IAuthorizationFilter
{
    public void OnAuthorization(AuthorizationFilterContext context)
    {
        if (context.ActionDescriptor.EndpointMetadata.Any(x => x is AllowAnonymousAttribute))
            return;

        var customerId = context.HttpContext.Session.GetInt32(nameof(Customer.CustomerID));
        if (!customerId.HasValue)
            context.Result = new RedirectToActionResult("Index", "Home", null);
    }
}