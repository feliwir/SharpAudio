using System;
using System.Collections.Generic;
using System.Text;

namespace SharpAudio
{
    public sealed class BufferChain
    {
        private List<AudioBuffer> _buffers;
        private readonly int _numBuffers = 3;
        private int _currentBuffer = 0;

        public BufferChain(AudioEngine engine)
        {
            _buffers = new List<AudioBuffer>();

            for (int i=0;i<_numBuffers;i++)
            {
                _buffers.Add(engine.CreateBuffer());
            }
        }

        public AudioBuffer BufferData<T>(T[] buffer, AudioFormat format) where T : unmanaged
        {
            var buf = _buffers[_currentBuffer];

            _buffers[_currentBuffer].BufferData(buffer, format);

            _currentBuffer++;
            _currentBuffer %= 3;

            return buf;
        }

        public void QueryData<T>(AudioSource target, T[] buffer, AudioFormat format) where T : unmanaged
        {
            var buf = BufferData(buffer, format);
            target.QueryBuffer(buf);
        }
    }
}
