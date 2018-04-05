﻿using System;
using Newtonsoft.Json.Linq;
using Rocket.API.Configuration;

namespace Rocket.Core.Configuration.Json
{
    public class JsonConfigurationSection : JsonConfiguration, IConfigurationSection
    {
        public JsonConfigurationSection(JToken token) : base(token) { }

        public string Key => (Node as JProperty)?.Name;

        public string Path => Node.Path;

        public string Value
        {
            get => Node.Value<string>();
            set
            {
                if (!(Node is JProperty)) throw new Exception("Can not set value of: " + Node.Path);

                ((JProperty) Node).Value = new JValue(value);
            }
        }

        public T Get<T>() => Node.ToObject<T>();

        public bool TryGet<T>(out T value)
        {
            value = default(T);
            try
            {
                value = Get<T>();
                return true;
            }
            catch
            {
                return false;
            }
        }
    }
}