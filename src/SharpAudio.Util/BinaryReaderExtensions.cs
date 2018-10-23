using System.IO;

namespace SharpAudio.Util
{
    internal static class BinaryReaderExtensions
    {
        public static string ReadFourCc(this BinaryReader reader, bool bigEndian = false)
        {
            var a = reader.ReadChar();
            var b = reader.ReadChar();
            var c = reader.ReadChar();
            var d = reader.ReadChar();

            return bigEndian
                ? new string(new[] { d, c, b, a })
                : new string(new[] { a, b, c, d });
        }
    }

    internal static class StreamExtensions
    {
        public static string ReadFourCc(this Stream reader, bool bigEndian = false)
        {
            var a = (char)reader.ReadByte();
            var b = (char)reader.ReadByte();
            var c = (char)reader.ReadByte();
            var d = (char)reader.ReadByte();

            return bigEndian
                ? new string(new[] { d, c, b, a })
                : new string(new[] { a, b, c, d });
        }
    }
}
