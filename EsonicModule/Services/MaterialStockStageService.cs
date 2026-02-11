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

    public async Task<List<MaterialStockStageDto>> GetAllAsync()
    {
        var entities = await _context.MaterialStockStages.ToListAsync();
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
