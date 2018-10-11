using System.IO;

namespace SharpAudio.Util.Wave
{
    internal class PcmParser : WavParser
    {
        public override byte[] Parse(BinaryReader reader, int size, WaveFormat format)
        {
            return reader.ReadBytes(size);
        }
    }
}
