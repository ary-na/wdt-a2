using MCBA_Customer.Data;
using MCBA_Model.Models;
using Microsoft.EntityFrameworkCore;

namespace MCBA_Customer.DataManagers;

// Code sourced and adapted from:
// https://stackoverflow.com/questions/10900250/select-all-rows-using-entity-framework

public class PayeeManager
{
    private readonly McbaContext _context;
    public PayeeManager(McbaContext context) => _context = context;
    public async Task<IEnumerable<Payee>> GetAllPayees() => await _context.Payees.ToListAsync();
}