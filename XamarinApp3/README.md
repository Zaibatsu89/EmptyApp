# XamarinApp3

Voorbeeld .NET MAUI-app (XamarinApp3) gemigreerd van Xamarin.Forms. Dit project richt zich op .NET 9 en ondersteunt Android, iOS, en Windows.

### Projectstructuur

Na de migratie is de oude structuur met aparte platform-projecten samengevoegd tot één **single project**. De belangrijkste onderdelen zijn nu:

-   **`XamarinApp3.csproj`**: Het centrale .NET MAUI-project dat alle gedeelde code en UI bevat.
-   **`Platforms/`**: Deze map bevat de platform-specifieke opstartcode (`MainActivity.cs`, `AppDelegate.cs`, etc.) en resources.
-   **`Resources/`**: Een centrale map voor alle gedeelde assets zoals app-iconen, splash screens, lettertypen, afbeeldingen en ruwe assets.

### Vereisten

-   Visual Studio 2022 17.10 of nieuwer met de **.NET Multi-platform App UI development** workload.
-   .NET 9 SDK.
-   Android SDK / emulatoren voor Android-ontwikkeling.
-   Een Mac met de laatste versie van Xcode voor iOS builds.

### Bouwen en uitvoeren

1.  Open de oplossing (`XamarinApp3.sln`) in Visual Studio.
2.  Herstel de NuGet-pakketten indien nodig.
3.  Selecteer het gewenste platform (bijv. `net9.0-android`) in de debug-target dropdown en start de app (F5).

**CLI-voorbeeld:**

```bash
# Herstel dependencies
dotnet restore

# Bouw de app voor een specifiek platform
dotnet build -f net9.0-android

# Voer de app uit op een aangesloten apparaat of emulator
dotnet build -t:Run -f net9.0-android
```

---

### Aantekeningen bij de Migratie

Deze sectie bevat belangrijke lessen uit het migratieproces van Xamarin.Forms naar .NET MAUI.

#### **Belangrijke Stap: `Resource.designer.cs` Verwijderen**

Het `Resource.designer.cs`-bestand in het oude Xamarin.Android-project is overbodig in .NET MAUI en veroorzaakt conflicten tijdens het migratieproces. Het nieuwe build-systeem van .NET genereert resource-ID's op een andere manier.

Om een soepele migratie met de .NET Upgrade Assistant te garanderen, is het cruciaal om dit bestand **proactief uit te sluiten of te verwijderen** uit het `.Android.csproj`-bestand ***voordat*** je de assistent op het Android-project start. Dit voorkomt build-fouten en zorgt voor een schone overgang.

---

### Bijdragen

-   Issues en pull requests zijn welkom.

### Licentie

-   Er is geen licentiebestand opgenomen. Voeg er een toe als je de repository openbaar wilt maken.