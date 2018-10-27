using System;
using System.Collections.Generic;
using System.Text;

namespace SharpAudio
{
    /// <summary>
    /// This class represents an audio buffer, which is used to transfer data to the hardware
    /// </summary>
    public abstract class AudioBuffer : IDisposable
    {
        internal AudioFormat _format;

        /// <summary>
        /// The format of this buffer
        /// </summary>
        public AudioFormat Format => _format;

        public abstract void BufferData<T>(T[] buffer, AudioFormat format) where T : unmanaged;
        public abstract void Dispose();
    }
}
