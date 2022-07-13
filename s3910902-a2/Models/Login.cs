using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace s3910902_a2.Models;

public class Login
{
    [Required, Column(TypeName = "char"), StringLength(8)]
    public string? LoginID { get; set; }
    
    public int CustomerID { get; set; }
    public virtual Customer? Customer { get; set; }
    
    [Required, Column(TypeName = "char"), StringLength(64)]
    public string? PasswordHash { get; set; }
}