using MCBA_Customer.Data;
using MCBA_Model.Models;
using MCBA_Model.Models.Types;
using MCBA_Web_API.Models.Repository;
using Microsoft.EntityFrameworkCore;

namespace MCBA_Web_API.Models.DataManagers;

public class LoginManager : IDataRepository<Login, string?>
{
    private readonly McbaContext _context;

    public LoginManager(McbaContext context)
    {
        _context = context;
    }

    public IEnumerable<Login> GetAll()
    {
        return _context.Logins.ToList();
    }

    public Login Get(string? id)
    {
        return _context.Logins.Find(id);
    }

    public string? Add(Login login)
    {
        _context.Logins?.Add(login);
        _context.SaveChanges();

        return login.LoginID;
    }

    public string? Update(string? id, Login login)
    {
        _context.Update(login);
        _context.SaveChanges();
            
        return id;
    }

    public string? Delete(string? id)
    {
        _context.Logins?.Remove(_context.Logins.Find(id));
        _context.SaveChanges();

        return id;
    }
}