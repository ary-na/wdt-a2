using MCBA_Customer.Data;
using MCBA_Model.Models;
using MCBA_Model.Models.Types;
using Microsoft.EntityFrameworkCore;

namespace MCBA_Customer.BackgroundServices;

// Code sourced and adapted from:
// Week 8 Lectorial - PeopleBackgroundService.cs

// https://docs.microsoft.com/en-us/dotnet/api/system.datetime.addmonths?view=net-6.0
// https://docs.microsoft.com/en-us/visualstudio/ide/reference/invert-if-statement?view=vs-2022

public class BillPayBackgroundService : BackgroundService
{
    private readonly IServiceProvider _service;
    private readonly ILogger<BillPayBackgroundService> _logger;

    public BillPayBackgroundService(IServiceProvider service, ILogger<BillPayBackgroundService> logger)
    {
        _service = service;
        _logger = logger;
    }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        _logger.LogInformation("Bill Pay background service started");

        while (!stoppingToken.IsCancellationRequested)
        {
            await PayBills(stoppingToken);
            _logger.LogInformation("Bill pay background service delayed for a minute");
            await Task.Delay(TimeSpan.FromMinutes(1), stoppingToken);
        }
    }

    private async Task PayBills(CancellationToken stoppingToken)
    {
        _logger.LogInformation("Bill Pay background service is working");

        await using var scope = _service.CreateAsyncScope();
        var context = scope.ServiceProvider.GetRequiredService<McbaContext>();

        var BillPays = await context.BillPays
            .Where(x => x.ScheduleTimeUtc <= DateTime.UtcNow && x.BillStatus == BillPayStatus.Pending)
            .ToListAsync(stoppingToken);

        foreach (var billPay in BillPays)
        {
            var account = await context.Accounts.FindAsync(billPay.AccountNumber);

            if (billPay.Amount <= await GetAccountBalance(billPay.AccountNumber))
            {
                account.Transactions.Add(
                    new Transaction
                    {
                        TransactionType = TransactionType.BillPay,
                        Amount = billPay.Amount,
                        Comment = "Bill Pay Service",
                        TransactionTimeUtc = DateTime.UtcNow
                    }
                );
                billPay.BillStatus = BillPayStatus.Paid;
            }
            else
            {
                billPay.BillStatus = BillPayStatus.Failed;
            }

            if (billPay.Period == BillPayPeriod.OneOff || billPay.BillStatus is BillPayStatus.Failed or BillPayStatus.Blocked) continue;
            billPay.ScheduleTimeUtc = billPay.ScheduleTimeUtc.AddMonths(1);
            billPay.BillStatus = BillPayStatus.Pending;
        }

        await context.SaveChangesAsync(stoppingToken);
        _logger.LogInformation("Bill Pay background service completed");
    }

    private async Task<decimal> GetAccountBalance(int accountNumber)
    {
        await using var scope = _service.CreateAsyncScope();
        var context = scope.ServiceProvider.GetRequiredService<McbaContext>();
        var accounts = await context.Accounts.FindAsync(accountNumber);
        return accounts.Transactions
                   .Where(x => x.TransactionType is TransactionType.Deposit or TransactionType.TransferIn)
                   .Sum(x => x.Amount) -
               accounts.Transactions
                   .Where(x => x.TransactionType is not (TransactionType.Deposit or TransactionType.TransferIn))
                   .Sum(x => x.Amount);
    }
}