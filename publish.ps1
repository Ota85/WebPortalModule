# PowerShell script to publish both applications for IIS deployment
# Run this script from the repository root

param(
    [string]$OutputPath = ".\publish",
    [string]$Configuration = "Release"
)

Write-Host "========================================" -ForegroundColor Cyan
Write-Host "WebPortalModule - IIS Publish Script" -ForegroundColor Cyan
Write-Host "========================================" -ForegroundColor Cyan
Write-Host ""

# Create output directory if it doesn't exist
$ApiOutput = Join-Path $OutputPath "EsonicApi"
$ModuleOutput = Join-Path $OutputPath "EsonicModule"

Write-Host "Output directories:" -ForegroundColor Yellow
Write-Host "  API:    $ApiOutput" -ForegroundColor Gray
Write-Host "  Module: $ModuleOutput" -ForegroundColor Gray
Write-Host ""

# Publish EsonicApi
Write-Host "Publishing EsonicApi..." -ForegroundColor Green
Set-Location -Path ".\EsonicApi"
dotnet publish -c $Configuration -o $ApiOutput --runtime win-x64 --self-contained false

if ($LASTEXITCODE -ne 0) {
    Write-Host "Error publishing EsonicApi!" -ForegroundColor Red
    Set-Location -Path ".."
    exit 1
}

Set-Location -Path ".."
Write-Host "EsonicApi published successfully!" -ForegroundColor Green
Write-Host ""

# Publish EsonicModule
Write-Host "Publishing EsonicModule..." -ForegroundColor Green
Set-Location -Path ".\EsonicModule"
dotnet publish -c $Configuration -o $ModuleOutput --runtime win-x64 --self-contained false

if ($LASTEXITCODE -ne 0) {
    Write-Host "Error publishing EsonicModule!" -ForegroundColor Red
    Set-Location -Path ".."
    exit 1
}

Set-Location -Path ".."
Write-Host "EsonicModule published successfully!" -ForegroundColor Green
Write-Host ""

# Summary
Write-Host "========================================" -ForegroundColor Cyan
Write-Host "Publish completed successfully!" -ForegroundColor Green
Write-Host "========================================" -ForegroundColor Cyan
Write-Host ""
Write-Host "Published files are located at:" -ForegroundColor Yellow
Write-Host "  API:    $(Resolve-Path $ApiOutput)" -ForegroundColor Gray
Write-Host "  Module: $(Resolve-Path $ModuleOutput)" -ForegroundColor Gray
Write-Host ""
Write-Host "Next steps:" -ForegroundColor Yellow
Write-Host "1. Update appsettings.Production.json files with your production URLs" -ForegroundColor Gray
Write-Host "2. Copy the published folders to your IIS server" -ForegroundColor Gray
Write-Host "3. Follow the instructions in IIS_DEPLOYMENT.md" -ForegroundColor Gray
Write-Host ""
