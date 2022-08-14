using MCBA_Customer.Data;
using MCBA_Customer.DTOs;
using MCBA_Model.Data;
using MCBA_Model.Models;
using MCBA_Model.Models.Types;
using Newtonsoft.Json;

namespace MCBA_Customer.Services;

// Code sourced and adapted from:
// Week 3 Tutorial - PersonWebService.cs
// Week 3 Tutorial - Primes.cs
// https://rmit.instructure.com/courses/102750/files/24402824?wrap=1

// Code sourced and adapted from:
// Week 6 Tutorial - McbaExampleWithLogin - SeedData.cs
// https://rmit.instructure.com/courses/102750/files/24426594?wrap=1

// Code sourced and adapted from:
// https://stackoverflow.com/questions/20120511/i-cant-await-awaitable

public static class CustomerWebService
{
    public static async Task GetAndSaveCustomersAsync(IServiceProvider serviceProvider)
    {
        var context = serviceProvider.GetRequiredService<McbaContext>();

        // Look for customers condition
        if (context.Customers.Any()) return;

        const string url = "https://coreteaching01.csit.rmit.edu.au/~e103884/wdt/services/customers/";
        using var client = new HttpClient();
        var json = await client.GetStringAsync(url);

        // Deserialize
        var customers = JsonConvert.DeserializeObject<List<CustomerDTO>>(json, new JsonSerializerSettings
        {
            // Code sourced and adapted from:
            // https://docs.microsoft.com/en-au/dotnet/standard/base-types/custom-date-and-time-format-strings
            DateFormatString = "dd/MM/yyyy hh:mm:ss tt"
        });

        if (customers is null) return;
        // Insert customers into database
        await InsertCustomersAsync(context, customers);
    }

    // Code sourced and adapted from:
    // https://stackoverflow.com/questions/29294582/what-are-the-benefits-of-inserting-the-data-into-the-database-asynchronously
    // https://www.c-sharpcorner.com/article/async-and-await-in-c-sharp/
    // https://stackoverflow.com/questions/68859259/how-to-await-until-parallel-task-done
    // https://docs.microsoft.com/en-us/dotnet/csharp/programming-guide/concepts/async/start-multiple-async-tasks-and-process-them-as-they-complete?pivots=dotnet-6-0
    // https://docs.microsoft.com/en-us/archive/msdn-magazine/2013/march/async-await-best-practices-in-asynchronous-programming
    // https://docs.microsoft.com/en-us/troubleshoot/sql/connect/error-message-when-you-connect
    // https://stackoverflow.com/questions/1202981/select-multiple-fields-from-list-in-linq
    // https://stackoverflow.com/questions/28588684/add-object-list-to-db-context-in-entity-framework


    private static async Task InsertCustomersAsync(McbaContext context, IEnumerable<CustomerDTO> customers)
    {
        foreach (var customer in customers)
        {
            await context.Customers.AddAsync(new Customer
            {
                CustomerID = customer.CustomerID,
                Name = customer.Name,
                Address = customer.Address,
                City = customer.City,
                Postcode = customer.PostCode
            });

            await context.Logins.AddAsync(new Login
            {
                LoginID = customer.Login?.LoginID,
                CustomerID = customer.CustomerID,
                PasswordHash = customer.Login?.PasswordHash
            });

            foreach (var account in customer.Accounts)
            {
                await context.Accounts.AddAsync(new Account
                {
                    AccountNumber = account.AccountNumber,
                    AccountType = account.AccountType == 'S' ? AccountType.Saving : AccountType.Checking,
                    CustomerID = account.CustomerId,
                });

                foreach (var transaction in account.Transactions)
                {
                    await context.Transactions.AddAsync(new Transaction
                    {
                        TransactionType = TransactionType.Deposit,
                        AccountNumber = account.AccountNumber,
                        Amount = transaction.Amount,
                        Comment = transaction.Comment,
                        TransactionTimeUtc = transaction.TransactionTimeUtc
                    });
                }
            }
        }

        await context.SaveChangesAsync();
    }
}