using System.ComponentModel.DataAnnotations;
using MCBA_Customer.ViewModels.Interfaces;

namespace MCBA_Customer.ViewModels;

public class LoginViewModel : ILoginViewModel
{
    public int CustomerID { get; set; }
    public string? LoginID { get; set; }
    
    [Required]
    public string? Password { get; set; }

    public string? ConfirmPassword { get; set; }
}