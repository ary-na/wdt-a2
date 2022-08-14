using System.Text;
using MCBA_Model.Models;
using Newtonsoft.Json;

namespace MCBA_Admin.Models.DataManagers;

public class LoginsManager
{
    private readonly IHttpClientFactory _clientFactory;
    private HttpClient Client => _clientFactory.CreateClient("api");

    public LoginsManager(IHttpClientFactory clientFactory) => _clientFactory = clientFactory;

    public async Task<IEnumerable<Login>?> GetAllAsync()
    {
        var response = await Client.GetAsync("api/login");

        if (!response.IsSuccessStatusCode)
            throw new Exception();

        // Storing the response details received from web api
        var result = await response.Content.ReadAsStringAsync();

        // Deserializing the response received from web api and returning a list
        return JsonConvert.DeserializeObject<List<Login>>(result);
    }

    public async Task<Login?> GetAsync(string? loginID)
    {
        var response = await Client.GetAsync($"api/login/{loginID}");

        if (!response.IsSuccessStatusCode)
            throw new Exception();

        // Storing the response details received from web api
        var result = await response.Content.ReadAsStringAsync();

        // Deserializing the response received from web api and returning it
        return JsonConvert.DeserializeObject<Login>(result);
    }

    // Update login
    public async Task<bool> PutAsync(Login login)
    {
        var content = new StringContent(JsonConvert.SerializeObject(login), Encoding.UTF8, "application/json");

        var response = await Client.PutAsync("api/login", content);

        return response.IsSuccessStatusCode;
    }
}