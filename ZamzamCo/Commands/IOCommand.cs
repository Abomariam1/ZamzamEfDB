using System;

namespace ZamzamCo.Commands
{
    public class IOCommand : CommandBase
    {
        private readonly Action _action = () => { };

        public IOCommand(Action action)
        {
            _action = action;
        }

        public override void Execute(object? parameter)
        {
            _action();
        }
    }
}
