using System.Collections.ObjectModel;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using MauiApp1.Data;
using MauiApp1.Models;
using MauiApp1.Services;

namespace MauiApp1.PageModels
{
    public partial class ManageMetaPageModel : ObservableObject
    {
        private readonly CategoryRepository _categoryRepository;
        private readonly TagRepository _tagRepository;
        private readonly SeedDataService _seedDataService;
        private readonly IDialogService _dialogService;

        private ObservableCollection<Category> _categories = [];
        private ObservableCollection<Tag> _tags = [];

        public ObservableCollection<Category> Categories
        {
            get => _categories;
            set => SetProperty(ref _categories, value);
        }

        public ObservableCollection<Tag> Tags
        {
            get => _tags;
            set => SetProperty(ref _tags, value);
        }

        public ManageMetaPageModel(CategoryRepository categoryRepository, TagRepository tagRepository, SeedDataService seedDataService, IDialogService dialogService)
        {
            _categoryRepository = categoryRepository;
            _tagRepository = tagRepository;
            _seedDataService = seedDataService;
            _dialogService = dialogService;
        }

        private async Task LoadData()
        {
            var categoriesList = await _categoryRepository.ListAsync();
            Categories = new ObservableCollection<Category>(categoriesList);
            var tagsList = await _tagRepository.ListAsync();
            Tags = new ObservableCollection<Tag>(tagsList);
        }

        [RelayCommand]
        private Task Appearing()
            => LoadData();

        [RelayCommand]
        private async Task SaveCategories()
        {
            foreach (var category in Categories)
            {
                await _categoryRepository.SaveItemAsync(category);
            }

            await _dialogService.DisplayAlertAsync("Success", "Categories saved successfully!", "OK");
        }

        [RelayCommand]
        private async Task DeleteCategory(Category category)
        {
            var confirmed = await _dialogService.DisplayDeleteConfirmationAsync(
                "Delete Category", 
                "Are you sure you want to delete this category?", 
                category.Title);

            if (!confirmed)
                return;

            Categories.Remove(category);
            await _categoryRepository.DeleteItemAsync(category);
            await _dialogService.DisplayAlertAsync("Success", "Category deleted successfully!", "OK");
        }

        [RelayCommand]
        private async Task AddCategory()
        {
            var category = new Category();
            Categories.Add(category);
            await _categoryRepository.SaveItemAsync(category);
            await _dialogService.DisplayAlertAsync("Success", "Category added successfully!", "OK");
        }

        [RelayCommand]
        private async Task SaveTags()
        {
            foreach (var tag in Tags)
            {
                await _tagRepository.SaveItemAsync(tag);
            }

            await _dialogService.DisplayAlertAsync("Success", "Tags saved successfully!", "OK");
        }

        [RelayCommand]
        private async Task DeleteTag(Tag tag)
        {
            var confirmed = await _dialogService.DisplayDeleteConfirmationAsync(
                "Delete Tag", 
                "Are you sure you want to delete this tag?", 
                tag.Title);

            if (!confirmed)
                return;

            Tags.Remove(tag);
            await _tagRepository.DeleteItemAsync(tag);
            await _dialogService.DisplayAlertAsync("Success", "Tag deleted successfully!", "OK");
        }

        [RelayCommand]
        private async Task AddTag()
        {
            var tag = new Tag();
            Tags.Add(tag);
            await _tagRepository.SaveItemAsync(tag);
            await _dialogService.DisplayAlertAsync("Success", "Tag added successfully!", "OK");
        }

        [RelayCommand]
        private async Task Reset()
        {
            var confirmed = await _dialogService.DisplayAlertAsync(
                "Reset Application", 
                "Are you sure you want to reset the application? This will delete all your data and restore the default seed data.", 
                "Reset", "Cancel");

            if (!confirmed)
                return;

            Preferences.Default.Remove("is_seeded");
            await _seedDataService.LoadSeedDataAsync();
            Preferences.Default.Set("is_seeded", true);
            await _dialogService.DisplayAlertAsync("Success", "Application reset successfully! Default data has been restored.", "OK");
            await Shell.Current.GoToAsync("//main");
        }
    }
}
