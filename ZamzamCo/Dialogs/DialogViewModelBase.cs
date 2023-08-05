namespace ZamzamCo.Dialogs
{
    public abstract class DialogViewModelBase<T>
    {
        public string Title { get; set; }
        public string Message { get; set; }
        public T DialogResult { get; set; }
        protected DialogViewModelBase(string title, string message)
        {
            Title = title;
            Message = message;
        }

        public void CloseDialogWithResult(IDialogWindow dialogWindow, T value)
        {
            DialogResult = value;
            if (dialogWindow != null)
            {
                dialogWindow.DialogResult = true;
            }
        }
    }
}
