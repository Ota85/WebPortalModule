# WebPortalModule

This solution contains a Blazor Server web application and a Web API.

## Projects

### EsonicModule
A Blazor Server web application that displays data in a grid format by calling the EsonicApi.

**Features:**
- Server-side Blazor rendering
- Data grid component displaying items from API
- Bootstrap-based UI

**Running the application:**
```bash
cd EsonicModule
dotnet run
```

The application will be available at:
- HTTPS: https://localhost:7068
- HTTP: http://localhost:5148

**Pages:**
- `/` - Home page
- `/counter` - Counter demo
- `/weather` - Weather forecast demo
- `/datagrid` - Data grid showing items from EsonicApi

### EsonicApi
A REST API built with ASP.NET Core that provides data endpoints.

**Running the API:**
```bash
cd EsonicApi
dotnet run
```

The API will be available at:
- HTTPS: https://localhost:7242
- HTTP: http://localhost:5107

**Endpoints:**
- `GET /api/Data` - Returns a list of data items
- `GET /api/Data/{id}` - Returns a specific data item by ID
- `GET /weatherforecast` - Returns weather forecast data (demo endpoint)

## Running the Solution

1. Start the API first:
```bash
cd EsonicApi
dotnet run
```

2. In a new terminal, start the Blazor app:
```bash
cd EsonicModule
dotnet run
```

3. Open a browser and navigate to https://localhost:7068/datagrid to see the data grid in action.

## Building the Solution

```bash
dotnet build
```

## Project Structure

```
WebPortalModule/
├── EsonicApi/
│   ├── Controllers/
│   │   └── DataController.cs       # API controller with dummy data
│   ├── Models/
│   │   └── DataItem.cs            # Data model
│   └── Program.cs                  # API configuration
├── EsonicModule/
│   ├── Components/
│   │   ├── Pages/
│   │   │   └── DataGrid.razor     # Data grid component
│   │   └── Layout/
│   │       └── NavMenu.razor      # Navigation menu
│   ├── Models/
│   │   └── DataItem.cs            # Data model
│   ├── Services/
│   │   └── DataService.cs         # Service to call API
│   └── Program.cs                  # Blazor app configuration
└── WebPortalModule.sln             # Solution file
```

## Technology Stack

- .NET 10.0
- ASP.NET Core Blazor Server
- ASP.NET Core Web API
- Bootstrap 5
- C# 13

## Notes

- This is a basic implementation with dummy data
- CORS is configured to allow the Blazor app to call the API
- The API uses in-memory data storage
- Will be expanded with additional features later
