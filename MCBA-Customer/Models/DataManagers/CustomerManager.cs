using MCBA_Customer.Data;
using MCBA_Customer.ViewModels;
using MCBA_Model.Models;

namespace MCBA_Customer.Models.DataManagers;

public class CustomerManager
{
    private readonly McbaContext _context;
    public CustomerManager(McbaContext context) => _context = context;
    public async Task<Customer> GetCustomerAsync(int customerID) => await _context.Customers.FindAsync(customerID);

    public async Task EditProfileAsync(EditProfileViewModel viewModel)
    {
        var customer = await GetCustomerAsync(viewModel.CustomerID);

        customer.Name = viewModel.Name;
        customer.TFN = viewModel.TFN;
        customer.Address = viewModel.Address;
        customer.City = viewModel.City;
        customer.State = viewModel.State;
        customer.Postcode = viewModel.Postcode;
        customer.Mobile = viewModel.Mobile;

        await _context.SaveChangesAsync();
    }
}