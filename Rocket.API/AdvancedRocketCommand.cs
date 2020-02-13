using Rocket.API.Commands;
using System.Collections.Generic;

namespace Rocket.API
{
    public abstract class AdvancedRocketCommand : IRocketCommand
    {
        public abstract AllowedCaller AllowedCaller { get; }
        public abstract string Name { get; }
        public abstract string Help { get; }
        public abstract string Syntax { get; }
        public virtual List<string> Aliases => new List<string>();
        public abstract List<string> Permissions { get; }
        public abstract void Execute(IRocketPlayer caller, CommandArgs args);
        public void Execute(IRocketPlayer caller, string[] command)
        {
            Execute(caller, new CommandArgs(command));
        }
    }
}
