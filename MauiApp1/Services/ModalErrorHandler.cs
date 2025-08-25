namespace MauiApp1.Services
{
    /// <summary>
    /// Modal Error Handler.
    /// </summary>
    public class ModalErrorHandler : IErrorHandler
    {
        private readonly IDialogService _dialogService;

        public ModalErrorHandler(IDialogService dialogService)
        {
            _dialogService = dialogService;
        }

        /// <summary>
        /// Handle error in UI.
        /// </summary>
        /// <param name="ex">Exception.</param>
        public void HandleError(Exception ex)
        {
            DisplayAlert(ex).FireAndForgetSafeAsync();
        }

        async Task DisplayAlert(Exception ex)
        {
            await _dialogService.DisplayAlertAsync("Error", ex.Message, "OK");
        }
    }
}