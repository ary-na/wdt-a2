using MCBA_Model.Models;
using Newtonsoft.Json;
using X.PagedList;

namespace MCBA_Admin.Models.DataManagers;

// Code sourced and adapted from:
// https://stackoverflow.com/questions/21905733/how-to-check-if-a-datetime-field-is-not-null-or-empty

public class AccountsManager
{
    private readonly IHttpClientFactory _clientFactory;
    private HttpClient Client => _clientFactory.CreateClient("api");

    public AccountsManager(IHttpClientFactory clientFactory) => _clientFactory = clientFactory;

    public async Task<IEnumerable<Account>?> GetAll()
    {
        var response = await Client.GetAsync("api/account");

        if (!response.IsSuccessStatusCode)
            throw new Exception();

        // Storing the response details received from web api.
        var result = await response.Content.ReadAsStringAsync();

        // Deserializing the response received from web api and storing into a list.
        return JsonConvert.DeserializeObject<List<Account>>(result);
    }

    private async Task<IEnumerable<Transaction>?> GetAccountTransactionsAsync(int accountNumber, DateTime? from,
        DateTime? to)
    {
        var response = await Client.GetAsync($"api/account/{accountNumber}");

        if (!response.IsSuccessStatusCode)
            throw new Exception();

        // Storing the response details received from web api.
        var result = await response.Content.ReadAsStringAsync();

        // Deserializing the response received from web api and storing into a list.
        var transactions = JsonConvert.DeserializeObject<List<Transaction>>(result);

        if (from.HasValue && to.HasValue)
            return await transactions.Where(x =>
                    x.TransactionTimeUtc >= from?.ToUniversalTime() && x.TransactionTimeUtc <= to?.ToUniversalTime())
                .ToListAsync();

        if (from.HasValue)
            return await transactions.Where(x => x.TransactionTimeUtc >= from?.ToUniversalTime()).ToListAsync();

        if (to.HasValue)
            return await transactions.Where(x => x.TransactionTimeUtc <= to?.ToUniversalTime()).ToListAsync();

        return transactions;
    }

    public async Task<IPagedList<Transaction>> GetPagedAccountTransactionsAsync(int accountNumber, DateTime? from,
        DateTime? to, int page)
    {
        var transactions = await GetAccountTransactionsAsync(accountNumber, from, to);
        const int pageSize = 4;
        return await transactions
            .OrderByDescending(x => x.TransactionTimeUtc)
            .ToPagedListAsync(page, pageSize);
    }
}