using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using MCBA_Customer.Attributes;
using MCBA_Customer.Models;
using MCBA_Customer.Models.Types;

namespace MCBA_Customer.ViewModels;

public class BillPayViewModel
{
    public Account? Account { get; set; }
    public int AccountNumber { get; set; }
    
    [Required, DisplayName("Payee"), InvalidPayee]
    public int PayeeID { get; set; }

    [HasMoreThanTwoDecimalPlaces, LessThanOrEqualToZero]
    public decimal Amount { get; set; }

    [DisplayName("Schedule Time")]
    public DateTime ScheduleTimeUtc { get; set; }
    
    public BillPayPeriod Period { get; set; }
}