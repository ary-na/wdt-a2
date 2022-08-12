using MCBA_Admin.ViewModels;

namespace MCBA_Admin.Models.DataManagers;

// Code sourced and adapted form:
// https://docs.microsoft.com/en-us/dotnet/csharp/language-reference/keywords/const

public class LoginManager
{
    private const int AdminID = 9999;
    private const string AdminName = "admin";

    public bool IsLoggedIn(LoginViewModel viewModel)
    {
        const string password = "admin";
        return viewModel.Password != null && !string.IsNullOrEmpty(viewModel.Password) &&
               viewModel.LoginID.Equals(AdminName) && viewModel.Password.Equals(password);
    }

    public Admin GetAdmin()
    {
        return new Admin
        {
            AdminID = AdminID,
            Name = AdminName
        };
    }
}