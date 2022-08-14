using MCBA_Model.Data;
using MCBA_Model.Models;
using MCBA_Web_API.Models.Repository;

namespace MCBA_Web_API.Models.DataManagers;

public class TransactionManger : IDataRepository<Transaction, int>
{
    private readonly McbaContext _context;

    public TransactionManger(McbaContext context)
    {
        _context = context;
    }

    public IEnumerable<Transaction> GetAll()
    {
        return _context.Transactions.ToList();
    }

    public Transaction Get(int id)
    {
        return _context.Transactions.Find(id);
    }

    public int Add(Transaction transaction)
    {
        _context.Transactions?.Add(transaction);
        _context.SaveChanges();

        return transaction.TransactionID;
    }

    public int Update(int id, Transaction transaction)
    {
        _context.Update(transaction);
        _context.SaveChanges();

        return id;
    }

    public int Delete(int id)
    {
        _context.Transactions?.Remove(_context.Transactions.Find(id));
        _context.SaveChanges();

        return id;
    }
}