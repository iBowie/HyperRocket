using Rocket.Core.Assets;
using System;
using Rocket.API;
using System.Collections.Generic;

namespace Rocket.Core.Serialization
{
    public sealed class RocketCommands : IDefaultable
    {
        public void LoadDefaults()
        {
            CommandMappings = new List<CommandMapping>();
        }

        public List<CommandMapping> CommandMappings = new List<CommandMapping>();
    }
}