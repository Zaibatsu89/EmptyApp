# Dialog Implementation Documentation

## Overzicht

Deze implementatie voegt asynchrone pop-upvensters toe aan de MAUI applicatie om de gebruikerservaring te verbeteren. De implementatie maakt gebruik van een abstractie laag (`IDialogService`) om de afhankelijkheid van de UI-laag in de viewmodels te verminderen en de testbaarheid te verhogen.

## Architectuur

### IDialogService Interface

De `IDialogService` interface definieert drie hoofdfuncties:

1. **DisplayAlertAsync(string title, string message, string cancel = "OK")**
   - Toont een eenvoudige alert met één OK knop
   - Gebruikt voor informatieve berichten

2. **DisplayAlertAsync(string title, string message, string accept, string cancel)**
   - Toont een alert met twee knoppen (bijv. Ja/Nee)
   - Retourneert een boolean die aangeeft welke knop is ingedrukt

3. **DisplayDeleteConfirmationAsync(string title, string message, string itemName)**
   - Speciale methode voor bevestiging van destructieve acties
   - Voegt automatisch waarschuwing toe dat de actie niet ongedaan kan worden gemaakt

### DialogService Implementatie

De `DialogService` implementeert de interface en gebruikt `Shell.Current.DisplayAlertAsync` voor het tonen van dialogen. De service gebruikt een `SemaphoreSlim` om te voorkomen dat meerdere dialogen tegelijkertijd worden getoond.

## Gebruik in PageModels

### Injectie van de Service

```csharp
public class ExamplePageModel : ObservableObject
{
    private readonly IDialogService _dialogService;

    public ExamplePageModel(IDialogService dialogService)
    {
        _dialogService = dialogService;
    }
}
```

### Voorbeelden van Gebruik

#### Eenvoudige Informatie Alert
```csharp
await _dialogService.DisplayAlertAsync("Success", "Item saved successfully!", "OK");
```

#### Bevestiging voor Destructieve Acties
```csharp
var confirmed = await _dialogService.DisplayDeleteConfirmationAsync(
    "Delete Item", 
    "Are you sure you want to delete this item?", 
    item.Name);

if (!confirmed)
    return;

// Voer de verwijdering uit
```

#### Ja/Nee Bevestiging
```csharp
var confirmed = await _dialogService.DisplayAlertAsync(
    "Clean Tasks", 
    "Are you sure you want to clean all completed tasks?", 
    "Clean", "Cancel");

if (!confirmed)
    return;

// Voer de actie uit
```

## Geïmplementeerde Locaties

### TaskDetailPageModel
- **Delete**: Bevestiging voor het verwijderen van een taak
- **Save**: Succesmelding na het opslaan van een taak

### ProjectDetailPageModel
- **Delete**: Bevestiging voor het verwijderen van een project (inclusief waarschuwing voor bijbehorende taken)
- **Save**: Succesmelding na het opslaan van een project
- **CleanTasks**: Bevestiging voor het opruimen van voltooide taken

### MainPageModel
- **CleanTasks**: Bevestiging voor het opruimen van voltooide taken

### ManageMetaPageModel
- **DeleteCategory**: Bevestiging voor het verwijderen van een categorie
- **DeleteTag**: Bevestiging voor het verwijderen van een tag
- **SaveCategories**: Succesmelding na het opslaan van categorieën
- **SaveTags**: Succesmelding na het opslaan van tags
- **AddCategory**: Succesmelding na het toevoegen van een categorie
- **AddTag**: Succesmelding na het toevoegen van een tag
- **Reset**: Bevestiging voor het resetten van de applicatie

## Registratie in Dependency Injection

De services worden geregistreerd in `MauiProgram.cs`:

```csharp
builder.Services.AddSingleton<IDialogService, DialogService>();
```

## Voordelen van deze Implementatie

1. **Testbaarheid**: Door de abstractie kunnen viewmodels getest worden zonder afhankelijkheid van de UI
2. **Herbruikbaarheid**: De service kan overal in de applicatie worden gebruikt
3. **Consistentie**: Alle dialogen volgen dezelfde patronen en stijl
4. **Thread Safety**: SemaphoreSlim voorkomt gelijktijdige dialogen
5. **Platform Onafhankelijkheid**: Werkt op alle ondersteunde platforms (Android, iOS, Windows)

## Best Practices

1. **Gebruik altijd await**: Zorg ervoor dat alle aanroepen naar `DisplayAlertAsync` worden afgehandeld met `await`
2. **Duidelijke titels**: Gebruik beschrijvende titels voor dialogen
3. **Informatieve berichten**: Geef de gebruiker voldoende informatie om een beslissing te maken
4. **Destructieve acties**: Gebruik altijd bevestiging voor acties die data kunnen verliezen
5. **Succesmeldingen**: Toon bevestiging na succesvolle operaties

## Toekomstige Uitbreidingen

De implementatie kan eenvoudig worden uitgebreid met:
- Custom dialog templates
- Progress indicators
- Input dialogs
- File picker dialogs
- Platform-specifieke styling
