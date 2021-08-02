using System.IO;

namespace Toolsed.Shared
{
    public static class StreamExtensions
    {
        public static byte[] ToByteArray(this Stream input)
        {
            using var memoryStream = new MemoryStream();
            input.CopyTo(memoryStream);
            return memoryStream.ToArray();
        }
    }
}
