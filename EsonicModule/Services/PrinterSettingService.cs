using Microsoft.EntityFrameworkCore;
using EsonicModule.Data;
using EsonicModule.Models;

namespace EsonicModule.Services;

public class PrinterSettingService : IPrinterSettingService
{
    private readonly SAPDataDbContext _context;

    public PrinterSettingService(SAPDataDbContext context)
    {
        _context = context;
    }

    public async Task<List<PrinterSetting>> GetAllAsync()
    {
        return await _context.PrinterSettings.ToListAsync();
    }

    public async Task SaveChangesAsync(List<PrinterSetting> printerSettings)
    {
        // Caller filters to only new or modified items
        foreach (var setting in printerSettings)
        {
            if (setting.Id == 0)
            {
                // New entry
                _context.PrinterSettings.Add(setting);
            }
            else
            {
                // Existing entry - update only if caller determined it was modified
                _context.PrinterSettings.Update(setting);
            }
        }
        
        await _context.SaveChangesAsync();
    }
}
