using System.ComponentModel.DataAnnotations;
using s3910902_a2.Attributes;
using s3910902_a2.Models;
using s3910902_a2.Models.Types;

namespace s3910902_a2.ViewModels;

public class BillPayViewModel
{
    public Account? Account { get; set; }
    public int AccountNumber { get; set; }
    public int PayeeID { get; set; }

    [HasMoreThanTwoDecimalPlaces, LessThanOrEqualToZero]
    public decimal Amount { get; set; }

    public DateTime ScheduleTimeUtc { get; set; }

    public BillPayPeriod Period { get; set; }
}