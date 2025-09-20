# XamarinApp2

Voorbeeld .NET MAUI-app (XamarinApp2) gemigreerd van Xamarin.Forms. Dit project richt zich op .NET 9 en ondersteunt Android, iOS, Windows en MacCatalyst.

Projecten
- `XamarinApp2` (.NET MAUI gedeeld project)
- `XamarinApp2.Android` (Android)
- `XamarinApp2.iOS` (iOS)
- `XamarinApp2.UWP` (Windows)

Vereisten
- Visual Studio 2022 17.10 of nieuwer met de .NET MAUI workload
- .NET 9 SDK
- Android SDK / emulatoren voor Android-ontwikkeling
- Een Mac met Xcode voor iOS builds (of een remote build host)

Bouwen en uitvoeren
1. Open de oplossing in Visual Studio.
2. Selecteer het gewenste platform/project en voer uit (F5 of "Start").
3. Herstel NuGet-pakketten indien nodig.

CLI-voorbeeld:
```
dotnet restore
dotnet build
dotnet run --framework net9.0-android # of net9.0-ios, net9.0-windows10.0.19041.0
```

Opmerkingen
- iOS builds vereisen doorgaans een Mac build host of remote agent.
- Dit project is gemigreerd van Xamarin.Forms naar .NET MAUI voor moderne cross-platform ondersteuning.

Bijdragen
- Issues en pull requests zijn welkom.

Licentie
- Er is geen licentiebestand opgenomen. Voeg er een toe als je de repository openbaar wilt maken.
