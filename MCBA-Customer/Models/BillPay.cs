using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using MCBA_Customer.Models.Types;

namespace MCBA_Customer.Models;

public class BillPay
{
    [DisplayName("Bill Pay ID")]
    public int BillPayID { get; set; }

    [Required, ForeignKey(nameof(Account))] public int AccountNumber { get; set; }
    public virtual Account? Account { get; set; }

    [Required, DisplayName("Payee ID")] public int PayeeID { get; set; }
    public virtual Payee? Payee { get; set; }

    [Column(TypeName = "money")] public decimal Amount { get; set; }

    [DisplayName("Schedule Time")]
    [Required] public DateTime ScheduleTimeUtc { get; set; }

    [Required] public BillPayPeriod Period { get; set; }
    
    [DisplayName("Bill Status")]
    [Required] public BillPayStatus BillStatus { get; set; }
}