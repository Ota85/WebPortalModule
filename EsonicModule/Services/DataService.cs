using EsonicModule.Models;
using System.Net.Http.Json;

namespace EsonicModule.Services;

public class DataService
{
    private readonly HttpClient _httpClient;
    private readonly ILogger<DataService> _logger;

    public DataService(HttpClient httpClient, ILogger<DataService> logger)
    {
        _httpClient = httpClient;
        _logger = logger;
    }

    public async Task<List<DataItem>> GetDataAsync()
    {
        try
        {
            var data = await _httpClient.GetFromJsonAsync<List<DataItem>>("api/Data");
            return data ?? new List<DataItem>();
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error fetching data from API");
            return new List<DataItem>();
        }
    }

    public async Task<DataItem?> GetDataByIdAsync(int id)
    {
        try
        {
            return await _httpClient.GetFromJsonAsync<DataItem>($"api/Data/{id}");
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error fetching data item {Id} from API", id);
            return null;
        }
    }
}
