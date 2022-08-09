using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using MCBA_Customer.Attributes;
using MCBA_Customer.Models;
using MCBA_Customer.Models.Types;
using MCBA_Customer.ViewModels.Interfaces;

namespace MCBA_Customer.ViewModels;

public class TransferViewModel : ITransactionViewModel
{
    public Account? Account { get; set; }
    public TransactionType TransactionType { get; set; }
    public int AccountNumber { get; set; }

    [Required, DisplayName("Destination Account Number")]
    public int? DestinationAccountNumber { get; set; }

    [HasMoreThanTwoDecimalPlaces, LessThanOrEqualToZero]
    public decimal Amount { get; set; }

    [MaxLength(30)] public string? Comment { get; set; }
}