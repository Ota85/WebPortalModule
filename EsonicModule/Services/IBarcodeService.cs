using EsonicModule.Models;

namespace EsonicModule.Services;

public interface IBarcodeService
{
    Task<List<Barcode>> GetAllBarcodesAsync();
    Task<Barcode?> GetBarcodeByIdAsync(int id);
    Task<Barcode> AddBarcodeAsync(Barcode barcode);
    Task<bool> UpdateBarcodeAsync(Barcode barcode);
    Task<bool> DeleteBarcodeAsync(int id);
}
