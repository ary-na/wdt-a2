using Newtonsoft.Json;
using s3910902_a2.Data;
using s3910902_a2.DTOs;
using s3910902_a2.Models;

namespace s3910902_a2.Services;

// Week 3 Tutorial - PersonWebService.cs
// Week 3 Tutorial - Primes.cs
// https://rmit.instructure.com/courses/102750/files/24402824?wrap=1

// Code sourced and adapted from:
// Week 6 Tutorial - McbaExampleWithLogin - SeedData.cs
// https://rmit.instructure.com/courses/102750/files/24426594?wrap=1

public static class CustomerWebService
{
    public static async Task GetAndSaveCustomer(IServiceProvider serviceProvider)
    {
        var context = serviceProvider.GetRequiredService<McbaContext>();

        // Any customers in database condition
        if (context.Customers.Any()) return;

        const string url = "https://coreteaching01.csit.rmit.edu.au/~e103884/wdt/services/customers/";
        using var client = new HttpClient();
        var json = client.GetStringAsync(url).Result;

        // Deserialize
        var customers = JsonConvert.DeserializeObject<List<CustomerDTO>>(json, new JsonSerializerSettings
        {
            // Code sourced and adapted from:
            // https://docs.microsoft.com/en-au/dotnet/standard/base-types/custom-date-and-time-format-strings
            DateFormatString = "dd/MM/yyyy hh:mm:ss tt"
        });
        
        if (customers is null) return;
        
        // Insert customers into database
        await InsertCustomers(context, customers);
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
    

    private static async Task InsertCustomers(McbaContext context, IEnumerable<CustomerDTO> customers)
    {
        foreach (var customer in customers)
        {
            await context.Customers.AddAsync(new Customer
            {
                CustomerID = customer.CustomerID,
                Name = customer.Name,
                Address = customer.Address,
                City = customer.City,
                PostCode = customer.PostCode
            });

            await context.Logins.AddAsync(new Login
            {
                LoginID = customer.Login?.LoginID,
                CustomerID = customer.CustomerID,
                PasswordHash = customer.Login?.PasswordHash
            });

            foreach (var account in customer.Accounts)
            {
                account.Transactions?.ForEach(x => account.Balance += x.Amount);
                await context.Accounts.AddAsync(new Account
                {
                    AccountNumber = account.AccountNumber,
                    AccountType = account.AccountType == 'S' ? AccountType.Saving : AccountType.Checking,
                    CustomerID = account.CustomerId,
                    Balance = account.Balance
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