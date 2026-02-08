# Průvodce nasazením na IIS (Czech)

Tento dokument poskytuje stručný přehled pro nasazení aplikací EsonicModule a EsonicApi na IIS server.

## Příprava

### 1. Instalace požadavků na server
- Windows Server 2016 nebo novější
- IIS 10 nebo novější
- **.NET 9.0 Hosting Bundle** - Stáhnout z: https://dotnet.microsoft.com/download/dotnet/9.0

### 2. Restart IIS po instalaci Hosting Bundle
```powershell
net stop was /y
net start w3svc
```

## Publikování aplikací

### Použití PowerShell skriptu (doporučeno)
```powershell
.\publish.ps1
```

Tento skript vytvoří publikované verze obou aplikací ve složce `.\publish\`

### Ruční publikování

#### EsonicApi
```powershell
cd EsonicApi
dotnet publish -c Release -o C:\inetpub\wwwroot\EsonicApi
```

#### EsonicModule
```powershell
cd EsonicModule
dotnet publish -c Release -o C:\inetpub\wwwroot\EsonicModule
```

## Konfigurace pro produkci

### Před nasazením upravte:

#### EsonicApi/appsettings.Production.json
Nastavte povolené domény pro CORS:
```json
{
  "CorsSettings": {
    "AllowedOrigins": [
      "https://vase-domena.cz",
      "http://vase-domena.cz"
    ]
  }
}
```

#### EsonicModule/appsettings.Production.json
Nastavte URL API:
```json
{
  "ApiSettings": {
    "BaseUrl": "https://vase-domena.cz/api"
  }
}
```

## Vytvoření IIS sitů

### 1. Vytvořte Application Pools
V IIS Manageru vytvořte dva application pools:
- **EsonicApi**
  - .NET CLR version: `No Managed Code`
  - Managed pipeline mode: `Integrated`
  
- **EsonicModule**
  - .NET CLR version: `No Managed Code`
  - Managed pipeline mode: `Integrated`

### 2. Vytvořte Web Sites

#### EsonicApi
- Site name: `EsonicApi`
- Application pool: `EsonicApi`
- Physical path: `C:\inetpub\wwwroot\EsonicApi`
- Binding:
  - Type: `https`
  - Port: `443`
  - Host name: `api.vase-domena.cz`
  - SSL certificate: Vyberte váš certifikát

#### EsonicModule
- Site name: `EsonicModule`
- Application pool: `EsonicModule`
- Physical path: `C:\inetpub\wwwroot\EsonicModule`
- Binding:
  - Type: `https`
  - Port: `443`
  - Host name: `vase-domena.cz`
  - SSL certificate: Vyberte váš certifikát

## Nastavení oprávnění

```powershell
# Pro EsonicApi
icacls "C:\inetpub\wwwroot\EsonicApi" /grant "IIS AppPool\EsonicApi:(OI)(CI)F" /T

# Pro EsonicModule
icacls "C:\inetpub\wwwroot\EsonicModule" /grant "IIS AppPool\EsonicModule:(OI)(CI)F" /T
```

## Ověření

### Test API
Otevřete v prohlížeči:
```
https://api.vase-domena.cz/api/Data
```
Měla by se zobrazit JSON odpověď s daty.

### Test Blazor aplikace
Otevřete v prohlížeči:
```
https://vase-domena.cz
```
Přejděte na "Data Grid" a ověřte, že se načítají data z API.

## Řešení problémů

### Povolení detailních logů
1. Editujte `web.config` v publikované složce aplikace
2. Změňte `stdoutLogEnabled="false"` na `stdoutLogEnabled="true"`
3. Vytvořte složku `logs` v adresáři aplikace
4. Restartujte application pool
5. Zkontrolujte logy ve složce `logs`

### Běžné chyby

#### 502.5 - Process Failure
- Ověřte instalaci .NET Hosting Bundle
- Zkontrolujte správnou verzi .NET runtime

#### 500.30 - In-Process Startup Failure
- Zkontrolujte konfiguraci `web.config`
- Ověřte nastavení application pool

#### CORS chyby
- Ověřte `appsettings.Production.json` v EsonicApi
- Zkontrolujte povolené domény v konfiguraci CORS

#### Připojení k API selhává
- Ověřte, že API běží a je dostupné
- Zkontrolujte URL API v `appsettings.Production.json` v EsonicModule

## Detailní dokumentace

Pro podrobný návod v angličtině viz: **[IIS_DEPLOYMENT.md](IIS_DEPLOYMENT.md)**

## Důležité

- ✅ Vždy používejte HTTPS v produkci
- ✅ Pravidelně aktualizujte .NET runtime a závislosti
- ✅ Nastavte silné SSL/TLS certifikáty
- ✅ Monitorujte aplikační logy
- ✅ Zálohujte aplikaci a data

## Podpora

Pro problémy s nasazením:
1. Zkontrolujte Event Viewer (Windows Logs → Application)
2. Zkontrolujte logy IIS
3. Zkontrolujte aplikační logy ve složce `logs`
