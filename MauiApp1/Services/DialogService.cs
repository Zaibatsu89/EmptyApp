namespace MauiApp1.Services
{
    /// <summary>
    /// Dialog Service Implementation.
    /// </summary>
    public class DialogService : IDialogService
    {
        private readonly SemaphoreSlim _semaphore = new(1, 1);

        /// <summary>
        /// Display an alert with a single OK button.
        /// </summary>
        /// <param name="title">Alert title.</param>
        /// <param name="message">Alert message.</param>
        /// <param name="cancel">Cancel button text.</param>
        /// <returns>Task.</returns>
        public async Task DisplayAlertAsync(string title, string message, string cancel = "OK")
        {
            try
            {
                await _semaphore.WaitAsync();
                if (Shell.Current is Shell shell)
                    await shell.DisplayAlertAsync(title, message, cancel);
            }
            finally
            {
                _semaphore.Release();
            }
        }

        /// <summary>
        /// Display an alert with Yes/No buttons.
        /// </summary>
        /// <param name="title">Alert title.</param>
        /// <param name="message">Alert message.</param>
        /// <param name="accept">Accept button text.</param>
        /// <param name="cancel">Cancel button text.</param>
        /// <returns>True if user clicked accept, false otherwise.</returns>
        public async Task<bool> DisplayAlertAsync(string title, string message, string accept, string cancel)
        {
            try
            {
                await _semaphore.WaitAsync();
                if (Shell.Current is Shell shell)
                    return await shell.DisplayAlertAsync(title, message, accept, cancel);
                return false;
            }
            finally
            {
                _semaphore.Release();
            }
        }

        /// <summary>
        /// Display a confirmation dialog for destructive actions.
        /// </summary>
        /// <param name="title">Dialog title.</param>
        /// <param name="message">Dialog message.</param>
        /// <param name="itemName">Name of the item being deleted.</param>
        /// <returns>True if user confirmed deletion, false otherwise.</returns>
        public async Task<bool> DisplayDeleteConfirmationAsync(string title, string message, string itemName)
        {
            var fullMessage = $"{message}\n\nItem: {itemName}\n\nThis action cannot be undone.";
            return await DisplayAlertAsync(title, fullMessage, "Delete", "Cancel");
        }
    }
}
