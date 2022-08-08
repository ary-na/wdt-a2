namespace MCBA_Customer.DTOs;

public class TransactionDTO
{
    public decimal Amount { get; set; }
    public string? Comment { get; set; }
    public DateTime TransactionTimeUtc { get; set; }
}