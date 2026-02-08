# Development Guide

## Architecture Overview

This application follows **Clean Architecture** principles with clear separation of concerns:

### Layer Structure

```
┌─────────────────────────────────────┐
│  Presentation (Desktop - WPF)       │  ← UI, XAML, Views
├─────────────────────────────────────┤
│  Application (Core)                 │  ← ViewModels, Business Logic
├─────────────────────────────────────┤
│  Infrastructure                     │
│  ├── Services (API Clients)         │  ← HTTP clients, WebSockets
│  └── Data (EF Core, Repositories)   │  ← Database access, caching
├─────────────────────────────────────┤
│  Domain (Models)                    │  ← Entities, DTOs, Enums
└─────────────────────────────────────┘
```

### Dependency Flow
- **Desktop** → Core, Models
- **Core** → Services, Data, Models
- **Services** → Models
- **Data** → Models
- **Models** → (No dependencies)

## MVVM Pattern

We use the **Model-View-ViewModel** pattern throughout:

- **Models**: Domain entities in `MotorsportsApp.Models`
- **Views**: XAML files in `MotorsportsApp.Desktop/Views`
- **ViewModels**: Observable classes in `MotorsportsApp.Core/ViewModels`

### ViewModel Example

```csharp
public partial class ExampleViewModel : ObservableObject
{
    [ObservableProperty]
    private string _title = "Example";
    
    [RelayCommand]
    private async Task LoadDataAsync()
    {
        // Command implementation
    }
}
```

Using `CommunityToolkit.Mvvm` source generators for:
- `[ObservableProperty]` → Generates property change notifications
- `[RelayCommand]` → Generates ICommand implementations

## Dependency Injection

Services are registered in `App.xaml.cs`:

```csharp
services.AddDbContext<MotorsportsDbContext>(options =>
    options.UseSqlite("Data Source=motorsports.db"));

services.AddHttpClient<IF1DataService, ErgastApiClient>();
services.AddSingleton<ILiveTimingService, LiveTimingService>();

services.AddTransient<MainViewModel>();
services.AddSingleton<MainWindow>();
```

### Service Lifetimes
- **Singleton**: One instance for application lifetime (e.g., LiveTimingService)
- **Scoped**: One instance per scope/request (e.g., DbContext)
- **Transient**: New instance every time (e.g., ViewModels)

## Data Access

### Entity Framework Core

DbContext configuration in `MotorsportsDbContext`:

```csharp
protected override void OnModelCreating(ModelBuilder modelBuilder)
{
    modelBuilder.Entity<Driver>(entity =>
    {
        entity.HasKey(e => e.DriverId);
        entity.Property(e => e.Code).HasMaxLength(3);
    });
}
```

### Migrations

```bash
# Add a new migration
dotnet ef migrations add MigrationName --project src/MotorsportsApp.Data

# Update database
dotnet ef database update --project src/MotorsportsApp.Data
```

## API Integration

### Creating a New API Client

1. Define interface in `Services/Interfaces/`:
```csharp
public interface IMyApiClient
{
    Task<Data> GetDataAsync();
}
```

2. Implement in `Services/ApiClients/`:
```csharp
public class MyApiClient : IMyApiClient
{
    private readonly HttpClient _httpClient;
    
    public MyApiClient(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }
    
    public async Task<Data> GetDataAsync()
    {
        return await _httpClient.GetFromJsonAsync<Data>("endpoint");
    }
}
```

3. Register in DI container:
```csharp
services.AddHttpClient<IMyApiClient, MyApiClient>();
```

### Rate Limiting

Implement rate limiting using `System.Threading.RateLimiting`:

```csharp
services.AddHttpClient<IF1DataService, ErgastApiClient>()
    .AddPolicyHandler(Policy.RateLimitAsync(10, TimeSpan.FromMinutes(1)));
```

## UI Development

### Creating a New View

1. **Create ViewModel** in `Core/ViewModels/`:
```csharp
public partial class NewViewModel : ObservableObject
{
    [ObservableProperty]
    private string _data;
}
```

2. **Create XAML View** in `Desktop/Views/`:
```xaml
<UserControl x:Class="MotorsportsApp.Desktop.Views.NewView"
             xmlns:vm="clr-namespace:MotorsportsApp.Core.ViewModels">
    <UserControl.DataContext>
        <vm:NewViewModel />
    </UserControl.DataContext>
    
    <TextBlock Text="{Binding Data}" />
</UserControl>
```

3. **Register in DI**:
```csharp
services.AddTransient<NewViewModel>();
services.AddTransient<NewView>();
```

### Styling

Global styles are defined in `App.xaml` or separate Resource Dictionaries:

