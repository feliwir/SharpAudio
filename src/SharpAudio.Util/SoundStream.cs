using SharpAudio.Util.Mp3;
using SharpAudio.Util.Wave;
using System;
using System.IO;

namespace SharpAudio.Util
{
    public class SoundStream : IDisposable
    {
        private Decoder _decoder;

        public AudioFormat Format => _decoder.Format; 

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

            switch(fourcc)
            {
                case "RIFF":
                    _decoder = new WaveDecoder(stream);
                    break;
                case "ID3\u0003":
                    _decoder = new Mp3Decoder(stream);
                    break;
                default:
                    throw new InvalidDataException("Unknown format: " + fourcc);
            }
        }

        public byte[] ReadAll()
        {
            _decoder.GetSamples(out byte[] data);

            return data;
        }

        public byte[] ReadSamples(int numSamples)
        {
            _decoder.GetSamples(numSamples, out byte[] data);

            return data;
        }

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
