using System;
using SharpAudio.ALBinding;

namespace SharpAudio.AL
{
    internal sealed class ALEngine : AudioEngine
    {
        private IntPtr _device;
        private IntPtr _context;
        private readonly bool _floatSupport;

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
            checkAlError();
        }


        internal static void checkAlError()
        {
            int error = AlNative.alGetError();
            if (error != AlNative.AL_NO_ERROR)
            {
                
                string formatErrMsg = string.Format("OpenAL Error: {0} - {1}", Marshal.PtrToStringAuto(AlNative.alGetString(error)), AlNative.alcGetCurrentContext().ToString());
                throw new SharpAudioException(formatErrMsg);
            }
        }

        private void checkAlcError()
        {
            int error = AlNative.alcGetError(_device);
            if (error != AlNative.ALC_NO_ERROR)
            {
                string formatErrMsg = string.Format("OpenALc Error: {0}", Marshal.PtrToStringAuto(AlNative.alcGetString(_device, error)));
                throw new SharpAudioException(formatErrMsg);
            }
        }

        public override AudioBuffer CreateBuffer()
        {
            return new ALBuffer();
        }

        protected override void PlatformDispose()
        {
            AlNative.alcMakeContextCurrent((IntPtr) 0);
            checkAlcError();
            AlNative.alcDestroyContext(_context);
            checkAlcError();
            AlNative.alcCloseDevice(_device);
            checkAlcError();
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
            return new ALSubmixer(this);
        }
    }
}
