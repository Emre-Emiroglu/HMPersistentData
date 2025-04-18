## [1.0.1] - 2025-04-18

### Added
- Introduced `PersistentDataConfig`, a `ScriptableObject` used for configuring the persistent data system.
- Added `PersistentDataServiceUtilities.Initialize()` method that automatically loads configuration from `Resources/HMPersistentData/PersistentDataConfig.asset`.
- Editor now automatically creates the config asset if missing.
- Editor window supports live editing of the config values.

### Changed
- Editor UI now reflects settings from `PersistentDataConfig` instead of hardcoded values.
- Updated README to explain the new initialization and configuration process.

### Fixed
- N/A

### Removed
- N/A