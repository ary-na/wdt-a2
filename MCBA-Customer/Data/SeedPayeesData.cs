using MCBA_Customer.Models;

namespace MCBA_Customer.Data;

// Code sourced and adapted from:
// Week 5 Lectorial - SeedData.cs

public static class SeedPayeesData
{
    public static async Task InitializePayeesAsync(IServiceProvider serviceProvider)
    {
        var context = serviceProvider.GetRequiredService<McbaContext>();

        // Look for payees condition
        if (context.Payees.Any())
            return;

        context.Payees.AddRange(
            new Payee
            {
                Name = "Telstra",
                Address = "123 Real Street",
                City = "Melbourne",
                State = "VIC",
                PostCode = "1234",
                Phone = "(03) 1234 1234"
            },
            new Payee
            {
                Name = "AGL",
                Address = "1 Fake Road",
                City = "Sydney",
                State = "NSW",
                PostCode = "2222",
                Phone = "(02) 2222 2222"
            },
            new Payee
            {
                Name = "South East Water",
                Address = "99 Authentic Avenue",
                City = "Perth",
                State = "WA",
                PostCode = "6000",
                Phone = "(08) 9999 9999"
            }
        );
        await context.SaveChangesAsync();
    }
}