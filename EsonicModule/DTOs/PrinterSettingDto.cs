namespace EsonicModule.DTOs;

public class PrinterSettingDto
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string? Description { get; set; }
    public string IPAddress { get; set; } = string.Empty;
    public int Port { get; set; }
}
