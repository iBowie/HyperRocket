﻿using System;
using Rocket.API.Commands;
using Rocket.Core.Exceptions;

namespace Rocket.Core.Permissions {
    public class NotEnoughPermissionsException : Exception, IFriendlyException
    {
        public ICommandCaller Caller { get; }
        public string[] Permissions { get; }

        public NotEnoughPermissionsException(ICommandCaller caller, string[] permissions)
        {
            Caller = caller;
            Permissions = permissions;
        }

        public override string Message
        {
            get
            {
                string message = $"{Caller.Name} does not have the following permissions: ";
                message += Environment.NewLine;
                foreach (var perm in Permissions)
                {
                    message += "* " + perm + Environment.NewLine;
                }

                return message;
            }
        }

        public NotEnoughPermissionsException(ICommandCaller caller, string permission) : this(caller,
            new[] { permission })
        {

        }

        public string ToFriendlyString(ICommandContext context)
        {
            return "Not enough permissions.";
        }
    }
}