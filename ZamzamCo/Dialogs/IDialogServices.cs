using System;

namespace ZamzamCo.Dialogs
{
    public interface IDialogServices
    {
        void ShowDialog(string name, Action<string> callBack);
        void ShowDialog<ViewModel>(Action<string> callBack);
    }
}
