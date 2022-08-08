using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using s3910902_a2.Models.Types;
using s3910902_a2.Attributes;

namespace s3910902_a2.Models;

public class Transaction
{
    public static readonly Dictionary<TransactionType, decimal> ServiceCharge = new()
    {
        { TransactionType.Withdraw, 0.05M },
        { TransactionType.Transfer, 0.10M }
    };

    public int TransactionID { get; set; }

    public TransactionType TransactionType { get; set; }

    [ForeignKey(nameof(Account))] public int AccountNumber { get; set; }
    public virtual Account Account { get; set; }

    [ForeignKey(nameof(DestinationAccount))]
    public int? DestinationAccountNumber { get; set; }

    public virtual Account? DestinationAccount { get; set; }

    [Column(TypeName = "money"), HasMoreThanTwoDecimalPlaces, LessThanOrEqualToZero]
    public decimal Amount { get; set; }

    [StringLength(30, ErrorMessage = "Comment exceeded maximum length")]
    public string? Comment { get; set; }

    public DateTime TransactionTimeUtc { get; set; }
}