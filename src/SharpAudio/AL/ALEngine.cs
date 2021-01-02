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
            checkAlcError();
        }

        private static String openAlErrorToString(int err)
        {
            switch (err)
            {
            case AlNative.AL_NO_ERROR:
                return "AL_NO_ERROR";
            case AlNative.AL_INVALID_NAME:
                return "AL_INVALID_NAME";
            case AlNative.AL_INVALID_ENUM:
                return "AL_INVALID_ENUM";
            case AlNative.AL_INVALID_VALUE:
                return "AL_INVALID_VALUE";
            case AlNative.AL_INVALID_OPERATION:
                return "AL_INVALID_OPERATION";
            case AlNative.AL_OUT_OF_MEMORY:
                return "AL_OUT_OF_MEMORY";
            default:
                return "Unknown error code";
            }
        }

        internal static void checkAlError()
        {
            int error = AlNative.alGetError();
            if (error != AlNative.AL_NO_ERROR)
            {
                throw new SharpAudioException("OpenAL Error: " + openAlErrorToString(error));
            }
        }

        private void checkAlcError()
        {
            int error = AlNative.alcGetError(_device);
            if (error != AlNative.ALC_NO_ERROR)
            {
                throw new SharpAudioException("OpenAL Error: " + error);
            }
        }

        public override AudioBuffer CreateBuffer()
        {
            return new ALBuffer();
        }

        protected override void PlatformDispose()
        {
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
