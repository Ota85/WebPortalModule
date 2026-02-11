using EsonicModule.Data;
using EsonicModule.Models;
using Microsoft.EntityFrameworkCore;
using System.Net.Sockets;
using System.Text;

namespace EsonicModule.Services;

public class MaterialStockStageService : IMaterialStockStageService
{
    private readonly SAPDataDbContext _context;

    public MaterialStockStageService(SAPDataDbContext context)
    {
        _context = context;
    }

    public async Task<List<MaterialStockStage>> GetAllAsync()
    {
        return await _context.MaterialStockStages.ToListAsync();
    }

    public async Task PrintZplAsync(MaterialStockStage item, PrinterSetting printer, ZebraTemplate template)
    {
        using var client = new TcpClient();
        await client.ConnectAsync(printer.IPAddress, printer.Port);

        using NetworkStream stream = client.GetStream();
        byte[] data = Encoding.ASCII.GetBytes(template.Template);

        await stream.WriteAsync(data, 0, data.Length);
        await stream.FlushAsync();
    }
}
