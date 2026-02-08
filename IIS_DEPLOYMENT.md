# IIS Deployment Guide

This document provides step-by-step instructions for deploying EsonicModule (Blazor Server) and EsonicApi (Web API) to Internet Information Services (IIS).

## Prerequisites

### Server Requirements
- Windows Server 2016 or later (or Windows 10/11 for development)
- IIS 10 or later
- .NET 10.0 Runtime (or .NET 10.0 Hosting Bundle)

### Install .NET Hosting Bundle
1. Download the .NET 10.0 Hosting Bundle from: https://dotnet.microsoft.com/download/dotnet/10.0
2. Run the installer
3. Restart IIS after installation:
   ```powershell
   net stop was /y
   net start w3svc
   ```

### Enable IIS Features
Run the following PowerShell command as Administrator:
```powershell
Enable-WindowsOptionalFeature -Online -FeatureName IIS-WebServerRole
Enable-WindowsOptionalFeature -Online -FeatureName IIS-WebServer
Enable-WindowsOptionalFeature -Online -FeatureName IIS-CommonHttpFeatures
Enable-WindowsOptionalFeature -Online -FeatureName IIS-HttpErrors
Enable-WindowsOptionalFeature -Online -FeatureName IIS-ApplicationDevelopment
Enable-WindowsOptionalFeature -Online -FeatureName IIS-NetFxExtensibility45
Enable-WindowsOptionalFeature -Online -FeatureName IIS-HealthAndDiagnostics
Enable-WindowsOptionalFeature -Online -FeatureName IIS-HttpLogging
Enable-WindowsOptionalFeature -Online -FeatureName IIS-Security
Enable-WindowsOptionalFeature -Online -FeatureName IIS-RequestFiltering
Enable-WindowsOptionalFeature -Online -FeatureName IIS-Performance
Enable-WindowsOptionalFeature -Online -FeatureName IIS-WebServerManagementTools
Enable-WindowsOptionalFeature -Online -FeatureName IIS-ManagementConsole
Enable-WindowsOptionalFeature -Online -FeatureName IIS-StaticContent
Enable-WindowsOptionalFeature -Online -FeatureName IIS-DefaultDocument
Enable-WindowsOptionalFeature -Online -FeatureName IIS-DirectoryBrowsing
Enable-WindowsOptionalFeature -Online -FeatureName IIS-HttpCompressionStatic
Enable-WindowsOptionalFeature -Online -FeatureName IIS-ASPNET45
```

## Building and Publishing

### 1. Publish EsonicApi

From the repository root, run:

```powershell
cd EsonicApi
dotnet publish -c Release -o C:\inetpub\wwwroot\EsonicApi
```

Or use the publish profile:
```powershell
dotnet publish -c Release /p:PublishProfile=FolderProfile
```

### 2. Publish EsonicModule

From the repository root, run:

```powershell
cd EsonicModule
dotnet publish -c Release -o C:\inetpub\wwwroot\EsonicModule
```

Or use the publish profile:
```powershell
dotnet publish -c Release /p:PublishProfile=FolderProfile
```

## Configuration

### 1. Configure Production Settings

Before deployment, update the production configuration files:

#### EsonicApi/appsettings.Production.json
Update the allowed CORS origins to match your production domain:
```json
{
  "CorsSettings": {
    "AllowedOrigins": [
      "https://yourdomain.com",
      "http://yourdomain.com"
    ]
  }
}
```

#### EsonicModule/appsettings.Production.json
Update the API base URL to match your API endpoint:
```json
{
  "ApiSettings": {
    "BaseUrl": "https://yourdomain.com/api"
  }
}
```

### 2. Create Application Pools in IIS

1. Open IIS Manager
2. Right-click on "Application Pools" → "Add Application Pool"

**For EsonicApi:**
- Name: `EsonicApi`
- .NET CLR version: `No Managed Code`
- Managed pipeline mode: `Integrated`
- Click OK

**For EsonicModule:**
- Name: `EsonicModule`
- .NET CLR version: `No Managed Code`
- Managed pipeline mode: `Integrated`
- Click OK

### 3. Configure Application Pool Settings

For both application pools:
1. Right-click the application pool → "Advanced Settings"
2. Set `Start Mode` to `AlwaysRunning`
3. Set `Idle Time-out (minutes)` to `0` (for production scenarios)
4. Click OK

### 4. Create IIS Sites

#### Create EsonicApi Site

1. Right-click "Sites" → "Add Website"
2. Configure:
   - Site name: `EsonicApi`
   - Application pool: Select `EsonicApi`
   - Physical path: `C:\inetpub\wwwroot\EsonicApi`
   - Binding:
     - Type: `https`
     - Port: `443`
     - Host name: `api.yourdomain.com` (or subdomain)
     - SSL certificate: Select your certificate
