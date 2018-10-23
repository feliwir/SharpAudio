using SharpAudio.ALBinding;
using System;

namespace SharpAudio.AL
{
    class ALSource : AudioSource
    {
        private uint _source;

        public override int BuffersQueued => throw new NotImplementedException();

        public override float Volume
        {
            get { return _volume; }
            set { _volume = value; AlNative.alSourcef(_source, AlNative.AL_GAIN, value); }
        }

        public ALSource()
        {
            var sources = new uint[1];
            AlNative.alGenSources(1, sources);
            ALEngine.checkAlError();
            _source = sources[0];
        }

        public override void Dispose()
        {
            AlNative.alDeleteSources(1, new uint[] { _source });
        }

        public override void Flush()
        {
            throw new NotImplementedException();
        }

        public override bool IsPlaying()
        {
            AlNative.alGetSourcei(_source, AlNative.AL_SOURCE_STATE, out int state);
            return state == AlNative.AL_PLAYING;
        }

        public override void Play()
        {
            AlNative.alSourcePlay(_source);
            ALEngine.checkAlError();
        }

        public override void QueryBuffer(AudioBuffer buffer)
        {
            var alBuffer = (ALBuffer)buffer;
            AlNative.alSourceQueueBuffers(_source, 1, new uint[] { alBuffer.Buffer });
            ALEngine.checkAlError();
        }

        public override void Stop()
        {
            throw new NotImplementedException();
        }
    }
}
