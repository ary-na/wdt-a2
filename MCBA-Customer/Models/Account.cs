using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using MCBA_Customer.Models.Types;

namespace MCBA_Customer.Models;

// Code sourced and adapted from:
// Week 10 Lectorial - Account.cs

public class Account
{
    private const int FreeTransactionCount = 2;

    private static readonly Dictionary<AccountType, decimal> MinimumBalance = new()
    {
        { AccountType.Saving, 0 },
        { AccountType.Checking, 300 }
    };

    [Key, DatabaseGenerated(DatabaseGeneratedOption.None)]
    public int AccountNumber { get; set; }

    public AccountType AccountType { get; set; }

    public int CustomerID { get; set; }
    public virtual Customer Customer { get; set; }

    [NotMapped] public decimal Balance { get; set; }

    [InverseProperty(nameof(Transaction.Account))]
    public virtual List<Transaction>? Transactions { get; set; }

    [InverseProperty(nameof(BillPay.Account))]
    public virtual List<BillPay>? BillPays { get; set; }

    public decimal AvailableBalance => Math.Max(Balance - MinimumBalance[AccountType], 0);
    public bool AvailableFreeTransaction(int transactionCount) => FreeTransactionCount >= transactionCount;
}