using Microsoft.AspNetCore.Mvc;
using EsonicApi.Models;

namespace EsonicApi.Controllers;

[ApiController]
[Route("api/[controller]")]
public class DataController : ControllerBase
{
    private static List<DataItem> GetDummyData()
    {
        return new List<DataItem>
        {
            new DataItem { Id = 1, Name = "Item 1", Description = "First item", CreatedDate = DateTime.Now.AddDays(-5) },
            new DataItem { Id = 2, Name = "Item 2", Description = "Second item", CreatedDate = DateTime.Now.AddDays(-4) },
            new DataItem { Id = 3, Name = "Item 3", Description = "Third item", CreatedDate = DateTime.Now.AddDays(-3) },
            new DataItem { Id = 4, Name = "Item 4", Description = "Fourth item", CreatedDate = DateTime.Now.AddDays(-2) },
            new DataItem { Id = 5, Name = "Item 5", Description = "Fifth item", CreatedDate = DateTime.Now.AddDays(-1) }
        };
    }

    [HttpGet]
    public ActionResult<IEnumerable<DataItem>> GetData()
    {
        var data = GetDummyData();
        return Ok(data);
    }

    [HttpGet("{id}")]
    public ActionResult<DataItem> GetDataById(int id)
    {
        var data = GetDummyData();
        var item = data.FirstOrDefault(d => d.Id == id);
        
        if (item == null)
        {
            return NotFound();
        }

        return Ok(item);
    }
}
