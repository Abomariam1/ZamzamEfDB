using System;

namespace ZamzamCo.Commands.CrudCommands
{
    public class CrudComand : CommandBase
    {
        private readonly Action _action = () => { };

        public CrudComand(Action action)
        {
            _action = action;
        }
        public override void Execute(object? parameter)
        {
            _action();
        }
    }
}
