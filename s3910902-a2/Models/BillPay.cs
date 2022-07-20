using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace s3910902_a2.Models;

public class BillPay
{
    public int BillPayID { get; set; }

    [Required, ForeignKey(nameof(Account))] public int AccountNumber { get; set; }
    public virtual Account? Account { get; set; }

    [Required] public int PayeeID { get; set; }
    public virtual Payee? Payee { get; set; }

    [Column(TypeName = "money")] public decimal Amount { get; set; }

    [Required] public DateTime ScheduleTimeUtc { get; set; }

    [Required] public char Period { get; set; }
}