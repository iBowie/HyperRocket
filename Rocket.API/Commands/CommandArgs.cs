using System.Linq;

namespace Rocket.API.Commands
{
    public struct CommandArgs
    {
        public CommandArgs(string[] raw)
        {
            RawArgs = raw;
            Args = new CommandArg[raw.Length];
            for (int i = 0; i < Args.Length; i++)
            {
                Args[i] = new CommandArg(i, raw[i]);
            }
        }
        public string[] RawArgs { get; }
        public CommandArg[] Args { get; }
        public int Count => RawArgs.Length;
        public bool IsEmpty => Count == 0;
        public CommandArg this[int index] => Args[index];
        public string Join(int initialIndex)
        {
            return string.Join(" ", Args.Skip(initialIndex)
                .Select(arg => arg.ToString())
                .ToArray());
        }
        public string Join(int startIndex, int endIndex, string separator)
        {
            return string.Join(separator, Args.Skip(startIndex).Take(endIndex - startIndex)
                .Select(arg => arg.ToString())
                .ToArray());
        }
        public CommandArg JoinArg(int initialIndex) => new CommandArg(initialIndex, Join(initialIndex));
        public CommandArg JoinArg(int startIndex, int endIndex, string separator) => new CommandArg(startIndex, Join(startIndex, endIndex, separator));
        public override string ToString()
        {
            return $"[{string.Join(", ", Args.Select(a => a.ToString()).ToArray())}]";
        }
    }
}
