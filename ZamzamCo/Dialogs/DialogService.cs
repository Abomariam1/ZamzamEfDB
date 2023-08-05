using ZamzamCo.Controls;

namespace ZamzamCo.Dialogs
{
    public class DialogService : IDialogServices
    {
        public void ShowDialog()
        {
            var dialog = new DialogWindow();
            dialog.ShowDialog();
        }
    }
}
