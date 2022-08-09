using MCBA_Customer.Data;
using MCBA_Customer.ViewModels;
using MCBA_Customer.ViewModels.Interfaces;
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

    public async Task ChangePasswordAsync(ILoginViewModel viewModel)
    {
        var login = await _context.Logins.Where(x => x.CustomerID == viewModel.CustomerID).FirstAsync();
        login.PasswordHash = PBKDF2.Hash(viewModel.Password);
        await _context.SaveChangesAsync();
    }
}