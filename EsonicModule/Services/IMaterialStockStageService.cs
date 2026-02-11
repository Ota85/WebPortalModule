using EsonicModule.DTOs;

namespace EsonicModule.Services;

public interface IMaterialStockStageService
{
    Task<List<MaterialStockStageDto>> GetAllAsync(DateTime? dateFrom = null, DateTime? dateTo = null);
    Task PrintZplAsync(MaterialStockStageDto item, PrinterSettingDto printer, ZebraTemplateDto template);
}
