using EsonicModule.Models;

namespace EsonicModule.Services;

public interface IPrinterSettingService
{
    Task<List<PrinterSetting>> GetAllAsync();
    Task SaveChangesAsync(List<PrinterSetting> printerSettings);
}
