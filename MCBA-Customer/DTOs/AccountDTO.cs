namespace MCBA_Customer.DTOs;

public class AccountDTO
{
    public int AccountNumber { get; set; }
    public char? AccountType { get; set; }
    public int CustomerId { get; set; }
    public decimal Balance { get; set; }
    public List<TransactionDTO>? Transactions { get; set; }
}