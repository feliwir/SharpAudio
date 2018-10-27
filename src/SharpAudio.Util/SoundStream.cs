using SharpAudio.Util.Mp3;
using SharpAudio.Util.Wave;
using System;
using System.IO;
using System.Linq;

namespace SharpAudio.Util
{
    public class SoundStream : IDisposable
    {
        private Decoder _decoder;

        private static byte[] MakeFourCC(string magic)
        {
            return new[] {  (byte)magic[0],
                            (byte)magic[1],
                            (byte)magic[2],
                            (byte)magic[3]};
        }

        /// <summary>
        /// The audio format of this stream
        /// </summary>
        public AudioFormat Format => _decoder.Format;

        public bool IsFinished => _decoder.IsFinished;

        /// <summary>
        /// Initializes a new instance of the <see cref="SoundStream"/> class.
        /// </summary>
        /// <param name="stream">The sound stream.</param>
        public SoundStream(Stream stream)
        {
            if (stream == null)
                throw new ArgumentNullException("Stream cannot be null!");
;
            var fourcc = stream.ReadFourCc();
            stream.Seek(0, SeekOrigin.Begin);

            if(fourcc.SequenceEqual(MakeFourCC("RIFF")))
            {
                _decoder = new WaveDecoder(stream);
            }
            else if(fourcc.SequenceEqual(MakeFourCC("ID3\u0003")) ||
                    fourcc.SequenceEqual(new byte[] { 0xFF, 0xFB, 0xE0, 0x64}))
            {
                _decoder = new Mp3Decoder(stream);
            }
            else
            {
                throw new InvalidDataException("Unknown format: " + fourcc);
            }
        }

        /// <summary>
        /// Real all samples from this stream
        /// </summary>
        /// <returns></returns>
        public byte[] ReadAll()
        {
            _decoder.GetSamples(out byte[] data);

            return data;
        }

        /// <summary>
        /// Read a specific amount of samples
        /// </summary>
        /// <param name="numSamples">The amount of samples</param>
        /// <returns></returns>
        public byte[] ReadSamples(int numSamples)
        {
            _decoder.GetSamples(numSamples, out byte[] data);

            return data;
        }

        /// <summary>
        /// Read a specific duration of samples
        /// </summary>
        /// <param name="span">The timespan</param>
        /// <returns></returns>
        public byte[] ReadSamples(TimeSpan span)
        {
            int numSamples = span.Seconds * Format.SampleRate * Format.Channels;

            _decoder.GetSamples(numSamples, out byte[] data);

            return data;
        }

        public void Dispose()
        {
            _decoder.Dispose();
        }
    }
}
