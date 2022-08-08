using s3910902_a2.Models;
using s3910902_a2.Models.Types;

namespace s3910902_a2.ViewModels;

public interface ITransactionViewModel
{
    public Account? Account { get; set; }
    public TransactionType TransactionType { get; set; }
    public int AccountNumber { get; set; }
    public int? DestinationAccountNumber { get; set; }
    public decimal Amount { get; set; }
    public string? Comment { get; set; }
}