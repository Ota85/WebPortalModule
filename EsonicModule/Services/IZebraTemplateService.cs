using EsonicModule.DTOs;

namespace EsonicModule.Services;

public interface IZebraTemplateService
{
    Task<List<ZebraTemplateDto>> GetAllAsync();
    Task SaveChangesAsync(List<ZebraTemplateDto> zebraTemplates);
}
