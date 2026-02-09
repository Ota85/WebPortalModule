using EsonicModule.Models;

namespace EsonicModule.Services;

public interface IZebraTemplateService
{
    Task<List<ZebraTemplate>> GetAllAsync();
    Task SaveChangesAsync(List<ZebraTemplate> zebraTemplates);
}
