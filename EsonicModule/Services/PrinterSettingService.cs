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
        var updateIds = printerSettings.Where(dto => dto.Id > 0).Select(dto => dto.Id).ToList();
        var existingEntities = await _context.PrinterSettings
            .Where(e => updateIds.Contains(e.Id))
            .ToDictionaryAsync(e => e.Id);

        foreach (var dto in printerSettings)
        {
            if (dto.Id == 0)
            {
                // New entry
                var entity = _mapper.Map<PrinterSetting>(dto);
                _context.PrinterSettings.Add(entity);
            }
            else if (existingEntities.TryGetValue(dto.Id, out var existingEntity))
            {
                // Existing entry - update properties
                _mapper.Map(dto, existingEntity);
            }
        }
        
        await _context.SaveChangesAsync();
    }
}
