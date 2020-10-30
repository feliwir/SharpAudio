using System.Collections.Generic;

namespace SharpAudio
{
    public sealed class BufferChain
    {
        private readonly int _numBuffers = 3;
        private readonly List<AudioBuffer> _buffers;
        private int _currentBuffer;

        public BufferChain(AudioEngine engine)
        {
            _buffers = new List<AudioBuffer>();

            for (var i = 0; i < _numBuffers; i++) _buffers.Add(engine.CreateBuffer());
        }

        public AudioBuffer BufferData<T>(T[] buffer, AudioFormat format) where T : unmanaged
        {
            var buf = _buffers[_currentBuffer];

            _buffers[_currentBuffer].BufferData(buffer, format);

            _currentBuffer++;
            _currentBuffer %= 3;

            return buf;
        }

        public void QueueData<T>(AudioSource target, T[] buffer, AudioFormat format) where T : unmanaged
        {
            var buf = BufferData(buffer, format);
            target.QueueBuffer(buf);
        }
    }
}