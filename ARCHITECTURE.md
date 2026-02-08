# Architecture Overview

## System Architecture

```
┌─────────────────────────────────────────────────────────────┐
│                     Desktop (WPF UI)                        │
│  ┌──────────────┐  ┌──────────────┐  ┌──────────────┐     │
│  │  MainWindow  │  │ LiveTiming   │  │   Analysis   │     │
│  │    View      │  │     View     │  │     View     │     │
│  └──────────────┘  └──────────────┘  └──────────────┘     │
└─────────────────────────────────────────────────────────────┘
                            │
                            ▼
┌─────────────────────────────────────────────────────────────┐
│                   Core (ViewModels)                         │
│  ┌──────────────┐  ┌──────────────┐  ┌──────────────┐     │
│  │     Main     │  │ LiveTiming   │  │   Analysis   │     │
│  │  ViewModel   │  │  ViewModel   │  │  ViewModel   │     │
│  └──────────────┘  └──────────────┘  └──────────────┘     │
└─────────────────────────────────────────────────────────────┘
                            │
        ┌───────────────────┴───────────────────┐
        ▼                                       ▼
┌──────────────────────┐            ┌──────────────────────┐
│  Services            │            │  Data (EF Core)      │
│  ┌────────────────┐  │            │  ┌────────────────┐  │
│  │ Cached         │  │            │  │   DbContext    │  │
│  │ F1DataService  │  │            │  └────────────────┘  │
│  │  (Decorator)   │  │            │  ┌────────────────┐  │
│  └────────────────┘  │            │  │ Repositories   │  │
│         │            │            │  └────────────────┘  │
│  ┌──────▼────────┐   │            │         │           │
│  │  Composite    │   │            │         ▼           │
│  │ F1DataService │   │            │   ┌──────────┐      │
│  │  (Fallback)   │   │            │   │  SQLite  │      │
│  └───────────────┘   │            │   └──────────┘      │
│    │         │       │            └──────────────────────┘
│    ▼         ▼       │
│ ┌──────┐ ┌──────┐   │
│ │Ergast│ │OpenF1│   │
│ │ API  │ │ API  │   │
│ └──────┘ └──────┘   │
└──────────────────────┘
```

## Data Flow

### 1. User Interaction
```
User Action → View → ViewModel → Command → Service → API/Database
```

### 2. API Response
```
API → Service → Cache → ViewModel → View → User
```

### 3. Live Data
```
WebSocket → LiveTimingService → Event → ViewModel → ObservableCollection → View
```

## Key Components

### Desktop Layer
- **MainWindow**: Application shell with navigation
- **Views**: XAML user controls for different features
- **Resources**: Styles, templates, converters

### Core Layer
- **ViewModels**: Observable objects with commands
- **Business Logic**: Domain-specific operations
- **Validation**: Input validation rules

### Services Layer
- **API Clients**: HTTP communication with F1 APIs
- **Caching**: Memory cache decorator
- **Live Timing**: WebSocket service for real-time data
- **Health Checks**: API availability monitoring

### Data Layer
- **DbContext**: Entity Framework configuration
- **Entities**: Database models
- **Repositories**: Data access patterns
- **Migrations**: Database schema versioning

### Models Layer
- **Domain Models**: Core business entities
- **DTOs**: Data transfer objects
- **Enums**: Type-safe constants

## Design Patterns

### MVVM (Model-View-ViewModel)
- Separates UI from business logic
- Uses data binding for reactive UI
- Commands for user interactions

### Decorator Pattern
```
IF1DataService
    └─ CachedF1DataService
        └─ CompositeF1DataService
            ├─ ErgastApiClient
            └─ OpenF1ApiClient
```

### Repository Pattern
- Abstracts data access
- Enables testing
- Centralizes data logic

### Dependency Injection
- Constructor injection throughout
- Service lifetime management
- Loose coupling

### Observer Pattern
- ObservableCollection for UI binding
- Events for cross-component communication
- INotifyPropertyChanged for property updates

## Technology Stack

| Layer | Technologies |
|-------|-------------|
| UI | WPF, XAML, SkiaSharp, LiveCharts |
| Logic | C# 12, .NET 8 |
| MVVM | CommunityToolkit.Mvvm |
| Data | EF Core 8, SQLite |
| HTTP | HttpClient, System.Text.Json |
| DI | Microsoft.Extensions.DependencyInjection |
| Caching | Microsoft.Extensions.Caching.Memory |
| Logging | Microsoft.Extensions.Logging |

## Performance Optimizations

### Caching Strategy
- Memory cache for API responses
- SQLite for persistent storage
- Cache expiration policies per data type

### Async/Await
- Non-blocking I/O operations
- Responsive UI
- Parallel API calls

### Data Binding
- ObservableCollection for efficient updates
- Value converters for data transformation
- Template selectors for dynamic UI

## Security Considerations

### API Safety
- No API keys stored
- Rate limiting
- Timeout policies
- Error handling

### Data Privacy
- Local-only storage
- No user tracking
- No telemetry (optional)

### Code Security
- Input validation
- SQL injection prevention (EF Core)
- XSS prevention in UI

## Extensibility Points

### Plugin System (Planned)
- Interface-based plugins
- Dynamic assembly loading
- Isolated plugin contexts

### Custom Data Sources
- Implement IF1DataService
- Register in DI container
- Automatic fallback integration

### UI Themes
- Resource dictionaries
- Dynamic theme switching
- Custom color schemes

### Export Formats
- IDataExporter interface
- CSV, JSON, XML exporters
- Custom format support
