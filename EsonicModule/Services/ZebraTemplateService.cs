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
        foreach (var dto in zebraTemplates)
        {
            var entity = _mapper.Map<ZebraTemplate>(dto);
            
            if (entity.Id == 0)
            {
                // New entry
                _context.ZebraTemplates.Add(entity);
            }
            else
            {
                // Existing entry - update only if caller determined it was modified
                _context.ZebraTemplates.Update(entity);
            }
        }
        
        await _context.SaveChangesAsync();
    }
}
