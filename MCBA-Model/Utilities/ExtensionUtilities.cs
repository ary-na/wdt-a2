namespace MCBA_Model.Utilities;

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
}