using Microsoft.EntityFrameworkCore;
using EsonicModule.Data;
using EsonicModule.Models;

namespace EsonicModule.Services;

public class BarcodeService : IBarcodeService
{
    private readonly ZebraDbContext _context;

    public BarcodeService(ZebraDbContext context)
    {
        _context = context;
    }

    public async Task<List<Barcode>> GetAllBarcodesAsync()
    {
        return await _context.Barcodes.ToListAsync();
    }

    public async Task<Barcode?> GetBarcodeByIdAsync(int id)
    {
        return await _context.Barcodes.FindAsync(id);
    }

    public async Task<Barcode> AddBarcodeAsync(Barcode barcode)
    {
        _context.Barcodes.Add(barcode);
        await _context.SaveChangesAsync();
        return barcode;
    }

    public async Task<bool> UpdateBarcodeAsync(Barcode barcode)
    {
        _context.Entry(barcode).State = EntityState.Modified;
        
        try
        {
            await _context.SaveChangesAsync();
            return true;
        }
        catch (DbUpdateConcurrencyException)
        {
            if (!await BarcodeExistsAsync(barcode.Id))
            {
                return false;
            }
            
            throw;
        }
    }

    public async Task<bool> DeleteBarcodeAsync(int id)
    {
        var barcode = await _context.Barcodes.FindAsync(id);
        if (barcode == null)
        {
            return false;
        }

        _context.Barcodes.Remove(barcode);
        await _context.SaveChangesAsync();
        return true;
    }

    private async Task<bool> BarcodeExistsAsync(int id)
    {
        return await _context.Barcodes.AnyAsync(e => e.Id == id);
    }
}
