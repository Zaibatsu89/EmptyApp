using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using MauiApp1.Models;

namespace MauiApp1.PageModels
{
    public partial class ProjectDetailPageModel : ObservableObject, IQueryAttributable, IProjectTaskPageModel
    {
        private Project? _project;
        private readonly ProjectRepository _projectRepository;
        private readonly TaskRepository _taskRepository;
        private readonly CategoryRepository _categoryRepository;
        private readonly TagRepository _tagRepository;
        private readonly ModalErrorHandler _errorHandler;
        private readonly IDialogService _dialogService;

        private string _name = string.Empty;
        private string _description = string.Empty;
        private List<ProjectTask> _tasks = [];
        private List<Category> _categories = [];
        private Category? _category;
        private int _categoryIndex = -1;
        private List<Tag> _allTags = [];
        private IconData _icon;
        private bool _isBusy;
        private List<IconData> _icons = new List<IconData>
        {
            new IconData { Icon = FluentUI.ribbon_24_regular, Description = "Ribbon Icon" },
            new IconData { Icon = FluentUI.ribbon_star_24_regular, Description = "Ribbon Star Icon" },
            new IconData { Icon = FluentUI.trophy_24_regular, Description = "Trophy Icon" },
            new IconData { Icon = FluentUI.badge_24_regular, Description = "Badge Icon" },
            new IconData { Icon = FluentUI.book_24_regular, Description = "Book Icon" },
            new IconData { Icon = FluentUI.people_24_regular, Description = "People Icon" },
            new IconData { Icon = FluentUI.bot_24_regular, Description = "Bot Icon" }
        };

        public string Name
        {
            get => _name;
            set => SetProperty(ref _name, value);
        }

        public string Description
        {
            get => _description;
            set => SetProperty(ref _description, value);
        }

        public List<ProjectTask> Tasks
        {
            get => _tasks;
            set => SetProperty(ref _tasks, value);
        }

        public List<Category> Categories
        {
            get => _categories;
            set => SetProperty(ref _categories, value);
        }

        public Category? Category
        {
            get => _category;
            set => SetProperty(ref _category, value);
        }

        public int CategoryIndex
        {
            get => _categoryIndex;
            set => SetProperty(ref _categoryIndex, value);
        }

        public List<Tag> AllTags
        {
            get => _allTags;
            set => SetProperty(ref _allTags, value);
        }

        public IconData Icon
        {
            get => _icon;
            set => SetProperty(ref _icon, value);
        }

        public bool IsBusy
        {
            get => _isBusy;
            set => SetProperty(ref _isBusy, value);
        }

        public List<IconData> Icons
        {
            get => _icons;
            set => SetProperty(ref _icons, value);
        }

        public bool HasCompletedTasks
            => _project?.Tasks.Any(t => t.IsCompleted) ?? false;

        public ProjectDetailPageModel(ProjectRepository projectRepository, TaskRepository taskRepository, CategoryRepository categoryRepository, TagRepository tagRepository, ModalErrorHandler errorHandler, IDialogService dialogService)
        {
            _projectRepository = projectRepository;
            _taskRepository = taskRepository;
            _categoryRepository = categoryRepository;
            _tagRepository = tagRepository;
            _errorHandler = errorHandler;
            _dialogService = dialogService;
            _icon = _icons.First();
            Tasks = [];
        }

        public void ApplyQueryAttributes(IDictionary<string, object> query)
        {
            if (query.ContainsKey("id"))
            {
                int id = Convert.ToInt32(query["id"]);
                LoadData(id).FireAndForgetSafeAsync(_errorHandler);
            }
            else if (query.ContainsKey("refresh"))
            {
                RefreshData().FireAndForgetSafeAsync(_errorHandler);
            }
            else
            {
                Task.WhenAll(LoadCategories(), LoadTags()).FireAndForgetSafeAsync(_errorHandler);
                _project = new();
                _project.Tags = [];
                _project.Tasks = [];
                Tasks = _project.Tasks;
            }
        }

        private async Task LoadCategories() =>
            Categories = await _categoryRepository.ListAsync();

        private async Task LoadTags() =>
            AllTags = await _tagRepository.ListAsync();

        private async Task RefreshData()
        {
            if (_project.IsNullOrNew())
            {
                if (_project is not null)
                    Tasks = new(_project.Tasks);

                return;
            }

            Tasks = await _taskRepository.ListAsync(_project.ID);
            _project.Tasks = Tasks;
        }

