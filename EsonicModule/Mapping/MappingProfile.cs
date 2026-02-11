using AutoMapper;
using EsonicModule.DTOs;
using EsonicModule.Models;

namespace EsonicModule.Mapping;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        CreateMap<MaterialStockStage, MaterialStockStageDto>().ReverseMap();
        CreateMap<PrinterSetting, PrinterSettingDto>().ReverseMap();
        CreateMap<ZebraTemplate, ZebraTemplateDto>().ReverseMap();
    }
}
