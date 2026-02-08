using EsonicModule.Models;

namespace EsonicModule.Services;

public class DataService
{
    private readonly IDataRepository _dataRepository;
    private readonly ILogger<DataService> _logger;

    public DataService(IDataRepository dataRepository, ILogger<DataService> logger)
    {
        _dataRepository = dataRepository;
        _logger = logger;
    }

    public async Task<List<DataItem>> GetDataAsync()
    {
        try
        {
            return await Task.FromResult(_dataRepository.GetAllData());
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error fetching data");
            return new List<DataItem>();
        }
    }

    public async Task<DataItem?> GetDataByIdAsync(int id)
    {
        try
        {
            return await Task.FromResult(_dataRepository.GetDataById(id));
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error fetching data item {Id}", id);
            return null;
        }
    }

    public async Task<DataItem?> CreateDataAsync(DataItem item)
    {
        try
        {
            return await Task.FromResult(_dataRepository.CreateData(item));
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error creating data item");
            return null;
        }
    }

    public async Task<DataItem?> UpdateDataAsync(DataItem item)
    {
        try
        {
            return await Task.FromResult(_dataRepository.UpdateData(item));
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error updating data item {Id}", item.Id);
            return null;
        }
    }

    public async Task<bool> DeleteDataAsync(int id)
    {
        try
        {
            return await Task.FromResult(_dataRepository.DeleteData(id));
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error deleting data item {Id}", id);
            return false;
        }
    }
}
