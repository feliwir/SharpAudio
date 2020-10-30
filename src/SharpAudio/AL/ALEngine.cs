using System;
using SharpAudio.ALBinding;

namespace SharpAudio.AL
{
    internal sealed class ALEngine : AudioEngine
    {
        private readonly bool _floatSupport;
        private readonly IntPtr _context;
        private readonly IntPtr _device;

        public ALEngine(AudioEngineOptions options)
        {
            _device = AlNative.alcOpenDevice(null);
            checkAlcError();
            _context = AlNative.alcCreateContext(_device, null);
            checkAlcError();
            AlNative.alcMakeContextCurrent(_context);
            checkAlcError();
            _floatSupport = AlNative.alIsExtensionPresent("AL_EXT_FLOAT32");
        }

        public override AudioBackend BackendType => AudioBackend.OpenAL;

        internal static void checkAlError()
        {
            var error = AlNative.alGetError();
            if (error != AlNative.AL_NO_ERROR) throw new SharpAudioException("OpenAL Error: " + error);
        }

        private void checkAlcError()
        {
            var error = AlNative.alcGetError(_device);
            if (error != AlNative.ALC_NO_ERROR) throw new SharpAudioException("OpenAL Error: " + error);
        }

        public override AudioBuffer CreateBuffer()
        {
            return new ALBuffer();
        }

        protected override void PlatformDispose()
        {
            AlNative.alcDestroyContext(_context);
            AlNative.alcCloseDevice(_device);
        }

        public override Audio3DEngine Create3DEngine()
        {
            return new AL3DEngine();
        }

        public override AudioSource CreateSource(Submixer mixer = null)
        {
            return new ALSource();
        }

        public override Submixer CreateSubmixer()
        {
            throw new NotImplementedException();
        }
    }
}