namespace ZamzamCo.Dialogs
{
    public interface IDialogWindow
    {
        bool? DialogResult { get; set; }
        object Datacontext { get; set; }

        bool? ShowDialog();
    }
}
