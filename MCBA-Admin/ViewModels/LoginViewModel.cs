using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace MCBA_Admin.ViewModels;

public class LoginViewModel

{
    [Required, DisplayName("Login ID")] public string? LoginID { get; set; }

    [Required] public string? Password { get; set; }
}