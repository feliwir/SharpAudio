using System.IO;

namespace SharpAudio.Codec
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
                ? new string(new[] {d, c, b, a})
                : new string(new[] {a, b, c, d});
        }
    }

    internal static class StreamExtensions
    {
        public static byte[] ReadFourCc(this Stream reader, bool bigEndian = false)
        {
            var a = (byte) reader.ReadByte();
            var b = (byte) reader.ReadByte();
            var c = (byte) reader.ReadByte();
            var d = (byte) reader.ReadByte();

            return new[] {a, b, c, d};
        }
    }
}