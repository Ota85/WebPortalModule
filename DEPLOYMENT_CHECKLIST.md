# IIS Deployment Checklist / Kontrolní seznam nasazení IIS

Use this checklist to ensure all steps are completed for a successful IIS deployment.
*Použijte tento seznam k ověření, že byly dokončeny všechny kroky pro úspěšné nasazení na IIS.*

## Pre-Deployment / Před nasazením

- [ ] .NET 9.0 Hosting Bundle installed on IIS server
      *.NET 9.0 Hosting Bundle nainstalován na IIS serveru*
- [ ] IIS features enabled (see IIS_DEPLOYMENT.md)
      *IIS funkce povoleny (viz IIS_DEPLOYMENT.md)*
- [ ] SSL certificate obtained and ready
      *SSL certifikát získán a připraven*
- [ ] Production domain names configured in DNS
      *Produkční doménová jména nakonfigurována v DNS*

## Configuration / Konfigurace

- [ ] `EsonicApi/appsettings.Production.json` updated with production CORS origins
      *EsonicApi/appsettings.Production.json aktualizován s produkčními CORS původy*
- [ ] `EsonicModule/appsettings.Production.json` updated with production API URL
      *EsonicModule/appsettings.Production.json aktualizován s produkční API URL*
- [ ] Both web.config files reviewed and customized if needed
      *Oba web.config soubory zkontrolovány a přizpůsobeny dle potřeby*

## Build and Publish / Sestavení a publikování

- [ ] Solution builds successfully in Release mode
      *Řešení se úspěšně sestaví v Release režimu*
      ```bash
      dotnet build -c Release
      ```
- [ ] EsonicApi published
      *EsonicApi publikován*
      ```bash
      cd EsonicApi
      dotnet publish -c Release -o C:\inetpub\wwwroot\EsonicApi
      ```
- [ ] EsonicModule published
      *EsonicModule publikován*
      ```bash
      cd EsonicModule
      dotnet publish -c Release -o C:\inetpub\wwwroot\EsonicModule
      ```
- [ ] Published files verified (web.config, appsettings.Production.json present)
      *Publikované soubory ověřeny (web.config, appsettings.Production.json přítomny)*

## IIS Configuration / Konfigurace IIS

- [ ] Application Pool created for EsonicApi
      *Application Pool vytvořen pro EsonicApi*
      - Name: EsonicApi
      - .NET CLR: No Managed Code
      - Pipeline: Integrated
- [ ] Application Pool created for EsonicModule
      *Application Pool vytvořen pro EsonicModule*
      - Name: EsonicModule
      - .NET CLR: No Managed Code
      - Pipeline: Integrated
- [ ] EsonicApi website created in IIS
      *EsonicApi web vytvořen v IIS*
      - Binding: https://{api-domain}
      - SSL certificate bound
- [ ] EsonicModule website created in IIS
      *EsonicModule web vytvořen v IIS*
      - Binding: https://{main-domain}
      - SSL certificate bound
- [ ] File permissions set correctly
      *Oprávnění souborů správně nastavena*
      ```powershell
      icacls "C:\inetpub\wwwroot\EsonicApi" /grant "IIS AppPool\EsonicApi:(OI)(CI)F" /T
      icacls "C:\inetpub\wwwroot\EsonicModule" /grant "IIS AppPool\EsonicModule:(OI)(CI)F" /T
      ```
- [ ] Application Pools started
      *Application Pooly spuštěny*

## Firewall / Firewall

- [ ] Port 80 (HTTP) open
      *Port 80 (HTTP) otevřen*
- [ ] Port 443 (HTTPS) open
      *Port 443 (HTTPS) otevřen*

## Testing / Testování

- [ ] API accessible via browser
      *API dostupné přes prohlížeč*
      - Test URL: `https://{api-domain}/api/Data`
      - Expected: JSON response with data items
- [ ] Blazor application loads
      *Blazor aplikace se načte*
      - Test URL: `https://{main-domain}`
      - Expected: Home page displays
- [ ] Data Grid page works
      *Stránka Data Grid funguje*
      - Navigate to: `https://{main-domain}/datagrid`
      - Expected: Grid displays data from API
- [ ] CORS working correctly
      *CORS funguje správně*
      - No CORS errors in browser console
- [ ] HTTPS redirect working
      *HTTPS přesměrování funguje*
      - HTTP requests redirect to HTTPS
- [ ] Static files loading (CSS, JS)
      *Statické soubory se načítají (CSS, JS)*
      - Check browser developer tools

## Monitoring and Logs / Monitorování a logy

- [ ] Application logs accessible
      *Aplikační logy dostupné*
      - Logs folder exists in each application directory
- [ ] IIS logs reviewed
      *IIS logy zkontrolovány*
      - Location: `C:\inetpub\logs\LogFiles`
- [ ] Event Viewer checked
      *Event Viewer zkontrolován*
      - Windows Logs → Application
- [ ] Performance baseline established
      *Výkonnostní baseline stanoven*

## Security / Bezpečnost

- [ ] SSL/TLS certificate valid and not expiring soon
      *SSL/TLS certifikát platný a brzy nevyprší*
- [ ] HTTPS enforced (HTTP redirects to HTTPS)
      *HTTPS vynucen (HTTP přesměrovává na HTTPS)*
- [ ] Sensitive information not in configuration files
      *Citlivé informace nejsou v konfiguračních souborech*
- [ ] Application Pools running under appropriate identity
      *Application Pooly běží pod vhodnou identitou*
- [ ] Request size limits configured appropriately
      *Limity velikosti požadavků správně nakonfigurovány*

## Documentation / Dokumentace

- [ ] Deployment documented for future reference
      *Nasazení zdokumentováno pro budoucí referenci*
- [ ] Configuration values documented (domains, ports, etc.)
      *Konfigurační hodnoty zdokumentovány (domény, porty, atd.)*
- [ ] Administrator contacts documented
      *Kontakty na správce zdokumentovány*

## Post-Deployment / Po nasazení

- [ ] Stakeholders notified of deployment
      *Zainteresované strany informovány o nasazení*
- [ ] Monitoring in place
      *Monitorování zavedeno*
- [ ] Backup strategy confirmed
      *Strategie zálohování potvrzena*
- [ ] Update procedure documented
      *Postup aktualizace zdokumentován*

## Notes / Poznámky

```
Date deployed / Datum nasazení: _______________
Deployed by / Nasadil: _______________
Production URLs / Produkční URL:
  - Main app / Hlavní aplikace: _______________
  - API: _______________
Issues encountered / Zjištěné problémy: 
_______________________________________________
_______________________________________________
```
