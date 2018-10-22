using SharpAudio.ALBinding;
using System;

namespace SharpAudio.AL
{
    class ALEngine : AudioEngine
    {
        private IntPtr _device;
        private IntPtr _context;

        public override AudioBackend BackendType => AudioBackend.OpenAL;

        public ALEngine(AudioEngineOptions options)
        {
            _device = AlNative.alcOpenDevice(null);
            checkAlError();
            _context = AlNative.alcCreateContext(_device, null);
            checkAlcError();
            AlNative.alcMakeContextCurrent(_context);
            checkAlcError();
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
