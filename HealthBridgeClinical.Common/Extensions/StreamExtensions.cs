using Newtonsoft.Json;
using System;
using System.IO;
using System.Threading.Tasks;

namespace HealthBridgeClinical.Common.Extensions
{
    public static class StreamExtensions
    {
        public static Stream Reset(this Stream stream)
        {
            if (stream == null)
            {
                new ArgumentNullException(nameof(stream));
            }

            if (stream.CanSeek && stream.Position > 0)
            {
                stream.Seek(0, SeekOrigin.Begin);
            }

            return stream;
        }

        public static async Task<string> AsStringAsync(this Stream stream)
        {
            if (stream == null)
            {
                new ArgumentNullException(nameof(stream));
            }

            try
            {
                var reader = new StreamReader(stream);
                return await reader.ReadToEndAsync();
            }
            finally
            {
                stream.Reset();
            }
        }

        public static async Task<T> AsObjectAsync<T>(this Stream stream)
        {
            if (stream == null)
            {
                new ArgumentNullException(nameof(stream));
            }

            var asString = await stream.AsStringAsync();
            return JsonConvert.DeserializeObject<T>(asString);
        }
    }
}
