using System;
using System.Collections.Generic;
using ZamzamCo.Controls;

namespace ZamzamCo.Dialogs
{
    public class DialogService : IDialogServices
    {
        static Dictionary<Type, Type> _mappings = new Dictionary<Type, Type>();
        public static void RegisterDialog<TView, TViewModel>()
        {
            _mappings.Add(typeof(TViewModel), typeof(TView));
        }
        public void ShowDialog<TViewModel>(Action<string> callBack)
        {
            var type = _mappings[typeof(TViewModel)];
            ShowDialogInternal(type, callBack);
        }
        public void ShowDialog(string name, Action<string> callBack)
        {
            var type = Type.GetType($"ZamzamCo.Controls.{name}");
            ShowDialogInternal(type, callBack);
        }

        private void ShowDialogInternal(Type type, Action<string> callBack)
        {
            var dialog = new DialogWindow();
            EventHandler closeEventHandler = (sender, e) => { };
            closeEventHandler = (sender, e) =>
            {
                callBack(dialog.DialogResult.ToString());
                dialog.Closed -= closeEventHandler;
            };
            dialog.Closed += closeEventHandler;
            dialog.Content = Activator.CreateInstance(type);
            dialog.ShowDialog();
        }
    }
}
