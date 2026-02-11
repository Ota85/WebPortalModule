namespace EsonicModule.DTOs;

public class ZebraTemplateDto
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string? Description { get; set; }
    public string Template { get; set; } = string.Empty;
}
