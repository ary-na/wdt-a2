using MCBA_Customer.Data;
using MCBA_Customer.ViewModels;
using MCBA_Model.Models;
using MCBA_Model.Models.Types;
using X.PagedList;

namespace MCBA_Customer.Models.DataManagers;

// https://stackoverflow.com/questions/2978736/linq-and-conditional-sum
// https://stackoverflow.com/questions/72198966/efficiently-querying-ordering-by-sum-aggregate-in-related-model-in-efcore
// https://forum.uipath.com/t/is-there-linq-query-tow-subtract-tow-datatable-column-values-pls-help/130955
// https://www.jetbrains.com/help/resharper/InvertIf.html
// https://docs.microsoft.com/en-us/dotnet/csharp/whats-new/csharp-9#pattern-matching-enhancements

public class AccountManager
{
    private readonly McbaContext _context;

    public AccountManager(McbaContext context) => _context = context;

    public async Task<Account> GetAccountAsync(int accountNumber) =>
        await _context.Accounts.FindAsync(accountNumber);

    public async Task<BillPay> GetBillPayAsync(int billPayID) =>
        await _context.BillPays.FindAsync(billPayID);

    public async Task<decimal> GetAccountBalanceAsync(int accountNumber)
    {
        var accounts = await _context.Accounts.FindAsync(accountNumber);
        return accounts.Transactions
                   .Where(x => x.TransactionType is TransactionType.Deposit or TransactionType.TransferIn)
                   .Sum(x => x.Amount) -
               accounts.Transactions
                   .Where(x => x.TransactionType is not (TransactionType.Deposit or TransactionType.TransferIn))
                   .Sum(x => x.Amount);
    }

    public async Task<IPagedList<Transaction>> GetTransactionsAsync(int accountNumber, int page)
    {
        var accounts = await _context.Accounts.FindAsync(accountNumber);
        const int pageSize = 4;
        return await accounts.Transactions
            .OrderByDescending(x => x.TransactionTimeUtc)
            .ToPagedListAsync(page, pageSize);
    }

    private async Task<int> GetTransactionCountAsync(int accountNumber)
    {
        var accounts = await _context.Accounts.FindAsync(accountNumber);
        return accounts.Transactions.Count(x =>
            x.TransactionType is TransactionType.Withdraw or TransactionType.TransferOut);
    }

    public async Task<IPagedList<BillPay>> GetBillPaysAsync(int accountNumber, int page)
    {
        var accounts = await _context.Accounts.FindAsync(accountNumber);
        const int pageSize = 4;
        return await accounts.BillPays
            .OrderByDescending(x => x.ScheduleTimeUtc)
            .ToPagedListAsync(page, pageSize);
    }

    public async Task DepositAsync(DepositViewModel viewModel)
    {
        var account = await _context.Accounts.FindAsync(viewModel.Account?.AccountNumber);
        account?.Transactions?.Add(
            new Transaction
            {
                TransactionType = viewModel.TransactionType,
                Amount = viewModel.Amount,
                Comment = viewModel.Comment,
                TransactionTimeUtc = DateTime.UtcNow
            });

        await _context.SaveChangesAsync();
    }

    public async Task<bool> WithdrawAsync(WithdrawViewModel viewModel)
    {
        var account = await _context.Accounts.FindAsync(viewModel.Account?.AccountNumber);
        account.Balance = await GetAccountBalanceAsync(viewModel.AccountNumber);

        if (viewModel.Amount > account.AvailableBalance) return false;
        account.Transactions?.Add(
            new Transaction
            {
                TransactionType = viewModel.TransactionType,
                Amount = viewModel.Amount,
                Comment = viewModel.Comment,
                TransactionTimeUtc = DateTime.UtcNow
            });

        if (!account.AvailableFreeTransaction(await GetTransactionCountAsync(account.AccountNumber)))
            await ServiceChargeAsync(account.AccountNumber, viewModel.TransactionType);
        await _context.SaveChangesAsync();
        return true;
    }

    public async Task<bool> TransferAsync(TransferViewModel viewModel)
    {
        var account = await _context.Accounts.FindAsync(viewModel.Account?.AccountNumber);
        var destinationAccount = await _context.Accounts.FindAsync(viewModel.DestinationAccountNumber);
        account.Balance = await GetAccountBalanceAsync(viewModel.AccountNumber);

        if (viewModel.Amount > account.AvailableBalance ||
            account.AccountNumber == destinationAccount.AccountNumber) return false;

        account.Transactions?.Add(
            new Transaction
            {
                TransactionType = viewModel.TransactionType,
                Amount = viewModel.Amount,
                DestinationAccountNumber = viewModel.DestinationAccountNumber,
                Comment = viewModel.Comment,
                TransactionTimeUtc = DateTime.UtcNow
            });

        destinationAccount?.Transactions?.Add(
            new Transaction
            {
                TransactionType = TransactionType.TransferIn,
                Amount = viewModel.Amount,
                Comment = viewModel.Comment,
                TransactionTimeUtc = DateTime.UtcNow
            });

        if (!account.AvailableFreeTransaction(await GetTransactionCountAsync(account.AccountNumber)))
            await ServiceChargeAsync(account.AccountNumber, viewModel.TransactionType);
        await _context.SaveChangesAsync();
        return true;
    }

    private async Task ServiceChargeAsync(int accountNumber, TransactionType transactionType)
    {
        var account = await _context.Accounts.FindAsync(accountNumber);

        account?.Transactions?.Add(
            new Transaction
            {
                TransactionType = TransactionType.ServiceCharge,
                Amount = Transaction.ServiceCharge[transactionType],
                TransactionTimeUtc = DateTime.UtcNow
            });
    }

    public async Task BillPayAsync(BillPayViewModel viewModel)
    {
        var account = await GetAccountAsync(viewModel.AccountNumber);
        account?.BillPays.Add(
            new BillPay
            {
                PayeeID = viewModel.PayeeID,
                Amount = viewModel.Amount,
                ScheduleTimeUtc = viewModel.ScheduleTime.ToUniversalTime(),
                Period = viewModel.Period,
                BillStatus = BillPayStatus.Pending
            });

        await _context.SaveChangesAsync();
    }

    public async Task<bool> UpdateBillPayAsync(BillPay billPay)
    {
        if (billPay.BillStatus == BillPayStatus.Blocked) return false;
        billPay.ScheduleTimeUtc = billPay.ScheduleTimeUtc.ToUniversalTime();
        billPay.BillStatus = BillPayStatus.Pending;
        _context.BillPays.Update(billPay);
        await _context.SaveChangesAsync();
        return true;
    }

    public async Task DeleteBillPayAsync(BillPay billPay)
    {
        _context.BillPays.Remove(billPay);
        await _context.SaveChangesAsync();
    }
}