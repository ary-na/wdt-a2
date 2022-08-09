using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using MCBA_Customer.Attributes;
using MCBA_Customer.ViewModels.Interfaces;

namespace MCBA_Customer.ViewModels;

public class ChangePasswordViewModel : ILoginViewModel
{
    public int CustomerID { get; set; }
    public string? LoginID { get; set; }
    
    [Required]
    public string? Password { get; set; }
    
    [Required, PasswordDoesNotMatch, DisplayName("Confirm Password")]
    public string? ConfirmPassword { get; set; }
}