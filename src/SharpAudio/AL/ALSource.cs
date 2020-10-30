using SharpAudio.ALBinding;

namespace SharpAudio.AL
{
    internal sealed class ALSource : AudioSource
    {
        private readonly uint _source;

        public ALSource()
        {
            var sources = new uint[1];
            AlNative.alGenSources(1, sources);
            ALEngine.checkAlError();
            _source = sources[0];
        }

        public override int BuffersQueued
        {
            get
            {
                RemoveProcessed();

                AlNative.alGetSourcei(_source, AlNative.AL_BUFFERS_QUEUED, out var bufs);
                return bufs;
            }
        }

        public override float Volume
        {
            get => _volume;
            set
            {
                _volume = value;
                AlNative.alSourcef(_source, AlNative.AL_GAIN, value);
            }
        }

        public override bool Looping
        {
            get => _looping;
            set
            {
                _looping = value;
                AlNative.alSourcei(_source, AlNative.AL_LOOPING, value ? 1 : 0);
            }
        }

        public override void Dispose()
        {
            AlNative.alDeleteSources(1, new[] {_source});
            ALEngine.checkAlError();
        }

        public override void Flush()
        {
            //TODO: not sure if we should unquery buffers here. Investigate
        }

        public override bool IsPlaying()
        {
            AlNative.alGetSourcei(_source, AlNative.AL_SOURCE_STATE, out var state);
            var playing = state == AlNative.AL_PLAYING;

            return playing;
        }

        public override void Play()
        {
            AlNative.alSourcePlay(_source);
            ALEngine.checkAlError();
        }

        private void RemoveProcessed()
        {
            //before querying new data check if sth was processed already:
            AlNative.alGetSourcei(_source, AlNative.AL_BUFFERS_PROCESSED, out var processed);
            ALEngine.checkAlError();

            while (processed > 0)
            {
                var bufs = new uint[] {1};
                AlNative.alSourceUnqueueBuffers(_source, 1, bufs);
                processed--;
            }
        }

        public override void QueueBuffer(AudioBuffer buffer)
        {
            RemoveProcessed();

            var alBuffer = (ALBuffer) buffer;
            AlNative.alSourceQueueBuffers(_source, 1, new[] {alBuffer.Buffer});
            ALEngine.checkAlError();
        }

        public override void Stop()
        {
            AlNative.alSourceStop(_source);
            ALEngine.checkAlError();
        }
    }
}