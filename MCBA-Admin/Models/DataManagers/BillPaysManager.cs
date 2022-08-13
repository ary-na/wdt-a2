using System.Text;
using MCBA_Model.Models;
using MCBA_Model.Models.Types;
using Newtonsoft.Json;
using X.PagedList;

namespace MCBA_Admin.Models.DataManagers;

public class BillPaysManager
{
    private readonly IHttpClientFactory _clientFactory;
    private HttpClient Client => _clientFactory.CreateClient("api");

    public BillPaysManager(IHttpClientFactory clientFactory) => _clientFactory = clientFactory;

    private async Task<IEnumerable<BillPay>?> GetBillPaysAsync()
    {
        var response = await Client.GetAsync("api/billpay");

        if (!response.IsSuccessStatusCode)
            throw new Exception();

        // Storing the response details received from web api.
        var result = await response.Content.ReadAsStringAsync();

        // Deserializing the response received from web api and storing into a list.
        return JsonConvert.DeserializeObject<List<BillPay>>(result);
    }

    public async Task<BillPay?> GetAsync(int billPayID)
    {
        var response = await Client.GetAsync($"api/billpay/{billPayID}");

        if (!response.IsSuccessStatusCode)
            throw new Exception();

        // Storing the response details received from web api.
        var result = await response.Content.ReadAsStringAsync();

        // Deserializing the response received from web api and storing
        return JsonConvert.DeserializeObject<BillPay>(result);
    }

    public async Task<IPagedList<BillPay>> GetPagedBillPaysAsync(int page)
    {
        var billPays = await GetBillPaysAsync();
        const int pageSize = 4;
        return await billPays.Where(x => x.BillStatus is BillPayStatus.Pending or BillPayStatus.Blocked)
            .OrderByDescending(x => x.ScheduleTimeUtc)
            .ToPagedListAsync(page, pageSize);
    }

    public async Task<bool> PutAsync(BillPay billPay)
    {
        var content = new StringContent(JsonConvert.SerializeObject(billPay), Encoding.UTF8, "application/json");

        var response = await Client.PutAsync("api/billpay", content);

        return response.IsSuccessStatusCode;
    }
}