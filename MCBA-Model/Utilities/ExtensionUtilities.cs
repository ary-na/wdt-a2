using Autofac;
using Microsoft.AspNetCore.Mvc;

namespace MCBA_Model.Utilities;

// Code sourced and adapted from:
// Week 10 Lectorial
// https://rmit.instructure.com/courses/102750/files/24468521?wrap=1

// Week 10 Tutorial
// https://rmit.instructure.com/courses/102750/files/24426949?wrap=1

// Code sourced and adapted from: 
// Week 5 Lectorial - MiscellaneousExtensionUtilities.cs
// https://rmit.instructure.com/courses/102750/files/24426483?wrap=1

public static class ExtensionUtilities
{
    private static bool HasMoreThanNDecimalPlaces(this decimal value, int n) => decimal.Round(value, n) != value;
    public static bool HasMoreThanTwoDecimalPlaces(this decimal value) => value.HasMoreThanNDecimalPlaces(2);
    public static bool LessThanOrEqualToZero(this decimal value) => value <= 0;

    // public static string ActiveClass(this IHtmlHelper htmlHelper, string? controllers = null, string? actions = null,
    //     string cssClass = "active")
    // {
    //     var currentController = htmlHelper?.ViewContext.RouteData.Values["controller"] as string;
    //     var currentAction = htmlHelper?.ViewContext.RouteData.Values["action"] as string;
    //
    //     var acceptedControllers = (controllers ?? currentController ?? "").Split(',');
    //     var acceptedActions = (actions ?? currentAction ?? "").Split(',');
    //
    //     return acceptedControllers.Contains(currentController) && acceptedActions.Contains(currentAction)
    //         ? cssClass
    //         : "";
    // }

    public static T ResolveController<T>(this IContainer container) where T : Controller
    {
        var controller = container.Resolve<T>();
        controller.ControllerContext = container.Resolve<ControllerContext>();
        return controller;
    }
}