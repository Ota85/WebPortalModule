using Microsoft.EntityFrameworkCore;
using AutoMapper;
using EsonicModule.Data;
using EsonicModule.DTOs;
using EsonicModule.Models;

namespace EsonicModule.Services;

public class PrinterSettingService : IPrinterSettingService
{
    private readonly SAPDataDbContext _context;
    private readonly IMapper _mapper;

    public PrinterSettingService(SAPDataDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<List<PrinterSettingDto>> GetAllAsync()
    {
        var entities = await _context.PrinterSettings.ToListAsync();
        return _mapper.Map<List<PrinterSettingDto>>(entities);
    }

    public async Task SaveChangesAsync(List<PrinterSettingDto> printerSettings)
    {
        // Caller filters to only new or modified items
        foreach (var dto in printerSettings)
        {
            var entity = _mapper.Map<PrinterSetting>(dto);
            
            if (entity.Id == 0)
            {
                // New entry
                _context.PrinterSettings.Add(entity);
            }
            else
            {
                // Existing entry - update only if caller determined it was modified
                _context.PrinterSettings.Update(entity);
            }
        }
        
        await _context.SaveChangesAsync();
    }
}
