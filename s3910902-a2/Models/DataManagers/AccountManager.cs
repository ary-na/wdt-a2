using s3910902_a2.Data;
using s3910902_a2.Models.Types;
using s3910902_a2.ViewModels;

namespace s3910902_a2.Models.DataManagers;

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

    public async Task<decimal> GetAccountBalanceAsync(int accountNumber)
    {
        var accounts = await _context.Accounts.FindAsync(accountNumber);
        return accounts.Transactions
                   .Where(x => x.TransactionType is TransactionType.Deposit or TransactionType.TransferIncoming)
                   .Sum(x => x.Amount) -
               accounts.Transactions
                   .Where(x => x.TransactionType is not (TransactionType.Deposit or TransactionType.TransferIncoming))
                   .Sum(x => x.Amount);
    }

    public async Task DepositAsync(TransactionViewModel viewModel)
    {
        var account = await _context.Accounts.FindAsync(viewModel.Account?.AccountNumber);
        account?.Transactions?.Add(
            new Transaction
            {
                TransactionType = TransactionType.Deposit,
                Amount = viewModel.Amount,
                Comment = viewModel.Comment,
                TransactionTimeUtc = DateTime.UtcNow
            });

        await _context.SaveChangesAsync();
    }

    public async Task WithdrawAsync(TransactionViewModel viewModel)
    {
        var account = await _context.Accounts.FindAsync(viewModel.Account?.AccountNumber);
        account.Balance = await GetAccountBalanceAsync(viewModel.AccountNumber);

        if (viewModel.Amount <= account.AvailableBalance)
        {
            account.Transactions?.Add(
                new Transaction
                {
                    TransactionType = TransactionType.Withdraw,
                    Amount = viewModel.Amount,
                    Comment = viewModel.Comment,
                    TransactionTimeUtc = DateTime.UtcNow
                });

            await _context.SaveChangesAsync();
        }
    }

    public async Task TransferAsync(TransactionViewModel viewModel)
    {
        var account = await _context.Accounts.FindAsync(viewModel.Account?.AccountNumber);
        var destinationAccount = await _context.Accounts.FindAsync(viewModel.DestinationAccountNumber);

        account.Transactions?.Add(
            new Transaction
            {
                TransactionType = TransactionType.Transfer,
                Amount = viewModel.Amount,
                DestinationAccountNumber = viewModel.DestinationAccountNumber,
                Comment = viewModel.Comment,
                TransactionTimeUtc = DateTime.UtcNow
            }
        );

        destinationAccount?.Transactions?.Add(
            new Transaction
            {
                TransactionType = TransactionType.TransferIncoming,
                Amount = viewModel.Amount,
                Comment = viewModel.Comment,
                TransactionTimeUtc = DateTime.UtcNow
            }
        );

        await _context.SaveChangesAsync();
    }

    public async Task BillPayAsync(TransactionViewModel viewModel)
    {
        var account = await _context.Accounts.FindAsync(viewModel.Account?.AccountNumber);
    }
}