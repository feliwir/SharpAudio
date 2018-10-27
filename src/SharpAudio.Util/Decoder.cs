using System;
using System.IO;

namespace SharpAudio.Util
{
    internal abstract class Decoder : IDisposable
    {
        //protected Stream _stream;
        protected AudioFormat _audioFormat;

        public AudioFormat Format => _audioFormat;

        public abstract bool IsFinished { get; }

        /// <summary>
        /// Initializes a decoder
        /// </summary>
        /// <param name="s">the input stream</param>
        //protected Decoder(Stream s)
        //{
        //    _stream = s;
        //}

        /// <summary>
        /// Reads the specified amount of samples
        /// </summary>
        /// <param name="samples"></param>
        /// <param name="data"></param>
        /// <returns></returns>
        public abstract long GetSamples(int samples, out byte[] data);

        /// <summary>
        /// Reads all available samples
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public abstract long GetSamples(out byte[] data);

        public void Dispose()
        {
            //_stream.Close();
        }
    }
}
