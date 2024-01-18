﻿using System.Windows.Input;

namespace ZamzamApp.Core;

public class RelayCommandBase : ICommand
{
    private readonly Action<object> _execute;
    private readonly Predicate<object> _canExecute;

    public RelayCommandBase(Action<Object> execute, Predicate<Object> canExecute)
    {
        _execute = execute;
        _canExecute = canExecute;
    }
    public event EventHandler CanExecuteChanged
    {
        add => CommandManager.RequerySuggested += value;
        remove => CommandManager.RequerySuggested -= value;
    }
    public bool CanExecute(object parameter) => _canExecute(parameter);
    public void Execute(object parameter)
    {
        _execute(parameter);
    }
}
