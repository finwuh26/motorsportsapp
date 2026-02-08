# Features & Roadmap

## Current Features (v0.1.0-alpha)

### ✅ Foundation
- [x] Clean architecture with 5 layers
- [x] MVVM pattern implementation
- [x] Dependency injection
- [x] SQLite database integration
- [x] Dark motorsport-themed UI

### ✅ Data Access
- [x] Ergast F1 API client
- [x] OpenF1 API client  
- [x] Multi-API fallback system
- [x] Intelligent memory caching
- [x] API health monitoring
- [x] Rate limiting protection

### ✅ Basic UI
- [x] Main window shell
- [x] Upcoming races list
- [x] Current season drivers
- [x] Session information display
- [x] Loading indicators

### ✅ Domain Models
- [x] Driver entity
- [x] Constructor entity
- [x] Circuit entity
- [x] Session entity
- [x] Timing data model
- [x] Lap data model
- [x] Weather model

## In Development (v0.2.0-alpha)

### 🚧 Enhanced UI
- [ ] Live timing view
- [ ] Analysis dashboard
- [ ] Session replay view
- [ ] Navigation menu
- [ ] View switching

### 🚧 Live Data
- [ ] WebSocket live timing
- [ ] Real-time leaderboard
- [ ] Live weather updates
- [ ] Session status updates

## Planned Features

### v0.3.0 - Live Timing Core
- [ ] Real-time position updates
- [ ] Gap and interval display
- [ ] Sector times
- [ ] Tyre compound indicators
- [ ] Pit stop detection
- [ ] DRS status
- [ ] Flag status display
- [ ] Session clock

### v0.4.0 - Track Map
- [ ] 2D track visualization
- [ ] Car position markers
- [ ] Speed visualization
- [ ] Mini-sector coloring
- [ ] Zoom and pan
- [ ] Multiple camera angles

### v0.5.0 - Analysis Tools
- [ ] Lap time graphs
- [ ] Tyre degradation analysis
- [ ] Pace comparison
- [ ] Gap evolution charts
- [ ] Sector comparison
- [ ] Speed trap data

### v0.6.0 - Session Replay
- [ ] Record live sessions
- [ ] Timeline scrubber
- [ ] Playback controls
- [ ] Variable speed playback
- [ ] Jump to key moments
- [ ] Export highlights

### v0.7.0 - Historical Data
- [ ] Season comparison
- [ ] Driver statistics
- [ ] Constructor standings
- [ ] Race results archive
- [ ] Qualifying history
- [ ] Lap record tracking

### v0.8.0 - Advanced Features
- [ ] Multi-driver comparison
- [ ] Strategy predictions
- [ ] Pit stop analysis
- [ ] Race simulation
- [ ] What-if scenarios

### v0.9.0 - User Experience
- [ ] Customizable layouts
- [ ] Dockable panels
- [ ] Keyboard shortcuts
- [ ] Themes (light/dark/custom)
- [ ] Settings persistence
- [ ] Auto-update system
- [ ] First-run wizard

### v1.0.0 - Production Release
- [ ] Performance optimization
- [ ] Full test coverage
- [ ] Complete documentation
- [ ] MSIX installer
- [ ] Portable executable
- [ ] User manual
- [ ] Video tutorials

## Future Enhancements

### Plugin System
- [ ] Plugin API
- [ ] Plugin marketplace
- [ ] Custom widgets
- [ ] Data source plugins
- [ ] Export format plugins

### Additional Series
- [ ] Formula 2 support
- [ ] Formula 3 support
- [ ] Formula E support
- [ ] MotoGP support
- [ ] IndyCar support
- [ ] WEC support

### Social Features
- [ ] Discord integration
- [ ] Twitter integration
- [ ] Twitch overlay mode
- [ ] OBS integration
- [ ] Custom overlays

### Advanced Analytics
- [ ] Machine learning predictions
- [ ] Weather impact analysis
- [ ] Tyre strategy optimizer
- [ ] Lap time predictor
- [ ] Championship scenarios

### Data Export
- [ ] CSV export
- [ ] JSON export
- [ ] PDF reports
- [ ] Excel workbooks
- [ ] API endpoint (local)

## Feature Requests

To request a feature:
1. Check existing issues
2. Open a new issue with tag `enhancement`
3. Describe the feature
4. Explain use case
5. Add mockups if applicable

## Known Limitations

### Current Alpha Status
- Limited to basic race calendar and driver info
- No live timing yet (WebSocket not implemented)
- No historical data beyond current season
- Windows-only (WPF limitation)
- Single language (English)

### API Limitations
- Ergast API: Historical data only, no live timing
- OpenF1 API: Limited historical data
- No official FIA/F1 API integration
- Rate limits apply to all APIs

### Performance Notes
- First load may be slow (API calls)
- Large datasets not optimized yet
- Memory usage increases with cached data
- SQLite database size grows over time

## Contributing

See [CONTRIBUTING.md](CONTRIBUTING.md) for how to contribute features.

Priority areas for contributions:
1. Live timing WebSocket implementation
2. Track map visualization
3. Telemetry graphing
4. Session replay UI
5. Testing and documentation

## Versioning

We use [Semantic Versioning](https://semver.org/):
- **MAJOR**: Breaking changes
- **MINOR**: New features (backwards compatible)
- **PATCH**: Bug fixes

## Release Schedule

- **Alpha** (current): Monthly releases
- **Beta**: Bi-weekly releases (after v0.5.0)
- **Stable**: As ready (after v1.0.0)

## Support

For questions about features:
- Check documentation
- Search existing issues
- Open a discussion
- Join Discord (coming soon)
