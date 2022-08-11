using MCBA_Customer.Data;
using MCBA_Model.Models;
using MCBA_Web_API.Models.Repository;

namespace MCBA_Web_API.Models.DataManagers;

public class BillPayManager : IDataRepository<BillPay, int>
{
    private readonly McbaContext _context;

    public BillPayManager(McbaContext context)
    {
        _context = context;
    }

    public IEnumerable<BillPay> GetAll()
    {
        return _context.BillPays.ToList();
    }

    public BillPay Get(int id)
    {
        return _context.BillPays.Find(id);
    }

    public int Add(BillPay billPay)
    {
        _context.BillPays?.Add(billPay);
        _context.SaveChanges();

        return billPay.BillPayID;
    }

    public int Update(int id, BillPay billPay)
    {
        _context.Update(billPay);
        _context.SaveChanges();

        return id;
    }

    public int Delete(int id)
    {
        _context.BillPays?.Remove(_context.BillPays.Find(id));
        _context.SaveChanges();
        return id;
    }
}