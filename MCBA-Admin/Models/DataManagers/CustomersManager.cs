using System.Text;
using MCBA_Model.Models;
using Newtonsoft.Json;

namespace MCBA_Admin.Models.DataManagers;

public class CustomersManager
{
    private readonly IHttpClientFactory _clientFactory;
    private HttpClient Client => _clientFactory.CreateClient("api");

    public CustomersManager(IHttpClientFactory clientFactory) => _clientFactory = clientFactory;

    public async Task<IEnumerable<Customer>?> GetAllAsync()
    {
        var response = await Client.GetAsync("api/customer");

        if (!response.IsSuccessStatusCode)
            throw new Exception();

        // Storing the response details received from web api.
        var result = await response.Content.ReadAsStringAsync();

        // Deserializing the response received from web api and storing into a list.
        return JsonConvert.DeserializeObject<List<Customer>>(result);
    }

    public async Task<Customer> GetAsync(int customerID)
    {
        var response = await Client.GetAsync($"api/customer/{customerID}");

        if (!response.IsSuccessStatusCode)
            throw new Exception();

        // Storing the response details received from web api.
        var result = await response.Content.ReadAsStringAsync();

        // Deserializing the response received from web api and storing
        return JsonConvert.DeserializeObject<Customer>(result);
    }

    public async Task<bool> PutAsync(Customer customer)
    {
        var content = new StringContent(JsonConvert.SerializeObject(customer), Encoding.UTF8, "application/json");

        var response = await Client.PutAsync("api/customer", content);

        return response.IsSuccessStatusCode;
    }
}