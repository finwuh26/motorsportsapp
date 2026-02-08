# Contributing to MotorsportsApp

Thank you for your interest in contributing to MotorsportsApp! This document provides guidelines for contributing to the project.

## Code of Conduct

- Be respectful and inclusive
- Focus on constructive feedback
- Help others learn and grow
- Keep discussions professional

## Getting Started

1. Fork the repository
2. Clone your fork: `git clone https://github.com/YOUR_USERNAME/motorsportsapp.git`
3. Create a branch: `git checkout -b feature/your-feature-name`
4. Make your changes
5. Test thoroughly
6. Commit with clear messages
7. Push and create a Pull Request

## Development Setup

See [DEVELOPMENT.md](DEVELOPMENT.md) for detailed setup instructions.

### Requirements
- .NET 8 SDK or later
- Visual Studio 2022 / JetBrains Rider / VS Code
- Basic knowledge of C# and WPF

## Coding Standards

### C# Style
- Follow [Microsoft C# Coding Conventions](https://docs.microsoft.com/en-us/dotnet/csharp/fundamentals/coding-style/coding-conventions)
- Use file-scoped namespaces (C# 10+)
- Prefer `var` for local variables when type is obvious
- Use expression-bodied members for simple methods
- Add XML documentation for public APIs

### XAML Style
- Use 4 spaces for indentation
- Group related properties
- Use meaningful x:Name values
- Prefer StaticResource over DynamicResource when possible

### Example

```csharp
namespace MotorsportsApp.Services;

/// <summary>
/// Service for managing F1 session data
/// </summary>
public class SessionService : ISessionService
{
    private readonly ILogger<SessionService> _logger;

    public SessionService(ILogger<SessionService> _logger)
    {
        _logger = logger;
    }

    /// <summary>
    /// Gets the current active session
    /// </summary>
    public async Task<Session?> GetCurrentSessionAsync()
    {
        // Implementation
    }
}
```

## Project Structure

Respect the layered architecture:
- **Desktop**: UI and XAML only
- **Core**: ViewModels and business logic
- **Services**: External integrations
- **Data**: Database access
- **Models**: Shared entities

## Pull Request Process

1. **Update Documentation**: Update README.md or DEVELOPMENT.md if needed
2. **Add Tests**: Include unit tests for new features
3. **Follow Conventions**: Ensure code follows project standards
4. **Describe Changes**: Provide clear PR description
5. **Link Issues**: Reference related issues with #issue-number

### PR Checklist
- [ ] Code builds without errors
- [ ] Tests pass
- [ ] Documentation updated
- [ ] Follows coding standards
- [ ] No unnecessary dependencies added
- [ ] Performance impact considered

## Areas to Contribute

### High Priority
- Live timing WebSocket implementation
- Track map visualization
- Lap time analysis tools
- Telemetry graphs

### Medium Priority
- Additional API integrations
- UI themes
- Keyboard shortcuts
- Session replay features

### Good First Issues
- UI polish and styling
- Documentation improvements
- Bug fixes
- Test coverage

## Testing

### Running Tests
```bash
dotnet test
```

### Writing Tests
- Use MSTest framework
- Follow Arrange-Act-Assert pattern
- Mock external dependencies
- Test edge cases

Example:
```csharp
[TestClass]
public class SessionServiceTests
{
    [TestMethod]
    public async Task GetCurrentSession_WhenActive_ReturnsSession()
    {
        // Arrange
        var mockRepo = new Mock<ISessionRepository>();
        var service = new SessionService(mockRepo.Object);

        // Act
        var result = await service.GetCurrentSessionAsync();

        // Assert
        Assert.IsNotNull(result);
    }
}
```

## API Usage Guidelines

When integrating APIs:
- Respect rate limits
- Cache responses appropriately
- Handle errors gracefully
- Don't scrape paid services
- No DRM bypassing
- Follow API terms of service

## Documentation

- Update README.md for user-facing changes
- Update DEVELOPMENT.md for developer changes
- Add XML documentation for public APIs
- Comment complex logic

## Questions?

- Check existing issues
- Review DEVELOPMENT.md
- Ask in discussions
- Open an issue for clarification

## License

By contributing, you agree that your contributions will be licensed under the MIT License.
