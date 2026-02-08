# F1 Live Timing & Analytics

A production-ready Windows desktop application for Formula 1 live timing, analytics, and historical data visualization.

## Overview

This application provides real-time F1 live timing, telemetry visualization, deep session analysis, and replay capabilities. Built with .NET 8 and WPF, it offers a modern, high-performance user interface inspired by professional race engineering tools while remaining fully community-friendly and open-source.

## Features

### Current Features (Phase 1)
- ✅ **Multi-layer Architecture**: Clean separation with Models, Data, Services, Core, and Desktop layers
- ✅ **MVVM Pattern**: Using CommunityToolkit.Mvvm for reactive UI
- ✅ **Dependency Injection**: Microsoft.Extensions.DependencyInjection for IoC
- ✅ **Database Support**: SQLite with Entity Framework Core for local caching
- ✅ **API Integration**: Ergast F1 API client with graceful error handling
- ✅ **Dark Theme UI**: Motorsport-inspired dark interface
- ✅ **Upcoming Races**: Display current season calendar
- ✅ **Driver Information**: Current season driver roster

### Planned Features

#### Live Timing (Phase 4)
- Real-time leaderboard with tyre compounds, gaps, intervals
- Sector times and mini-sector performance
- Live track map with car positions and speed traces
- DRS and pit status indicators
- Session control awareness (flags, safety car, VSC)

#### Session Replay (Phase 5)
- Record full sessions locally (timing, telemetry, events)
- Scrubbable timeline with pause, rewind, fast-forward
- Side-by-side driver/lap comparisons

#### Analysis & Stats (Phase 6)
- Lap-by-lap tyre degradation graphs
- Stint analysis and pit strategy visualization
- Historical comparisons across seasons
- Qualifying pace deltas and race pace averages

#### User Experience (Phase 7)
- Keyboard shortcuts and controller support
- Multiple layout modes (Race Day, Analysis, Replay, Minimal)
- Fully resizable and dockable panels
- Optional streamer mode
- Auto-update system
- Theming support

#### Extensibility (Phase 8)
- Plugin system for additional motorsport series
- Custom overlays and widgets
- Data export (CSV, JSON)
- Feature flags for experimental functionality

## Tech Stack

- **Framework**: .NET 8 with WPF
- **Language**: C#
- **UI**: WPF with modern dark theme
- **Graphics**: SkiaSharp for GPU-accelerated rendering
- **Charts**: LiveCharts for data visualization
- **MVVM**: CommunityToolkit.Mvvm
- **Database**: SQLite with Entity Framework Core 8
- **HTTP**: HttpClient with rate limiting
- **Data Sources**: 
  - Ergast F1 API
  - Jolpica F1 API (planned)
  - OpenF1 API (planned)

## Architecture

```
MotorsportsApp/
├── src/
│   ├── MotorsportsApp.Desktop/      # WPF Application (UI Layer)
│   ├── MotorsportsApp.Core/         # Business Logic & ViewModels
│   ├── MotorsportsApp.Services/     # API Clients & External Services
│   ├── MotorsportsApp.Data/         # Entity Framework & Database
│   └── MotorsportsApp.Models/       # Domain Models & DTOs
```

### Project Responsibilities

- **Desktop**: WPF UI, XAML views, dependency injection setup
- **Core**: ViewModels, business logic, orchestration
- **Services**: HTTP clients, API integrations, live timing services
- **Data**: Entity Framework DbContext, repositories, caching
- **Models**: Shared domain entities, DTOs, enums

## Building

### Prerequisites
- .NET 8 SDK or later
- Windows 10/11 (for running the application)
- Visual Studio 2022 or JetBrains Rider (recommended for development)

### Build Instructions

```bash
# Clone the repository
git clone https://github.com/finwuh26/motorsportsapp.git
cd motorsportsapp

# Restore dependencies
dotnet restore

# Build the solution
dotnet build -c Release

# Run the application (on Windows)
dotnet run --project src/MotorsportsApp.Desktop -c Release
```

### Cross-Platform Development
The class libraries (Models, Services, Data, Core) are cross-platform and can be developed on Windows, macOS, or Linux. The Desktop WPF project requires Windows to run but can be built on other platforms with the EnableWindowsTargeting flag.

## Data Sources

### Ergast F1 API
- Historical F1 data from 1950 onwards
- Race schedules, results, driver and constructor information
- Free and open, no API key required
- Rate limit: Requests should be reasonable and spaced

### Planned Integrations
- **Jolpica F1 API**: Additional timing and telemetry data
- **OpenF1 API**: Real-time session data feeds
- **Session-specific feeds**: Live timing WebSocket connections

All APIs are abstracted behind a unified interface for easy swapping and graceful fallback.

## Database Schema

SQLite database (`motorsports.db`) stores:
- **Drivers**: Driver information and metadata
- **Constructors**: Team information
- **Circuits**: Track details and coordinates
- **Sessions**: Race weekend sessions (Practice, Quali, Race)
- **Timing**: Real-time timing data snapshots
- **LapData**: Individual lap times and sector splits
- **Weather**: Track and weather conditions

## Configuration

The application uses:
- SQLite for local storage (automatically created on first run)
- HTTP clients with automatic retry and rate limiting
- Dependency injection for service configuration
- EF Core migrations for database schema updates

## Contributing

Contributions are welcome! This project follows clean architecture principles and MVVM patterns. Please ensure:
- Code follows existing patterns and conventions
- New features include appropriate tests
- UI changes maintain the dark motorsport theme
- API integrations respect rate limits and include error handling

## Roadmap

### Phase 1: Foundation ✅ (Current)
- Project structure and architecture
- Basic UI and data models
- Ergast API integration
- Database setup

### Phase 2: Data Layer (In Progress)
- Complete API client implementations
- Caching strategy
- Rate limiting
- API fallback logic

### Phase 3: Core UI
- Main window layouts
- Navigation system
- Panel docking
- Theme system

### Phase 4-10: See problem statement for detailed roadmap

## License

This project is open source. See LICENSE file for details.

## Acknowledgments

- Inspired by f1-dash and professional F1 timing systems
- Built on community F1 APIs (Ergast, OpenF1, Jolpica)
- Uses open-source libraries and frameworks

## Disclaimer

This is a fan-made application using publicly available F1 data APIs. It is not affiliated with or endorsed by Formula 1, FIA, or any F1 teams. No proprietary data sources or DRM bypassing is used.
