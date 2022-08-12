using MCBA_Admin.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace MCBA_Admin.Filters;

// Code sourced and adapted from:
// https://docs.microsoft.com/en-us/dotnet/api/system.attributeusageattribute?view=net-6.0

[AttributeUsage(AttributeTargets.Class)]
public class AuthoriseAdminAttribute : Attribute, IAuthorizationFilter
{
    public void OnAuthorization(AuthorizationFilterContext context)
    {
        if (context.ActionDescriptor.EndpointMetadata.Any(x => x is AllowAnonymousAttribute))
            return;

        var adminID = context.HttpContext.Session.GetInt32(nameof(Admin.AdminID));
        if (!adminID.HasValue)
            context.Result = new RedirectToActionResult("Index", "Home", null);
    }
}