namespace Rocket.API.Commands
{
    public struct CommandArg
    {
        public CommandArg(int index, string arg)
        {
            RawValue = arg;
            Index = index;
        }
        public string RawValue { get; }
        public int Index { get; }
        public override string ToString() => RawValue;
    }
}
