using System;

namespace Danske.Service.Services.Adapters
{
    internal static class CacheKeyHelper
    {
        public static string ArrayHash(int[] values)
        {
            return BitConverter.ToString(BitConverter.GetBytes(ArrayHashCode(values))).Replace("-", "").ToLowerInvariant();
        }

        private static int ArrayHashCode(int[] values)
        {
            if (values == null || values.Length == 0)
                return 0;

            var value = values[0] == 0 ? -1 : values[0];
            var hashCode = value.GetHashCode();
            for (var i = 1; i < values.Length; i++)
            {
                value = values[i] == 0 ? -1 : values[i];
                unchecked
                {
                    hashCode = (hashCode * 397) ^ value.GetHashCode();
                }
            }
            return hashCode;
        }
    }
}
