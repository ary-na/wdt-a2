using MCBA_Model.Models;
using MCBA_Model.Models.Types;

namespace MCBA_Customer.ViewModels.Interfaces;

public interface ITransactionViewModel
{
    public Account? Account { get; set; }
    public TransactionType TransactionType { get; set; }
    public int AccountNumber { get; set; }
    public int? DestinationAccountNumber { get; set; }
    public decimal Amount { get; set; }
    public string? Comment { get; set; }
}