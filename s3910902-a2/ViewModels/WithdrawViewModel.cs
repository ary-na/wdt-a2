using System.ComponentModel.DataAnnotations;
using s3910902_a2.Models;
using s3910902_a2.Attributes;
using s3910902_a2.Models.Types;

namespace s3910902_a2.ViewModels;

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