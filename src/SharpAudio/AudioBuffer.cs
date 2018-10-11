using System;
using System.Collections.Generic;
using System.Text;

namespace SharpAudio
{
    public abstract class AudioBuffer : IDisposable
    {
        public abstract AudioFormat Format { get; protected set; }

        public abstract void BufferData<T>(T[] buffer, AudioFormat format, int sizeInBytes) where T : struct;
        public abstract void Dispose();
    }
}
