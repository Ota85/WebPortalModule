# Localization Implementation

This document describes the localization setup for the EsonicModule application.

## Overview

The application now supports multiple languages using ASP.NET Core's built-in localization features with .resx resource files. Currently supported languages are:

- **Czech (cs-CZ)** - Default language
- **English (en)** - Fallback language

## Implementation Details

### Resource Files

Resource files are located in `/EsonicModule/Resources/`:

- `SharedResources.resx` - English translations (original strings)
- `SharedResources.cs-CZ.resx` - Czech translations
- `SharedResources.cs` - Empty marker class for locating resources

### Configuration

Localization is configured in `Program.cs`:

```csharp
// Add localization services
builder.Services.AddLocalization(options => options.ResourcesPath = "Resources");

// Configure supported cultures - Czech as default
var supportedCultures = new[]
{
    new CultureInfo("cs-CZ"), // Czech
    new CultureInfo("en")     // English
};

app.UseRequestLocalization(new RequestLocalizationOptions
{
    DefaultRequestCulture = new RequestCulture("cs-CZ"), // Czech as default
    SupportedCultures = supportedCultures,
    SupportedUICultures = supportedCultures
});
```

### Usage in Razor Components

To use localization in Razor components:

1. Add using directives:
```csharp
@using Microsoft.Extensions.Localization
@using EsonicModule.Resources
```

2. Inject the localizer:
```csharp
@inject IStringLocalizer<SharedResources> Localizer
```

3. Use localized strings:
```razor
<h1>@Localizer["PageTitle"]</h1>
<p>@Localizer["Description"]</p>
```

4. For formatted strings:
```csharp
errorMessage = string.Format(Localizer["ErrorWithParameter"].Value, parameter);
```

## Localized Components

The following components have been localized:

- `NavMenu.razor` - Navigation menu
- `PrinterSettings.razor` - Printer configuration page
- `BarcodeTemplates.razor` - Barcode template management page
- `DataForPrint.razor` - Material stock data page

## Adding New Translations

To add new translations:

1. Add the key-value pair to `SharedResources.resx` (English)
2. Add the corresponding translation to `SharedResources.cs-CZ.resx` (Czech)
3. Use the key in your Razor component: `@Localizer["YourNewKey"]`
4. Build the project to compile the resources

## Adding New Languages

To add support for additional languages:

1. Create a new resource file: `SharedResources.[culture-code].resx` (e.g., `SharedResources.de-DE.resx` for German)
2. Copy all keys from `SharedResources.resx` and translate the values
3. Update `Program.cs` to include the new culture in `supportedCultures`
4. Build the project

## Default Language

Czech (cs-CZ) is set as the default language. The application will use Czech translations by default for all users. Users can potentially override this by setting the Accept-Language header in their browser, though no UI is currently provided for language switching.

## Technical Notes

- Resource files are compiled into satellite assemblies during build
- The Czech resource assembly is located at: `bin/Debug/net9.0/cs-CZ/EsonicModule.resources.dll`
- All UI strings should be moved to resource files - avoid hardcoding strings in Razor components
- Use `.Value` property when passing localized strings to JavaScript or formatting with `string.Format()`
