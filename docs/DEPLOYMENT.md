# Deployment Guide

## Prerequisites

- .NET 9.0 Runtime
- Web server (IIS, Nginx, Apache)
- Database (SQLite, SQL Server, PostgreSQL)

## Production Configuration

### 1. Update appsettings.json

```json
{
  "ConnectionStrings": {
    "DefaultConnection": "YOUR_PRODUCTION_CONNECTION_STRING"
  },
  "Logging": {
    "LogLevel": {
      "Default": "Warning"
    }
  }
}
```

### 2. Build for Production

```bash
dotnet publish -c Release -o ./publish --configfile nuget.config
```

### 3. Database Migration

```bash
dotnet ef database update --configuration Release
```

## Deployment Options

### Option 1: IIS (Windows)

1. Install .NET 9.0 Hosting Bundle
2. Create IIS site pointing to publish folder
3. Set application pool to "No Managed Code"
4. Configure permissions for database file (if using SQLite)

### Option 2: Linux (Nginx + Systemd)

1. **Copy files to server**
   ```bash
   scp -r ./publish/* user@server:/var/www/myportfolio
   ```

2. **Create systemd service**
   ```bash
   sudo nano /etc/systemd/system/myportfolio.service
   ```

   ```ini
   [Unit]
   Description=MyPortfolio ASP.NET Core App

   [Service]
   WorkingDirectory=/var/www/myportfolio
   ExecStart=/usr/bin/dotnet /var/www/myportfolio/MyPortfolio.dll
   Restart=always
   RestartSec=10
   User=www-data
   Environment=ASPNETCORE_ENVIRONMENT=Production

   [Install]
   WantedBy=multi-user.target
   ```

3. **Configure Nginx**
   ```nginx
   server {
       listen 80;
       server_name yourdomain.com;

       location / {
           proxy_pass http://localhost:5000;
           proxy_http_version 1.1;
           proxy_set_header Upgrade $http_upgrade;
           proxy_set_header Connection keep-alive;
           proxy_set_header Host $host;
           proxy_cache_bypass $http_upgrade;
       }
   }
   ```

4. **Start services**
   ```bash
   sudo systemctl enable myportfolio
   sudo systemctl start myportfolio
   sudo systemctl restart nginx
   ```

### Option 3: Docker

Create `Dockerfile`:
```dockerfile
FROM mcr.microsoft.com/dotnet/aspnet:9.0
WORKDIR /app
COPY ./publish .
ENTRYPOINT ["dotnet", "MyPortfolio.dll"]
```

Build and run:
```bash
docker build -t myportfolio .
docker run -d -p 5000:8080 myportfolio
```

## Security Checklist

- [ ] Change default admin password
- [ ] Use HTTPS in production
- [ ] Set secure connection strings
- [ ] Enable error logging
- [ ] Configure CORS if needed
- [ ] Set up regular backups
- [ ] Use strong authentication keys
- [ ] Disable debug mode

## Environment Variables

```bash
ASPNETCORE_ENVIRONMENT=Production
ASPNETCORE_URLS=http://localhost:5000
```

## Database Options

### SQLite (Default)
- Simple, file-based
- Good for small sites
- Connection: `Data Source=MyPortfolio.db`

### SQL Server
```json
"DefaultConnection": "Server=SERVER;Database=MyPortfolio;User Id=USER;Password=PASS;"
```

Update `Program.cs`:
```csharp
options.UseSqlServer(connectionString)
```

### PostgreSQL
```json
"DefaultConnection": "Host=localhost;Database=myportfolio;Username=user;Password=pass"
```

Update `Program.cs`:
```csharp
options.UseNpgsql(connectionString)
```

## Monitoring

- Check logs in `/var/log/` or IIS logs
- Monitor application performance
- Set up health checks
- Configure email notifications for errors
