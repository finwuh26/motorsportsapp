# Build and Run Scripts

## Windows

### Build
```batch
build.bat
```

### Run
```batch
run.bat
```

### Publish
```batch
publish.bat
```

## Configuration

The application uses the following configuration:
- **Database**: SQLite (`motorsports.db`) - automatically created on first run
- **Cache**: In-memory cache for API responses
- **APIs**: Multiple F1 data sources with automatic fallback
  - Ergast API (primary for historical data)
  - OpenF1 API (primary for live sessions)

## Environment Variables

No environment variables or API keys required - all APIs are free and open!

## Development

### Hot Reload
WPF supports hot reload for XAML changes:
```bash
dotnet watch run --project src/MotorsportsApp.Desktop
```

### Database Migrations
```bash
# Create migration
dotnet ef migrations add MigrationName --project src/MotorsportsApp.Data

# Apply migrations
dotnet ef database update --project src/MotorsportsApp.Data

# Remove last migration
dotnet ef migrations remove --project src/MotorsportsApp.Data
```

### Clearing Cache
Delete the `motorsports.db` file to reset the local cache and database.
