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

    public async Task PrintZplAsync(string printerIp, int port, string zpl)
    {
        printerIp = "10.0.5.31";
        port = 9100;
        zpl = "^XA^FO50,50^ADN,36,20^FDPozdrav z Pardubic!^FS^XZ";


        using var client = new TcpClient();
        await client.ConnectAsync(printerIp, port);

        using NetworkStream stream = client.GetStream();
        byte[] data = Encoding.ASCII.GetBytes(zpl);

        await stream.WriteAsync(data, 0, data.Length);
        await stream.FlushAsync();
    }
}
