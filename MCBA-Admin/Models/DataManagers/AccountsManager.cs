using MCBA_Model.Models;
using Newtonsoft.Json;

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

    public async Task<IEnumerable<Transaction>?> GetAccountTransactions(int accountNumber, DateTime? from, DateTime? to)
    {
        var response = await Client.GetAsync($"api/account/{accountNumber}");

        if (!response.IsSuccessStatusCode)
            throw new Exception();

        // Storing the response details received from web api.
        var result = await response.Content.ReadAsStringAsync();

        // Deserializing the response received from web api and storing into a list.
        var transactions = JsonConvert.DeserializeObject<List<Transaction>>(result);

        if (from is null && to is null)
        {
            return transactions;
        }

        if (from is not null && to is null)
        {
            return transactions.Where(x => x.TransactionTimeUtc >= from);
        }

        if (from is null && to is not null)
        {
            return transactions.Where(x => x.TransactionTimeUtc <= to);
        }

        return transactions.Where(x => x.TransactionTimeUtc >= from && x.TransactionTimeUtc <= to);
    }
}