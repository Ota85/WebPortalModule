using EsonicModule.Models;

namespace EsonicModule.Services;

public class DataRepository : IDataRepository
{
    private static readonly List<DataItem> _data = new()
    {
        new DataItem { Id = 1, Name = "Item 1", Description = "First item", CreatedDate = DateTime.Now.AddDays(-5) },
        new DataItem { Id = 2, Name = "Item 2", Description = "Second item", CreatedDate = DateTime.Now.AddDays(-4) },
        new DataItem { Id = 3, Name = "Item 3", Description = "Third item", CreatedDate = DateTime.Now.AddDays(-3) },
        new DataItem { Id = 4, Name = "Item 4", Description = "Fourth item", CreatedDate = DateTime.Now.AddDays(-2) },
        new DataItem { Id = 5, Name = "Item 5", Description = "Fifth item", CreatedDate = DateTime.Now.AddDays(-1) }
    };

    public List<DataItem> GetAllData()
    {
        return _data.ToList();
    }

    public DataItem? GetDataById(int id)
    {
        return _data.FirstOrDefault(d => d.Id == id);
    }

    public DataItem CreateData(DataItem item)
    {
        item.Id = _data.Count > 0 ? _data.Max(d => d.Id) + 1 : 1;
        item.CreatedDate = DateTime.Now;
        _data.Add(item);
        return item;
    }

    public DataItem? UpdateData(DataItem item)
    {
        var existingItem = _data.FirstOrDefault(d => d.Id == item.Id);
        if (existingItem == null)
            return null;

        existingItem.Name = item.Name;
        existingItem.Description = item.Description;
        return existingItem;
    }

    public bool DeleteData(int id)
    {
        var item = _data.FirstOrDefault(d => d.Id == id);
        if (item == null)
            return false;

        _data.Remove(item);
        return true;
    }
}