namespace MCBA_Customer.ViewModels.Interfaces;

public interface ILoginViewModel
{
    public int CustomerID { get; set; }
    public string? LoginID { get; set; }
    public string? Password { get; set; }
    public string? ConfirmPassword { get; set; }
}