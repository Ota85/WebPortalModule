using EsonicModule.Models;

namespace EsonicModule.Services;

public interface IMaterialStockStageService
{
    Task<List<MaterialStockStage>> GetAllAsync();
    Task PrintZplAsync(MaterialStockStage item, PrinterSetting printer, ZebraTemplate template);
}
