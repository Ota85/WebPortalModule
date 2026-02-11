using EsonicModule.DTOs;

namespace EsonicModule.Services;

public interface IMaterialStockStageService
{
    Task<List<MaterialStockStageDto>> GetAllAsync();
    Task PrintZplAsync(MaterialStockStageDto item, PrinterSettingDto printer, ZebraTemplateDto template);
}
