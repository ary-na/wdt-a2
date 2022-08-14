using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using MCBA_Model.Models.Types;

namespace MCBA_Model.Models;

public class Login
{
    [Column(TypeName = "char"), StringLength(8), DisplayName("Login ID")]
    public string? LoginID { get; set; }

    [ForeignKey(nameof(Customer)), DisplayName("Customer ID")]
    public int CustomerID { get; set; }

    public virtual Customer? Customer { get; set; }

    [Column(TypeName = "char"), StringLength(64)]
    public string? PasswordHash { get; set; }

    [DisplayName("Login State")] public LoginState? LoginState { get; set; }
}