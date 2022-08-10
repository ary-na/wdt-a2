using System.ComponentModel.DataAnnotations;
using MCBA_Customer.Utilities;
using MCBA_Customer.ViewModels;

namespace MCBA_Customer.Attributes;

public class InvalidPayeeAttribute : ValidationAttribute
{
    private static string GetErrorMessage() => "Please select a payee.";

    protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
    {
        var billPay = (BillPayViewModel)validationContext.ObjectInstance;
        return !billPay.PayeeID.Equals(0)
            ? ValidationResult.Success
            : new ValidationResult(GetErrorMessage());
    }
}