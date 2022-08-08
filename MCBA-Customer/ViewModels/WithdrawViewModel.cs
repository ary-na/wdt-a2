using System.ComponentModel.DataAnnotations;
using MCBA_Customer.Attributes;
using MCBA_Customer.Models;
using MCBA_Customer.Models.Types;

namespace MCBA_Customer.ViewModels;

public class WithdrawViewModel : ITransactionViewModel
{
    public Account? Account { get; set; }
    public TransactionType TransactionType { get; set; }
    public int AccountNumber { get; set; }
    public int? DestinationAccountNumber { get; set; }

    [HasMoreThanTwoDecimalPlaces, LessThanOrEqualToZero]
    public decimal Amount { get; set; }

    [MaxLength(30)] public string? Comment { get; set; }
}