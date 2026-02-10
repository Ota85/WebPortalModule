using EsonicModule.Models;

namespace EsonicModule.Services;

public interface IMaterialStockStageService
{
    Task<List<MaterialStockStage>> GetAllAsync();
    Task PrintZplAsync(string printerIp, int port, string zpl);
}
