# Localization Removed

This document describes the previous localization setup that has been removed from the EsonicModule application.

## Change Summary

Localization has been removed from the application. All UI text is now hardcoded in Czech (cs-CZ) directly in the Razor components.

## Changes Made

1. **Removed localization configuration** from `Program.cs`:
   - Removed `AddLocalization` service registration
   - Removed `UseRequestLocalization` middleware
   - Removed culture configuration

2. **Updated Razor components** to use hardcoded Czech text:
   - `NavMenu.razor` - Navigation menu
   - `PrinterSettings.razor` - Printer configuration page
   - `BarcodeTemplates.razor` - Barcode template management page
   - `DataForPrint.razor` - Material stock data page

3. **Removed resource files**:
   - Deleted `/EsonicModule/Resources/` directory containing:
     - `SharedResources.resx` (English translations)
     - `SharedResources.cs-CZ.resx` (Czech translations)
     - `SharedResources.cs` (Marker class)

4. **Removed dependencies**:
   - Removed `Microsoft.Extensions.Localization` references from components
   - Removed `EsonicModule.Resources` using statements
   - Removed `IStringLocalizer<SharedResources>` injections

## Previous Implementation

The application previously supported multiple languages using ASP.NET Core's built-in localization features with .resx resource files. The supported languages were:
- **Czech (cs-CZ)** - Default language
- **English (en)** - Fallback language

## Current Implementation

All UI text is now directly embedded in Czech in the Razor components. The application no longer supports multiple languages or runtime language switching.

## Reverting This Change

If localization support needs to be restored in the future:
1. Restore the resource files from version control history
2. Re-add localization configuration to `Program.cs`
3. Update Razor components to use `IStringLocalizer<SharedResources>`
4. Add back the using statements for localization namespaces

