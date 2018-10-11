using SharpAudio.Util.Wave;
using System;
using System.IO;

namespace SharpAudio.Util
{
    public class SoundStream
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
                throw new ArgumentNullException("stream");
;
            _decoder = new WaveDecoder(stream);
        }

        public byte[] ReadAll()
        {
            _decoder.GetSamples(out byte[] data);

            return data;
        }
    }
}
