# Slot Configuration Validation & Comparison Tool

A modern, high-performance CLI tool built in .NET 10 to validate and compare game slot configuration files (JSON and YAML format). This tool loads configurations into a workspace, executes model-based validation checks, and enables comparison between different configuration files.

---

## 1. Setup Instructions

### Prerequisites
* **.NET 10 SDK** (Installed on your system)
* Supported configuration formats: **JSON** (`.json`) and **YAML** (`.yml`, `.yaml`)

### Installation & Build
Clone the repository and build the solution using the .NET CLI:
```powershell
# Navigate to the project root
cd c:/Users/phucn/Desktop/PersonalProjects/ConfigurationValidation

# Build the solution
dotnet build ConfigurationValidation.slnx
```

### CLI Command Reference
The application is built using Cocona, offering command routing and parameters:

#### 1. Load Files to Workspace
Copies specified configuration files into the local `./workspace/` directory for validation and comparison.
```powershell
dotnet run --project src/ConfigurationValidation.csproj -- loadfiles <path-to-file1> <path-to-file2>
```
* **Command Handler**: [LoadFilesCommand](file:///c:/Users/phucn/Desktop/PersonalProjects/ConfigurationValidation/src/Commands/LoadFilesCommand.cs)
* **Rules**: 
  * Files must have `.json`, `.yml`, or `.yaml` extensions.
  * Duplicate file entries in the command arguments are rejected.

#### 2. Verify Configuration File
Verifies a configuration file present inside the workspace directory against domain model constraints.
```powershell
dotnet run --project src/ConfigurationValidation.csproj -- verify <filename-in-workspace>
```
* **Command Handler**: [VerifyConfigurationFileCommand](file:///c:/Users/phucn/Desktop/PersonalProjects/ConfigurationValidation/src/Commands/VerifyConfigurationFileCommand.cs)
* **Rules**: Parses the file and outputs configuration errors if any required properties are missing or invalid.

#### 3. Compare Configuration Files
Compares two configuration files present in the workspace.
```powershell
dotnet run --project src/ConfigurationValidation.csproj -- compare <file1> <file2>
```
* **Command Handler**: [CompareFilesCommand](file:///c:/Users/phucn/Desktop/PersonalProjects/ConfigurationValidation/src/Commands/CompareFilesCommand.cs)

### Running Unit Tests
Execute the unit test suite inside the test project:
```powershell
dotnet test test/ConfigurationValidation.Tests.csproj
```
* **Test Project**: [ConfigurationValidation.Tests.csproj](file:///c:/Users/phucn/Desktop/PersonalProjects/ConfigurationValidation/test/ConfigurationValidation.Tests.csproj)

---

## 2. Design Choices

### CLI Framework: Cocona
* The CLI application is powered by [Cocona](https://github.com/mayuki/Cocona), a lightweight and highly efficient CLI framework for .NET.
* Cocona allows us to map method parameters (like arguments and options) directly to command-line arguments, automatically generating standard `--help` outputs and validating arguments.
* CLI entry-point: [Program.cs](file:///c:/Users/phucn/Desktop/PersonalProjects/ConfigurationValidation/src/Program.cs).

### Data Models & Serialization
* **Unified Domain Models**: Configurations are mapped to a nested record structure in [SlotGameConfiguration.cs](file:///c:/Users/phucn/Desktop/PersonalProjects/ConfigurationValidation/src/Shared/Models/SlotGameConfiguration.cs) (e.g. `SlotConfig`, `GameMetadata`, `BetSettings`, `ReelConfiguration`, etc.).
* **Multi-Format Parsing**: The tool uses a unified internal [Parser](file:///c:/Users/phucn/Desktop/PersonalProjects/ConfigurationValidation/src/Shared/Utils/Parser.cs) which uses:
  * `System.Text.Json` with `snake_case_lower` naming conventions for JSON configurations.
  * `YamlDotNet` with `PascalCase` naming conventions for YAML configurations.

### Workspace-Centric Architecture
* By staging configurations inside a local `./workspace/` directory using the `loadfiles` command, it ensures that verify and compare commands are decoupled from original file locations, making operations fast, isolated, and repeatable.

---

## 3. Future Improvements

### 3.1 Finish Validation & Compare Logic
* **Validation**: Extend the `Validate` methods inside [SlotGameConfiguration.cs](file:///c:/Users/phucn/Desktop/PersonalProjects/ConfigurationValidation/src/Shared/Models/SlotGameConfiguration.cs) to validate business rules such as verifying that `DefaultBet` is within `BetLevels`, `ReelCount` matches the actual reel lists, and campaign start dates precede end dates.
* **Comparison**: Complete the implementation in [CompareFilesCommand.cs](file:///c:/Users/phucn/Desktop/PersonalProjects/ConfigurationValidation/src/Commands/CompareFilesCommand.cs) to produce deep diffs highlighting modified, added, or deleted configuration keys.

### 3.2 Add Generate HTML Report
* Introduce a reporting feature that outputs an interactive, aesthetically pleasing HTML page visualizing the status of validation checks (Pass/Fail) and side-by-side diffs for compared configurations.

### 3.3 Add Unit Tests
* Implement unit tests in `ConfigurationValidation.Tests` using xUnit and mocking libraries to test custom parsing corner cases, validation error outputs, and the comparison utility logic.

### 3.4 Add WebUI
* Develop a lightweight Web UI (e.g., using ASP.NET Core Minimal APIs / Blazor or a Next.js/Vite frontend) to drag-and-drop config files, run validation rules, and inspect config differences side-by-side.

---

## 4. AI Usage Notes
* An AI assistant (e.g. Google Gemini) was used during development as a query assistant for rapid reference on library syntax, such as configuring custom naming conventions for `YamlDotNet` serialization and setting up Cocona commands routing/parameters.