```xaml
<Application.Resources>
    <SolidColorBrush x:Key="AccentBrush" Color="#E10600"/>
    
    <Style TargetType="Button">
        <Setter Property="Background" Value="{StaticResource AccentBrush}"/>
    </Style>
</Application.Resources>
```

## Testing

### Unit Tests

Create test projects for each layer:

```bash
dotnet new mstest -n MotorsportsApp.Core.Tests
dotnet new mstest -n MotorsportsApp.Services.Tests
```

### Example Test

```csharp
[TestClass]
public class MainViewModelTests
{
    [TestMethod]
    public async Task LoadData_Should_PopulateDrivers()
    {
        // Arrange
        var mockService = new Mock<IF1DataService>();
        var viewModel = new MainViewModel(mockService.Object);
        
        // Act
        await viewModel.LoadDataCommand.ExecuteAsync(null);
        
        // Assert
        Assert.IsTrue(viewModel.Drivers.Count > 0);
    }
}
```

## Performance Considerations

### Async/Await
- Always use `async`/`await` for I/O operations
- Use `ConfigureAwait(false)` in library code
- Avoid blocking calls like `.Result` or `.Wait()`

### Collections
- Use `ObservableCollection<T>` for UI-bound collections
- Use `List<T>` for internal processing
- Consider `ImmutableList<T>` for thread-safe scenarios

### Memory Management
- Dispose `HttpClient` responses
- Unsubscribe from events in `Dispose()`
- Use `WeakEventManager` for long-lived event subscriptions

## Live Timing Implementation

### WebSocket Connection

```csharp
public class LiveTimingService : ILiveTimingService
{
    private ClientWebSocket? _webSocket;
    
    public async Task ConnectAsync(string sessionId)
    {
        _webSocket = new ClientWebSocket();
        await _webSocket.ConnectAsync(new Uri($"wss://.../{sessionId}"), CancellationToken.None);
        
        _ = Task.Run(ReceiveLoop);
    }
    
    private async Task ReceiveLoop()
    {
        var buffer = new byte[4096];
        while (_webSocket?.State == WebSocketState.Open)
        {
            var result = await _webSocket.ReceiveAsync(buffer, CancellationToken.None);
            // Process data and raise events
        }
    }
}
```

## Debugging

### Enable Detailed Logging

```csharp
services.AddLogging(builder =>
{
    builder.AddConsole();
    builder.AddDebug();
    builder.SetMinimumLevel(LogLevel.Debug);
});
```

### Database Inspection

Use SQLite browser or:
```bash
sqlite3 motorsports.db
.schema
SELECT * FROM Drivers;
```

### Network Debugging

Use Fiddler or browser DevTools to inspect HTTP/WebSocket traffic.

## Build & Deployment

### Release Build

```bash
dotnet publish src/MotorsportsApp.Desktop -c Release -r win-x64 --self-contained
```

### MSIX Packaging

Add to Desktop project:
```xml
<PropertyGroup>
    <GenerateAppInstallerFile>False</GenerateAppInstallerFile>
    <AppxPackageSigningEnabled>True</AppxPackageSigningEnabled>
    <PackageCertificateThumbprint>...</PackageCertificateThumbprint>
</PropertyGroup>
```

Build MSIX:
```bash
msbuild src/MotorsportsApp.Desktop /p:Configuration=Release /p:Platform=x64 /p:UapAppxPackageBuildMode=StoreUpload
```

## Common Patterns

### Repository Pattern

```csharp
public interface IRepository<T>
{
    Task<T?> GetByIdAsync(string id);
    Task<IEnumerable<T>> GetAllAsync();
    Task AddAsync(T entity);
    Task UpdateAsync(T entity);
    Task DeleteAsync(string id);
}
```

### Service Pattern

```csharp
public interface IDataService
{
    Task<Result<Data>> GetDataAsync();
}

public class Result<T>
{
    public bool IsSuccess { get; set; }
    public T? Data { get; set; }
    public string? ErrorMessage { get; set; }
}
```

### Event Aggregator

For decoupled component communication:

```csharp
public interface IEventAggregator
{
    void Subscribe<T>(Action<T> handler);
    void Publish<T>(T message);
}
```

## Code Style

- Use C# 12 features (file-scoped namespaces, global usings)
- Prefer `var` for local variables when type is obvious
- Use expression-bodied members for simple properties/methods
- Follow Microsoft C# naming conventions
- Keep methods small and focused (< 20 lines ideal)
- Add XML documentation for public APIs

## Resources

- [.NET Documentation](https://docs.microsoft.com/dotnet)
- [WPF Documentation](https://docs.microsoft.com/dotnet/desktop/wpf)
- [MVVM Toolkit](https://learn.microsoft.com/windows/communitytoolkit/mvvm)
- [Entity Framework Core](https://docs.microsoft.com/ef/core)
- [Ergast API Docs](http://ergast.com/mrd/)