        private async Task LoadData(int id)
        {
            try
            {
                IsBusy = true;

                _project = await _projectRepository.GetAsync(id);

                if (_project.IsNullOrNew())
                {
                    _errorHandler.HandleError(new Exception($"Project with id {id} could not be found."));
                    return;
                }

                Name = _project.Name;
                Description = _project.Description;
                Tasks = _project.Tasks;

                Icon.Icon = _project.Icon;

                Categories = await _categoryRepository.ListAsync();
                Category = Categories?.FirstOrDefault(c => c.ID == _project.CategoryID);
                CategoryIndex = Categories?.FindIndex(c => c.ID == _project.CategoryID) ?? -1;

                var allTags = await _tagRepository.ListAsync();
                foreach (var tag in allTags)
                {
                    tag.IsSelected = _project.Tags.Any(t => t.ID == tag.ID);
                }
                AllTags = new(allTags);
            }
            catch (Exception e)
            {
                _errorHandler.HandleError(e);
            }
            finally
            {
                IsBusy = false;
                OnPropertyChanged(nameof(HasCompletedTasks));
            }
        }

        [RelayCommand]
        private async Task TaskCompleted(ProjectTask task)
        {
            await _taskRepository.SaveItemAsync(task);
            OnPropertyChanged(nameof(HasCompletedTasks));
        }


        [RelayCommand]
        private async Task Save()
        {
            if (_project is null)
            {
                _errorHandler.HandleError(
                    new Exception("Project is null. Cannot Save."));

                return;
            }

            _project.Name = Name;
            _project.Description = Description;
            _project.CategoryID = Category?.ID ?? 0;
            _project.Icon = Icon.Icon ?? FluentUI.ribbon_24_regular;
            await _projectRepository.SaveItemAsync(_project);

            if (_project.IsNullOrNew())
            {
                foreach (var tag in AllTags)
                {
                    if (tag.IsSelected)
                    {
                        await _tagRepository.SaveItemAsync(tag, _project.ID);
                    }
                }
            }

            foreach (var task in _project.Tasks)
            {
                if (task.ID == 0)
                {
                    task.ProjectID = _project.ID;
                    await _taskRepository.SaveItemAsync(task);
                }
            }

            await Shell.Current.GoToAsync("..");
            await _dialogService.DisplayAlertAsync("Success", "Project saved successfully!", "OK");
        }

        [RelayCommand]
        private async Task AddTask()
        {
            if (_project is null)
            {
                _errorHandler.HandleError(
                    new Exception("Project is null. Cannot navigate to task."));

                return;
            }

            // Pass the project so if this is a new project we can just add
            // the tasks to the project and then save them all from here.
            await Shell.Current.GoToAsync($"task",
                new ShellNavigationQueryParameters(){
                    {TaskDetailPageModel.ProjectQueryKey, _project}
                });
        }

        [RelayCommand]
        private async Task Delete()
        {
            if (_project.IsNullOrNew())
            {
                await Shell.Current.GoToAsync("..");
                return;
            }

            var confirmed = await _dialogService.DisplayDeleteConfirmationAsync(
                "Delete Project", 
                "Are you sure you want to delete this project? This will also delete all associated tasks.", 
                _project.Name);

            if (!confirmed)
                return;

            await _projectRepository.DeleteItemAsync(_project);
            await Shell.Current.GoToAsync("..");
            await _dialogService.DisplayAlertAsync("Success", "Project deleted successfully!", "OK");
        }

        [RelayCommand]
        private Task NavigateToTask(ProjectTask task) =>
            Shell.Current.GoToAsync($"task?id={task.ID}");

        [RelayCommand]
        private async Task ToggleTag(Tag tag)
        {
            tag.IsSelected = !tag.IsSelected;

            if (!_project.IsNullOrNew())
            {
                if (tag.IsSelected)
                {
                    await _tagRepository.SaveItemAsync(tag, _project.ID);
                }
                else
                {
                    await _tagRepository.DeleteItemAsync(tag, _project.ID);
                }
            }

            AllTags = new(AllTags);
        }

        [RelayCommand]
        private async Task CleanTasks()
        {
            var completedTasks = Tasks.Where(t => t.IsCompleted).ToArray();
            
            if (completedTasks.Length == 0)
            {
                await _dialogService.DisplayAlertAsync("No Tasks", "There are no completed tasks to clean up.", "OK");
                return;
            }

            var confirmed = await _dialogService.DisplayAlertAsync(
                "Clean Completed Tasks", 
                $"Are you sure you want to delete {completedTasks.Length} completed task(s)?", 
                "Clean", "Cancel");

            if (!confirmed)
                return;

            foreach (var task in completedTasks)
            {
                await _taskRepository.DeleteItemAsync(task);
                Tasks.Remove(task);
            }

            Tasks = new(Tasks);
            OnPropertyChanged(nameof(HasCompletedTasks));
            await _dialogService.DisplayAlertAsync("Success", $"Cleaned up {completedTasks.Length} completed task(s)!", "OK");
        }
    }
}
