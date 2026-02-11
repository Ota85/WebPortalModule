using EsonicModule.DTOs;

namespace EsonicModule.Services;

public interface IPrinterSettingService
{
    Task<List<PrinterSettingDto>> GetAllAsync();
    Task SaveChangesAsync(List<PrinterSettingDto> printerSettings);
}
