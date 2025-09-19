# XamarinApp1

This repository contains a multi-target .NET MAUI sample app (XamarinApp1) migrated from Xamarin.Forms. It now targets .NET 9 and supports Android, iOS, Windows, and MacCatalyst platforms.

Projects
- `XamarinApp1` (.NET MAUI shared project)
- `XamarinApp1.Android` (Android app)
- `XamarinApp1.iOS` (iOS app)
- `XamarinApp1.UWP` (Windows app)

Prerequisites
- Visual Studio 2022 17.10 or later with .NET MAUI workload
- .NET 9.0 SDK
- Android SDK / emulators for Android development
- A Mac with Xcode for building and debugging iOS projects (or use Hot Restart where applicable)

Build and run
1. Open the solution in Visual Studio.
2. Select the platform/project you want to run (Android, iOS, or Windows).
3. Restore NuGet packages if prompted.
4. Deploy to an emulator or device from Visual Studio.
   
   Or use CLI:
   ```bash
   dotnet restore
   dotnet build
   dotnet run --framework net9.0-android # or net9.0-ios, net9.0-windows10.0.19041.0
   ```

Notes
- iOS builds require a Mac build host or remote Mac build agent.
- Target frameworks now use .NET 9.0 for all supported platforms.
- This project was migrated from Xamarin.Forms to .NET MAUI for modern cross-platform support.

Contributing
- Feel free to open issues and pull requests.

License
- This repository does not include a license file. Add one if you intend to make the project public.
