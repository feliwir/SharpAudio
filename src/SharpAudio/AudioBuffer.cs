using System;
using System.Collections.Generic;
using System.Text;

namespace SharpAudio
{
    public abstract class AudioBuffer : IDisposable
    {
        protected AudioFormat _format;

        public AudioFormat Format => _format;

        public abstract void BufferData<T>(T[] buffer, AudioFormat format, int sizeInBytes) where T : struct;
        public abstract void Dispose();
    }
}
