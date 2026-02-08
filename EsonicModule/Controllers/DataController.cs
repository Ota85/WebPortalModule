using Microsoft.AspNetCore.Mvc;
using EsonicModule.Models;
using EsonicModule.Services;

namespace EsonicModule.Controllers;

[ApiController]
[Route("api/[controller]")]
public class DataController : ControllerBase
{
    private readonly IDataRepository _dataRepository;

    public DataController(IDataRepository dataRepository)
    {
        _dataRepository = dataRepository;
    }

    [HttpGet]
    public ActionResult<IEnumerable<DataItem>> GetData()
    {
        var data = _dataRepository.GetAllData();
        return Ok(data);
    }

    [HttpGet("{id}")]
    public ActionResult<DataItem> GetDataById(int id)
    {
        var item = _dataRepository.GetDataById(id);
        
        if (item == null)
        {
            return NotFound();
        }

        return Ok(item);
    }

    [HttpPost]
    public ActionResult<DataItem> CreateData([FromBody] DataItem item)
    {
        if (item == null)
        {
            return BadRequest();
        }

        var createdItem = _dataRepository.CreateData(item);
        return CreatedAtAction(nameof(GetDataById), new { id = createdItem.Id }, createdItem);
    }

    [HttpPut("{id}")]
    public ActionResult<DataItem> UpdateData(int id, [FromBody] DataItem item)
    {
        if (item == null || id != item.Id)
        {
            return BadRequest();
        }

        var updatedItem = _dataRepository.UpdateData(item);
        if (updatedItem == null)
        {
            return NotFound();
        }

        return Ok(updatedItem);
    }

    [HttpDelete("{id}")]
    public ActionResult DeleteData(int id)
    {
        var success = _dataRepository.DeleteData(id);
        if (!success)
        {
            return NotFound();
        }

        return NoContent();
    }
}