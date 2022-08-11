using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using MCBA_Customer.Attributes;
using MCBA_Model.Models;
using MCBA_Model.Models.Types;

namespace MCBA_Customer.ViewModels;

public class BillPayViewModel
{
    public Account? Account { get; set; }
    public int AccountNumber { get; set; }
    
    [Required, PayeeNotSelected, DisplayName("Payee")]
    public int PayeeID { get; set; }

    [HasMoreThanTwoDecimalPlaces, LessThanOrEqualToZero]
    public decimal Amount { get; set; }

    [DateInThePast, DisplayName("Schedule Time")]
    public DateTime ScheduleTime { get; set; }
    
    public BillPayPeriod Period { get; set; }
}