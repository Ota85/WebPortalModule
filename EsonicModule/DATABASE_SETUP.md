# EsonicModule - Database Configuration

> **⚠️ SECURITY WARNING**: This setup contains hardcoded credentials as specified in the requirements. These credentials are visible in source control. This is NOT recommended for production use. See the Security Note section below for recommended practices.

## Database Connection

The EsonicModule is configured to connect to a MS SQL Server database with the following details:

- **Server**: 10.0.21.60\PLANTIT
- **Database**: SAPData
- **Username**: eso_app
- **Password**: asdfasdf

## Configuration

The connection string is stored in the `appsettings.json` file (and environment-specific versions) under the `ConnectionStrings:SAPDataDatabase` key.

```json
{
  "ConnectionStrings": {
    "SAPDataDatabase": "Server=10.0.21.60\\PLANTIT;Database=SAPData;User Id=eso_app;Password=asdfasdf;TrustServerCertificate=True;Encrypt=False;"
  }
}
```

## Entity Framework Setup

### Components Created

1. **SAPDataDbContext** (`Data/SAPDataDbContext.cs`): Entity Framework DbContext for the SAP Data database
2. **MaterialStockStage Entity** (`Models/MaterialStockStage.cs`): Entity model for material stock data
3. **MaterialStockStageService** (`Services/MaterialStockStageService.cs`): Service layer for working with material stock data
4. **IMaterialStockStageService** (`Services/IMaterialStockStageService.cs`): Interface for the MaterialStockStageService

### Services Registered

The following services are registered in `Program.cs`:

- `SAPDataDbContext`: Scoped DbContext with SQL Server provider
- `IMaterialStockStageService` / `MaterialStockStageService`: Scoped service for material stock operations

## Using the Material Stock Service

You can inject `IMaterialStockStageService` into your components or controllers to work with material stock data:

```csharp
public class MyComponent
{
    private readonly IMaterialStockStageService _materialStockStageService;

    public MyComponent(IMaterialStockStageService materialStockStageService)
    {
        _materialStockStageService = materialStockStageService;
    }

    public async Task LoadDataAsync()
    {
        var data = await _materialStockStageService.GetAllAsync();
        // Use the data...
    }
}
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
