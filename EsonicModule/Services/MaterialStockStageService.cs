using Microsoft.EntityFrameworkCore;
using EsonicModule.Data;
using EsonicModule.Models;

namespace EsonicModule.Services;

public class MaterialStockStageService : IMaterialStockStageService
{
    private readonly SAPDataDbContext _context;

    public MaterialStockStageService(SAPDataDbContext context)
    {
        _context = context;
    }

    public async Task<List<MaterialStockStage>> GetAllAsync()
    {
        return await _context.MaterialStockStages.ToListAsync();
    }
}
