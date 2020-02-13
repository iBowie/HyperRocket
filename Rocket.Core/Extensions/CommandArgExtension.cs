using Rocket.API.Commands;
using System;
using System.Globalization;

namespace Rocket.Core.Extensions
{
    /// <summary>
    /// Adds essential type auto conversion
    /// </summary>
    public static class CommandArgExtension
    {
        private static readonly CultureInfo Culture = CultureInfo.InvariantCulture;
        /// <param name="value">Parsed value</param>
        /// <returns>Parsed</returns>
        public static bool IsBoolean(this CommandArg arg, out bool value)
        {
            return bool.TryParse(arg.RawValue, out value);
        }
        /// <param name="value">Parsed value</param>
        /// <returns>Parsed</returns>
        public static bool IsByte(this CommandArg arg, out byte value)
        {
            return byte.TryParse(arg.RawValue, NumberStyles.Integer | NumberStyles.HexNumber, Culture, out value);
        }
        /// <param name="value">Parsed value</param>
        /// <returns>Parsed</returns>
        public static bool IsSByte(this CommandArg arg, out sbyte value)
        {
            return sbyte.TryParse(arg.RawValue, NumberStyles.Integer | NumberStyles.HexNumber, Culture, out value);
        }
        /// <param name="value">Parsed value</param>
        /// <returns>Parsed</returns>
        public static bool IsInt16(this CommandArg arg, out short value)
        {
            return short.TryParse(arg.RawValue, NumberStyles.Integer, Culture, out value);
        }
        /// <param name="value">Parsed value</param>
        /// <returns>Parsed</returns>
        public static bool IsInt32(this CommandArg arg, out int value)
        {
            return int.TryParse(arg.RawValue, NumberStyles.Integer, Culture, out value);
        }
        /// <param name="value">Parsed value</param>
        /// <returns>Parsed</returns>
        public static bool IsInt64(this CommandArg arg, out long value)
        {
            return long.TryParse(arg.RawValue, NumberStyles.Integer, Culture, out value);
        }
        /// <param name="value">Parsed value</param>
        /// <returns>Parsed</returns>
        public static bool IsUInt16(this CommandArg arg, out ushort value)
        {
            return ushort.TryParse(arg.RawValue, NumberStyles.Integer, Culture, out value);
        }
        /// <param name="value">Parsed value</param>
        /// <returns>Parsed</returns>
        public static bool IsUInt32(this CommandArg arg, out uint value)
        {
            return uint.TryParse(arg.RawValue, NumberStyles.Integer, Culture, out value);
        }
        /// <param name="value">Parsed value</param>
        /// <returns>Parsed</returns>
        public static bool IsUInt64(this CommandArg arg, out ulong value)
        {
            return ulong.TryParse(arg.RawValue, NumberStyles.Integer, Culture, out value);
        }
        /// <param name="value">Parsed value</param>
        /// <returns>Parsed</returns>
        public static bool IsSingle(this CommandArg arg, out float value)
        {
            return float.TryParse(arg.RawValue, NumberStyles.Number, Culture, out value);
        }
        /// <param name="value">Parsed value</param>
        /// <returns>Parsed</returns>
        public static bool IsDouble(this CommandArg arg, out double value)
        {
            return double.TryParse(arg.RawValue, NumberStyles.Number, Culture, out value);
        }
        /// <param name="value">Parsed value</param>
        /// <returns>Parsed</returns>
        public static bool IsDecimal(this CommandArg arg, out decimal value)
        {
            return decimal.TryParse(arg.RawValue, NumberStyles.Number, Culture, out value);
        }
        /// <param name="value">Parsed value</param>
        /// <returns>Parsed</returns>
        public static bool IsDateTime(this CommandArg arg, out DateTime value)
        {
            return DateTime.TryParse(arg.RawValue, Culture, DateTimeStyles.None, out value);
        }
        /// <param name="value">Parsed value</param>
        /// <returns>Parsed</returns>
        public static bool IsTimeSpan(this CommandArg arg, out TimeSpan value)
        {
            return TimeSpan.TryParse(arg.RawValue, out value);
        }
        /// <param name="value">Parsed value</param>
        /// <returns>Parsed</returns>
        /// <exception cref="ArgumentException"/>
        public static bool IsEnum<T>(this CommandArg arg, out T value) where T : struct, IConvertible
        {
            if (!typeof(T).IsEnum)
                throw new ArgumentException($"{typeof(T).Name} is not an enum");
            var isDefined = arg.RawValue == null ? false : Enum.IsDefined(typeof(T), arg.RawValue);
            value = isDefined ? (T)Enum.Parse(typeof(T), arg.RawValue) : default;
            return isDefined;
        }
    }
}
