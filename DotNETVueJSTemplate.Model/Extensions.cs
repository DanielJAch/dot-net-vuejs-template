using System;

namespace DotNETVueJSTemplate.Model
{
    public static class Extensions
    {
        /// <summary>
        /// Converts date to UTC if necessary
        /// </summary>
        public static DateTime ToUtc(this DateTime value)
        {
            if (value.Kind == DateTimeKind.Utc)
                return value;

            if (value.Kind == DateTimeKind.Local)
                return value.ToUniversalTime();

            return DateTime.SpecifyKind(value, DateTimeKind.Utc);
        }

        /// <summary>
        /// Converts nullable date to UTC if necessary
        /// </summary>
        public static DateTime? ToUtc(this DateTime? value)
        {
            if (value.HasValue)
            {
                return value.Value.ToUtc();
            }

            return null;
        }
    }
}
