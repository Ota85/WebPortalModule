# EsonicModule - Database Configuration

## Database Connection

The EsonicModule is configured to connect to a MS SQL Server database with the following details:

- **Server**: 10.0.16.175\ESONIC
- **Database**: Zebra
- **Username**: sa
- **Password**: asdfasdf

## Configuration

The connection string is stored in the `appsettings.json` file (and environment-specific versions) under the `ConnectionStrings:ZebraDatabase` key.

```json
{
  "ConnectionStrings": {
    "ZebraDatabase": "Server=10.0.16.175\\ESONIC;Database=Zebra;User Id=sa;Password=asdfasdf;TrustServerCertificate=True;Encrypt=False;"
  }
}
```

## Entity Framework Setup

### Components Created

1. **ZebraDbContext** (`Data/ZebraDbContext.cs`): Entity Framework DbContext for the Zebra database
2. **Barcode Entity** (`Models/Barcode.cs`): Entity model for the Barcodes table
3. **BarcodeService** (`Services/BarcodeService.cs`): Service layer for working with Barcodes
4. **IBarcodeService** (`Services/IBarcodeService.cs`): Interface for the BarcodeService

### Services Registered

The following services are registered in `Program.cs`:

- `ZebraDbContext`: Scoped DbContext with SQL Server provider
- `IBarcodeService` / `BarcodeService`: Scoped service for barcode operations

## Using the Barcode Service

You can inject `IBarcodeService` into your components or controllers to work with barcodes:

```csharp
public class MyComponent
{
    private readonly IBarcodeService _barcodeService;

    public MyComponent(IBarcodeService barcodeService)
    {
        _barcodeService = barcodeService;
    }

    public async Task LoadBarcodesAsync()
    {
        var barcodes = await _barcodeService.GetAllBarcodesAsync();
        // Use the barcodes...
    }
}
```

## Scaffolding from Existing Database

If you need to re-scaffold the Barcodes table from the database (when you have access to it), run:

```bash
dotnet ef dbcontext scaffold "Name=ConnectionStrings:ZebraDatabase" Microsoft.EntityFrameworkCore.SqlServer --table Barcodes --context ZebraDbContext --context-dir Data --output-dir Models --force
```

Or with the full connection string:

```bash
dotnet ef dbcontext scaffold "Server=10.0.16.175\ESONIC;Database=Zebra;User Id=sa;Password=asdfasdf;TrustServerCertificate=True;Encrypt=False;" Microsoft.EntityFrameworkCore.SqlServer --table Barcodes --context ZebraDbContext --context-dir Data --output-dir Models --force
```

## NuGet Packages

The following Entity Framework packages are already installed:

- Microsoft.EntityFrameworkCore (9.0.12)
- Microsoft.EntityFrameworkCore.SqlServer (9.0.12)
- Microsoft.EntityFrameworkCore.Design (9.0.12)
- Microsoft.EntityFrameworkCore.Tools (9.0.12)

## Security Note

⚠️ **Important**: The connection string contains sensitive credentials. In production, consider:
- Using Azure Key Vault or similar secret management
- Using environment variables
- Using Windows Authentication instead of SQL Authentication
- Encrypting the connection string in configuration files
