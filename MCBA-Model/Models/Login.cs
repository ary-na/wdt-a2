using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using MCBA_Model.Models.Types;

namespace MCBA_Model.Models;

public class Login
{
    [Column(TypeName = "char"), StringLength(8)]
    public string? LoginID { get; set; }
    
    [ForeignKey(nameof(Customer))]
    public int CustomerID { get; set; }
    public virtual Customer? Customer { get; set; }
    
    [Required, Column(TypeName = "char"), StringLength(64)]
    public string? PasswordHash { get; set; }
    
    public LoginState? LoginState { get; set; }
}