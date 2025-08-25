namespace MauiApp1.Services
{
    /// <summary>
    /// Dialog Service Interface.
    /// </summary>
    public interface IDialogService
    {
        /// <summary>
        /// Display an alert with a single OK button.
        /// </summary>
        /// <param name="title">Alert title.</param>
        /// <param name="message">Alert message.</param>
        /// <param name="cancel">Cancel button text.</param>
        /// <returns>Task.</returns>
        Task DisplayAlertAsync(string title, string message, string cancel = "OK");

        /// <summary>
        /// Display an alert with Yes/No buttons.
        /// </summary>
        /// <param name="title">Alert title.</param>
        /// <param name="message">Alert message.</param>
        /// <param name="accept">Accept button text.</param>
        /// <param name="cancel">Cancel button text.</param>
        /// <returns>True if user clicked accept, false otherwise.</returns>
        Task<bool> DisplayAlertAsync(string title, string message, string accept, string cancel);

        /// <summary>
        /// Display a confirmation dialog for destructive actions.
        /// </summary>
        /// <param name="title">Dialog title.</param>
        /// <param name="message">Dialog message.</param>
        /// <param name="itemName">Name of the item being deleted.</param>
        /// <returns>True if user confirmed deletion, false otherwise.</returns>
        Task<bool> DisplayDeleteConfirmationAsync(string title, string message, string itemName);
    }
}
