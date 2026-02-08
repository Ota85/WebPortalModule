@echo off
REM Batch script to publish both applications for IIS deployment
REM Run this script from the repository root

echo ========================================
echo WebPortalModule - IIS Publish Script
echo ========================================
echo.

set OUTPUT_PATH=.\publish
set CONFIGURATION=Release

REM Create output directories
set API_OUTPUT=%OUTPUT_PATH%\EsonicApi
set MODULE_OUTPUT=%OUTPUT_PATH%\EsonicModule

echo Output directories:
echo   API:    %API_OUTPUT%
echo   Module: %MODULE_OUTPUT%
echo.

REM Publish EsonicApi
echo Publishing EsonicApi...
cd EsonicApi
dotnet publish -c %CONFIGURATION% -o ..\%API_OUTPUT% --runtime win-x64 --self-contained false

if errorlevel 1 (
    echo Error publishing EsonicApi!
    cd ..
    exit /b 1
)

cd ..
echo EsonicApi published successfully!
echo.

REM Publish EsonicModule
echo Publishing EsonicModule...
cd EsonicModule
dotnet publish -c %CONFIGURATION% -o ..\%MODULE_OUTPUT% --runtime win-x64 --self-contained false

if errorlevel 1 (
    echo Error publishing EsonicModule!
    cd ..
    exit /b 1
)

cd ..
echo EsonicModule published successfully!
echo.

REM Summary
echo ========================================
echo Publish completed successfully!
echo ========================================
echo.
echo Published files are located at:
echo   API:    %CD%\%API_OUTPUT%
echo   Module: %CD%\%MODULE_OUTPUT%
echo.
echo Next steps:
echo 1. Update appsettings.Production.json files with your production URLs
echo 2. Copy the published folders to your IIS server
echo 3. Follow the instructions in IIS_DEPLOYMENT.md or NASAZENI_IIS.md
echo.
pause
