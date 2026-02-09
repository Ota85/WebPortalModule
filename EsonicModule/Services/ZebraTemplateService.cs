using Microsoft.EntityFrameworkCore;
using EsonicModule.Data;
using EsonicModule.Models;

namespace EsonicModule.Services;

public class ZebraTemplateService : IZebraTemplateService
{
    private readonly SAPDataDbContext _context;

    public ZebraTemplateService(SAPDataDbContext context)
    {
        _context = context;
    }

    public async Task<List<ZebraTemplate>> GetAllAsync()
    {
        return await _context.ZebraTemplates.ToListAsync();
    }

    public async Task SaveChangesAsync(List<ZebraTemplate> zebraTemplates)
    {
        // Caller filters to only new or modified items
        foreach (var template in zebraTemplates)
        {
            if (template.Id == 0)
            {
                // New entry
                _context.ZebraTemplates.Add(template);
            }
            else
            {
                // Existing entry - update only if caller determined it was modified
                _context.ZebraTemplates.Update(template);
            }
        }
        
        await _context.SaveChangesAsync();
    }
}
