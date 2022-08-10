using System.ComponentModel.DataAnnotations;
using MCBA_Customer.ViewModels;

namespace MCBA_Customer.Attributes;

public class DateInThePastAttribute : ValidationAttribute
{
    private static string GetErrorMessage() => "Date must be in the future.";

    protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
    {
        var billPay = (BillPayViewModel)validationContext.ObjectInstance;
        var dateTime = billPay.ScheduleTime.ToUniversalTime();
        return dateTime > DateTime.UtcNow
            ? ValidationResult.Success
            : new ValidationResult(GetErrorMessage());
    }
}