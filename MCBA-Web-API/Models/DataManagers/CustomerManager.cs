using MCBA_Model.Data;
using MCBA_Model.Models;
using MCBA_Web_API.Models.Repository;

namespace MCBA_Web_API.Models.DataManagers;

public class CustomerManager : IDataRepository<Customer, int>
{
    private readonly McbaContext _context;

    public CustomerManager(McbaContext context)
    {
        _context = context;
    }

    public IEnumerable<Customer> GetAll()
    {
        return _context.Customers.ToList();
    }

    public Customer Get(int id)
    {
        return _context.Customers.Find(id);
    }

    public int Add(Customer customer)
    {
        _context.Customers?.Add(customer);
        _context.SaveChanges();

        return customer.CustomerID;
    }

    public int Update(int id, Customer customer)
    {
        _context.Update(customer);
        _context.SaveChanges();

        return id;
    }

    public int Delete(int id)
    {
        _context.Customers?.Remove(_context.Customers.Find(id));
        _context.SaveChanges();

        return id;
    }
}