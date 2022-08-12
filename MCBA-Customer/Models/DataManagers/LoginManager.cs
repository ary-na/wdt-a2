using MCBA_Customer.Data;
using MCBA_Customer.ViewModels.Interfaces;
using MCBA_Model.Models;
using MCBA_Model.Models.Types;
using Microsoft.EntityFrameworkCore;
using SimpleHashing;

namespace MCBA_Customer.Models.DataManagers;

// Code sourced and adapted from:
// https://github.com/Job79/SimpleHashing
// https://stackoverflow.com/questions/18964488/findasync-with-non-primary-key-value

public class LoginManager
{
    private readonly McbaContext _context;
    public LoginManager(McbaContext context) => _context = context;

    public async Task<Login> GetLoginAsync(string? loginID) => await _context.Logins.FindAsync(loginID);

    public async Task<bool> IsLoggedInAsync(ILoginViewModel viewModel)
    {
        var login = await GetLoginAsync(viewModel.LoginID);
        return login != null && !string.IsNullOrEmpty(viewModel.Password) && login.LoginState != LoginState.Lock &&
               PBKDF2.Verify(login.PasswordHash, viewModel.Password);
    }

    public async Task ChangePasswordAsync(ILoginViewModel viewModel)
    {
        var login = await _context.Logins.Where(x => x.CustomerID == viewModel.CustomerID).FirstAsync();
        login.PasswordHash = PBKDF2.Hash(viewModel.Password);
        await _context.SaveChangesAsync();
    }
}