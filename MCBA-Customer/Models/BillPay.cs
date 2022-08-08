using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using MCBA_Customer.Models.Types;

namespace MCBA_Customer.Models;

public class BillPay
{
    public int BillPayID { get; set; }

    [Required, ForeignKey(nameof(Account))] public int AccountNumber { get; set; }
    public virtual Account? Account { get; set; }

    [Required] public int PayeeID { get; set; }
    public virtual Payee? Payee { get; set; }

    [Column(TypeName = "money")] public decimal Amount { get; set; }

    [Required] public DateTime ScheduleTimeUtc { get; set; }

    [Required] public BillPayPeriod Period { get; set; }
    
    [Required] public BillPayState BillState { get; set; }
}