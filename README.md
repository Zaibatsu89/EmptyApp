# Empty App

**Empty App** is een startproject dat is gemaakt met .NET MAUI. Het biedt een basisstructuur met ingebouwde ondersteuning voor Shell-navigatie, themaselectie, SQLite-databasebeheer en een eenvoudige repository-laag. Dit project is ideaal voor ontwikkelaars die een solide startpunt zoeken voor het bouwen van cross-platform mobiele en desktop-applicaties.

## Functionaliteiten

- **.NET MAUI Shell**:
  - Voorgeconfigureerde navigatiestructuur met Flyout-menu, inclusief routes voor Dashboard, Projecten en Meta-beheer.
  - Dynamische themaselectie (Licht & Donker) met behulp van een Syncfusion Segmented Control.

- **SQLite Databasebeheer**:
  - Beheer categorieën met CRUD-functionaliteiten via een `CategoryRepository`.
  - Automatische initialisatie en dynamisch maken van de database.

- **Snackbar- en Toastmeldingen**:
  - Gebruik van CommunityToolkit.Maui voor visuele meldingen.
  - Snackbar voor foutmeldingen en waarschuwingen.
  - Toast ondersteunt mobiele platforms.

- **Cross-platform**:
  - Werkt naadloos op Windows, Android, iOS en macOS.

## Projectstructuur

- **`MauiApp1/App.xaml` en `App.xaml.cs`**  
  Definieert globale stijlen en de applicatie-initialisatie.

- **`MauiApp1/AppShell.xaml` en `AppShell.xaml.cs`**  
  Beheert navigatie en themaselectie.

- **`MauiApp1/Data/CategoryRepository.cs`**  
  Implementeert databasebeheer voor de `Category`-entiteit.

- **`MauiApp1/Resources/Styles`**  
  Bevat stijlen en kleurenschema’s voor de app.

## Installatie

1. **Clone de repository**:
   ```bash
   git clone https://github.com/Zaibatsu89/EmptyApp.git
   ```

2. **Open het project in Visual Studio 2022 of nieuwer**:
   Zorg ervoor dat je de .NET 8 SDK en de MAUI workload hebt geïnstalleerd.

3. **Configureer een SQLite-database**:
   Update `Constants.DatabasePath` met het pad naar je SQLite-databasebestand.

4. **Start de applicatie**:
   Build en run het project op je gewenste platform of emulator.

## Gebruiksaanwijzing

- **Navigatie**:
  Gebruik het Flyout-menu om te navigeren tussen Dashboard, Projecten en Meta-beheer.

- **Categorieën beheren**:
  Voeg nieuwe categorieën toe, bewerk bestaande of verwijder ze via de databasebeheerlaag in `CategoryRepository`.

- **Thema wijzigen**:
  Wissel tussen licht en donker thema met de themaschakelaar in het menu.

- **Meldingen**:
  Voeg eenvoudig Snackbar- of Toastmeldingen toe door respectievelijk `AppShell.DisplaySnackbarAsync` en `AppShell.DisplayToastAsync` aan te roepen.

## Toekomstige uitbreidingen

- Toevoegen van meer pagina’s en functionaliteiten.
- Integratie van een externe API.
- Uitbreiding van databasebeheermogelijkheden.

## Bijdragen

Bijdragen zijn van harte welkom! Open een issue of maak een pull request om verbeteringen of nieuwe functionaliteiten voor te stellen.

## Licentie

Dit project is gelicenseerd onder de MIT-licentie. Zie [LICENSE](LICENSE) voor meer informatie.

## Contact

Voor vragen of suggesties, neem contact op via [Zaibatsu89](https://github.com/Zaibatsu89).
