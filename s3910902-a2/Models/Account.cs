using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using s3910902_a2.Models.Types;

namespace s3910902_a2.Models;

public class Account
{
    [Key, DatabaseGenerated(DatabaseGeneratedOption.None)]
    public int AccountNumber { get; set; }
    
    public AccountType AccountType { get; set; }
    
    public int CustomerID { get; set; }
    public virtual Customer Customer { get; set; }
    
    [Column(TypeName = "money"), DataType(DataType.Currency)]
    public decimal Balance { get; set; }
    
    [InverseProperty(nameof(Transaction.Account))]
    public List<Transaction>? Transactions { get; set; }
    
    [InverseProperty(nameof(BillPay.Account))]
    public List<BillPay>? BillPays { get; set; }
}