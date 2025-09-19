# XamarinApp1

This repository contains a multi-target Xamarin.Forms sample app (XamarinApp1) with projects for Android, iOS, and UWP plus a shared .NET Standard project.

Projects
- `XamarinApp1` (shared .NET Standard project)
- `XamarinApp1.Android` (Android app)
- `XamarinApp1.iOS` (iOS app)
- `XamarinApp1.UWP` (UWP app)

Prerequisites
- Visual Studio 2019 or 2022 with Xamarin and Mobile development workload
- .NET 5 SDK (for any .NET Core tools used by the solution)
- Android SDK / emulators for Android development
- A Mac with Xcode for building and debugging iOS projects (or use Hot Restart where applicable)

Build and run
1. Open the solution in Visual Studio.
2. Select the platform/project you want to run (Android, iOS, or UWP).
3. Restore NuGet packages if prompted.
4. Deploy to an emulator or device from Visual Studio.

Notes
- iOS builds require a Mac build host or remote Mac build agent.
- Target frameworks used in this workspace may include .NET Core 5.0, Xamarin Android/iOS targets, and .NET Standard 2.0 for shared code.

Contributing
- Feel free to open issues and pull requests.

License
- This repository does not include a license file. Add one if you intend to make the project public.
