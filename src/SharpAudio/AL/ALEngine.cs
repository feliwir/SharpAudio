using SharpAudio.ALBinding;
using System;

namespace SharpAudio.AL
{
    internal class ALEngine : AudioEngine
    {
        private IntPtr _device;
        private IntPtr _context;
        private bool _floatSupport;

        public override AudioBackend BackendType => AudioBackend.OpenAL;

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

        internal static void checkAlError()
        {
            int error = AlNative.alGetError();
            if (error != AlNative.AL_NO_ERROR)
            {
                throw new Exception("OpenAL Error: " + error);
            }
        }

        private void checkAlcError()
        {
            int error = AlNative.alcGetError(_device);
            if (error != AlNative.ALC_NO_ERROR)
            {
                throw new Exception("OpenAL Error: " + error);
            }
        }

        public override AudioBuffer CreateBuffer()
        {
            return new ALBuffer();
        }

        public override AudioSource CreateSource()
        {
            return new ALSource();
        }

        protected override void PlatformDispose()
        {
            AlNative.alcDestroyContext(_context);
            AlNative.alcCloseDevice(_device);
        }
    }
}
