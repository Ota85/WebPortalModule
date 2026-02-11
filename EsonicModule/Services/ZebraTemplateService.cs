using Microsoft.EntityFrameworkCore;
using AutoMapper;
using EsonicModule.Data;
using EsonicModule.DTOs;
using EsonicModule.Models;

namespace EsonicModule.Services;

public class ZebraTemplateService : IZebraTemplateService
{
    private readonly SAPDataDbContext _context;
    private readonly IMapper _mapper;

    public ZebraTemplateService(SAPDataDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<List<ZebraTemplateDto>> GetAllAsync()
    {
        var entities = await _context.ZebraTemplates.ToListAsync();
        return _mapper.Map<List<ZebraTemplateDto>>(entities);
    }

    public async Task SaveChangesAsync(List<ZebraTemplateDto> zebraTemplates)
    {
        // Caller filters to only new or modified items
        var updateIds = zebraTemplates.Where(dto => dto.Id > 0).Select(dto => dto.Id).ToList();
        var existingEntities = await _context.ZebraTemplates
            .Where(e => updateIds.Contains(e.Id))
            .ToDictionaryAsync(e => e.Id);

        foreach (var dto in zebraTemplates)
        {
            if (dto.Id == 0)
            {
                // New entry
                var entity = _mapper.Map<ZebraTemplate>(dto);
                _context.ZebraTemplates.Add(entity);
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
