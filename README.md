# EmptyApp

A .NET MAUI solution built with .NET 10. Includes a legacy Xamarin.Forms project.

## Requirements

- .NET 10.0 SDK (Preview or later)
- Visual Studio 2022 17.10 or later with .NET MAUI workload
- Windows 10 SDK 10.0.19041.0 or later (for Windows development)

## Getting Started

1. Ensure you have .NET 10.0 SDK installed:
   ```bash
   dotnet --list-sdks
   ```

2. Clone the repository and navigate to a project, e.g. MauiApp1/MauiApp2/MauiApp3/XamarinApp1:
   ```bash
   cd MauiApp1
   ```

3. Restore dependencies:
   ```bash
   dotnet restore
   ```

4. Build the project:
   ```bash
   dotnet build
   ```

5. Run the application:
   ```bash
   # For Windows
   dotnet run --framework net10.0-windows10.0.19041.0
   
   # For Android
   dotnet run --framework net10.0-android
   
   # For iOS (requires macOS)
   dotnet run --framework net10.0-ios
   
   # For MacCatalyst (requires macOS)
   dotnet run --framework net10.0-maccatalyst
   ```

## Project Structure

- **MauiApp1.csproj / MauiApp2.csproj / MauiApp3.csproj**: Project files targeting .NET 10
- **XamarinApp1.csproj**: Legacy Xamarin.Forms project
- **Platforms/**: Platform-specific code for Windows, Android, iOS, and MacCatalyst
- **Pages/**: XAML pages for the user interface
- **PageModels/**: ViewModels using CommunityToolkit.Mvvm
- **Services/**: Business logic and data services
- **Models/**: Data models
- **Resources/**: Images, fonts, and other assets

## Dependencies

- **Microsoft.Maui.Controls**: .NET MAUI UI framework
- **CommunityToolkit.Mvvm**: MVVM toolkit for .NET MAUI
- **CommunityToolkit.Maui**: Additional MAUI controls and utilities
- **Microsoft.Data.Sqlite.Core**: SQLite database support
- **Syncfusion.Maui.Toolkit**: UI components and controls

## Notes

- This solution uses .NET 10 Preview, which is the latest version of .NET
- Some warnings may appear during build related to AOT compatibility in WinRT scenarios
- The application supports multiple platforms: Windows, Android, iOS, and MacCatalyst
- `XamarinApp1` is a legacy Xamarin.Forms project included for compatibility/reference
