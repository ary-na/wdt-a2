using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace s3910902_a2.Models;

public class Payee
{
    public int PayeeID { get; set; }

    [Required, StringLength(50)] public string? Name { get; set; }

    [Required, StringLength(50)] public string? Address { get; set; }

    [Required, StringLength(40)] public string? City { get; set; }

    [Required, StringLength(3), MaxLength(3)]
    public string? State { get; set; }

    [Required, StringLength(4), MaxLength(4), RegularExpression(@"^[1-9][1-9][1-9][1-9]$")]
    public string? PostCode { get; set; }

    [Required, StringLength(14)] public string? Phone { get; set; }
    
    [InverseProperty(nameof(BillPay.Payee))]
    public List<BillPay>? Payments { get; set; }
}