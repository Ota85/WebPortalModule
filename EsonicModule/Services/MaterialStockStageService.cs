using AutoMapper;
using EsonicModule.Data;
using EsonicModule.DTOs;
using EsonicModule.Models;
using Microsoft.EntityFrameworkCore;
using System.Net.Sockets;
using System.Text;

namespace EsonicModule.Services;

public class MaterialStockStageService : IMaterialStockStageService
{
    private readonly SAPDataDbContext _context;
    private readonly IMapper _mapper;

    public MaterialStockStageService(SAPDataDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<List<MaterialStockStageDto>> GetAllAsync(DateTime? dateFrom = null, DateTime? dateTo = null)
    {
        var query = _context.MaterialStockStages.AsQueryable();

        // Apply date filtering if provided
        if (dateFrom.HasValue)
        {
            // Filter by date only (ignore time component)
            var dateFromStart = dateFrom.Value.Date;
            query = query.Where(x => x.TimeStamp.HasValue && x.TimeStamp.Value >= dateFromStart);
        }

        if (dateTo.HasValue)
        {
            // Filter by date only - include the entire day (up to 23:59:59.999)
            var dateToEnd = dateTo.Value.Date.AddDays(1).AddTicks(-1);
            query = query.Where(x => x.TimeStamp.HasValue && x.TimeStamp.Value <= dateToEnd);
        }

        // Filter out null TimeStamp values and order by TimeStamp descending, then take top 250
        var entities = await query
            .Where(x => x.TimeStamp.HasValue)
            .OrderByDescending(x => x.TimeStamp)
            .Take(250)
            .ToListAsync();

        return _mapper.Map<List<MaterialStockStageDto>>(entities);
    }

    public async Task PrintZplAsync(MaterialStockStageDto item, PrinterSettingDto printer, ZebraTemplateDto template)
    {
        using var client = new TcpClient();
        await client.ConnectAsync(printer.IPAddress, printer.Port);

        using NetworkStream stream = client.GetStream();
        byte[] data = Encoding.ASCII.GetBytes(template.Template);

        await stream.WriteAsync(data, 0, data.Length);
        await stream.FlushAsync();
    }
}
