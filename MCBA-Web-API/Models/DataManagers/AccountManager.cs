using MCBA_Model.Data;
using MCBA_Model.Models;
using MCBA_Web_API.Models.Repository;

namespace MCBA_Web_API.Models.DataManagers;

// Code sourced and adapted from:
// Week 9 Lectorial - MovieManager.cs
// https://rmit.instructure.com/courses/102750/files/26419005?wrap=1

public class AccountManager : IDataRepository<Account, int>
{
    private readonly McbaContext _context;

    public AccountManager(McbaContext context)
    {
        _context = context;
    }

    public IEnumerable<Account> GetAll()
    {
        return _context.Accounts.ToList();
    }

    public Account Get(int id)
    {
        return _context.Accounts.Find(id);
    }

    public int Add(Account account)
    {
        _context.Accounts?.Add(account);
        _context.SaveChanges();
        return account.AccountNumber;
    }

    public int Update(int id, Account account)
    {
        _context.Update(account);
        _context.SaveChanges();
        return id;
    }

    public int Delete(int id)
    {
        _context.Accounts?.Remove(_context.Accounts.Find(id));
        _context.SaveChanges();
        return id;
    }

    public IEnumerable<Transaction> GetTransactions(int id)
    {
        return _context.Transactions.Where(x => x.AccountNumber == id).ToList();
    }
}