3. Click OK

#### Create EsonicModule Site

1. Right-click "Sites" → "Add Website"
2. Configure:
   - Site name: `EsonicModule`
   - Application pool: Select `EsonicModule`
   - Physical path: `C:\inetpub\wwwroot\EsonicModule`
   - Binding:
     - Type: `https`
     - Port: `443`
     - Host name: `yourdomain.com` (or your main domain)
     - SSL certificate: Select your certificate
3. Click OK

## SSL Certificate Configuration

### Option 1: Using a Commercial Certificate
1. Purchase an SSL certificate from a trusted Certificate Authority
2. Install the certificate in Windows Certificate Store
3. Bind the certificate to your IIS sites as shown above

### Option 2: Using Let's Encrypt (Free)
1. Install win-acme: https://www.win-acme.com/
2. Run win-acme and follow the wizard to create certificates
3. Certificates will be automatically installed and bound to IIS

## Permissions

Set proper permissions for the application folders:

```powershell
# For EsonicApi
icacls "C:\inetpub\wwwroot\EsonicApi" /grant "IIS AppPool\EsonicApi:(OI)(CI)F" /T

# For EsonicModule
icacls "C:\inetpub\wwwroot\EsonicModule" /grant "IIS AppPool\EsonicModule:(OI)(CI)F" /T
```

## Firewall Configuration

Ensure Windows Firewall allows HTTP and HTTPS traffic:

```powershell
netsh advfirewall firewall add rule name="HTTP" dir=in action=allow protocol=TCP localport=80
netsh advfirewall firewall add rule name="HTTPS" dir=in action=allow protocol=TCP localport=443
```

## Verification

### 1. Test API
Open browser and navigate to:
- `https://api.yourdomain.com/api/Data`
- You should see JSON response with data items

### 2. Test Blazor Application
Open browser and navigate to:
- `https://yourdomain.com`
- Navigate to the "Data Grid" page
- Verify that data is loaded from the API

## Troubleshooting

### Enable Detailed Errors

If you encounter errors, enable detailed logging:

1. Edit `web.config` in the deployed application folder
2. Change `stdoutLogEnabled="false"` to `stdoutLogEnabled="true"`
3. Create a `logs` folder in the application directory
4. Restart the application pool
5. Check the log files in the `logs` folder

### Common Issues

#### 502.5 Error - Process Failure
- Verify .NET Hosting Bundle is installed
- Check that the correct .NET runtime version is installed
- Review logs in the `logs` folder

#### 500.30 Error - In-Process Startup Failure
- Check `web.config` configuration
- Verify application pool settings
- Review Event Viewer logs

#### CORS Errors
- Verify `appsettings.Production.json` has correct allowed origins
- Ensure CORS policy is applied before other middleware
- Check browser developer console for specific CORS error messages

#### Connection to API Fails
- Verify API is running and accessible
- Check `appsettings.Production.json` in EsonicModule has correct API URL
- Test API endpoint directly in browser

## Monitoring

### Application Insights (Optional)
For production monitoring, consider adding Application Insights:

1. Add NuGet package:
   ```powershell
   dotnet add package Microsoft.ApplicationInsights.AspNetCore
   ```

2. Configure in `appsettings.Production.json`:
   ```json
   {
     "ApplicationInsights": {
       "InstrumentationKey": "your-key-here"
     }
   }
   ```

## Maintenance

### Updating the Application

1. Stop the IIS site/application pool
2. Back up the current deployment
3. Publish the new version to the deployment folder
4. Start the IIS site/application pool

### Updating via PowerShell Script

```powershell
# Stop application pools
Stop-WebAppPool -Name "EsonicApi"
Stop-WebAppPool -Name "EsonicModule"

# Deploy new versions
# ... copy files ...

# Start application pools
Start-WebAppPool -Name "EsonicApi"
Start-WebAppPool -Name "EsonicModule"
```

## Security Considerations

1. **Always use HTTPS in production**
2. **Keep .NET runtime updated** with security patches
3. **Regularly update dependencies** and packages
4. **Use strong SSL/TLS certificates**
5. **Implement proper authentication** if needed
6. **Set up regular backups** of application and data
7. **Monitor application logs** for suspicious activity
8. **Use environment variables** for sensitive configuration (connection strings, API keys)

## Additional Resources

- [Host ASP.NET Core on Windows with IIS](https://docs.microsoft.com/en-us/aspnet/core/host-and-deploy/iis/)
- [ASP.NET Core Module](https://docs.microsoft.com/en-us/aspnet/core/host-and-deploy/aspnet-core-module)
- [.NET Downloads](https://dotnet.microsoft.com/download)
