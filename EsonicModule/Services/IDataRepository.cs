using EsonicModule.Models;

namespace EsonicModule.Services;

public interface IDataRepository
{
    List<DataItem> GetAllData();
    DataItem? GetDataById(int id);
    DataItem CreateData(DataItem item);
    DataItem? UpdateData(DataItem item);
    bool DeleteData(int id);
}