using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using MCBA_Customer.Attributes;
using MCBA_Customer.Models.Types;

namespace MCBA_Customer.Models;

public class Transaction
{
    public static readonly Dictionary<TransactionType, decimal> ServiceCharge = new()
    {
        { TransactionType.Withdraw, 0.05M },
        { TransactionType.TransferOut, 0.10M }
    };

    [DisplayName("Transaction ID")]
    public int TransactionID { get; set; }

    [DisplayName("Transaction Type")]
    public TransactionType TransactionType { get; set; }

    [ForeignKey(nameof(Account)), DisplayName("Account Number")] public int AccountNumber { get; set; }
    public virtual Account Account { get; set; }

    [ForeignKey(nameof(DestinationAccount)), DisplayName("Destination Account Number")]
    public int? DestinationAccountNumber { get; set; }

    public virtual Account? DestinationAccount { get; set; }

    [Column(TypeName = "money"), HasMoreThanTwoDecimalPlaces, LessThanOrEqualToZero]
    public decimal Amount { get; set; }

    [StringLength(30, ErrorMessage = "Comment exceeded maximum length")]
    public string? Comment { get; set; }

    [DisplayName("Transaction Time")]
    public DateTime TransactionTimeUtc { get; set; }
}