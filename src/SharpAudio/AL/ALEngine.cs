using System;
using System.Runtime.InteropServices;
using System.Threading;
using SharpAudio.ALBinding;

namespace SharpAudio.AL
{
    internal sealed class ALEngine : AudioEngine
    {
        private static IntPtr _device;
        private static IntPtr _context;
        private bool _floatSupport { get; set; }
        private static int usingResource = 0;

        public override AudioBackend BackendType => AudioBackend.OpenAL;
        private static Mutex mutex = new Mutex();

        public ALEngine(AudioEngineOptions options)
        {
            mutex.WaitOne();
            usingResource++;
            if (usingResource == 1)
            {
                int[] argument = new int[] { AlNative.ALC_FREQUENCY, options.SampleRate };
                // opens the default device.
                _device = AlNative.alcOpenDevice(null);
                checkAlcError();
                _context = AlNative.alcCreateContext(_device, argument);
                checkAlcError();

                //
                AlNative.alcMakeContextCurrent(_context);
                checkAlcError();
                _floatSupport = AlNative.alIsExtensionPresent("AL_EXT_FLOAT32");
                checkAlError();

            }
            mutex.ReleaseMutex();
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
                string formatErrMsg = string.Format("OpenALc Error: {0} - {1}", Marshal.PtrToStringAuto(AlNative.alcGetString(_device, error)), AlNative.alcGetCurrentContext().ToString());
                throw new SharpAudioException(formatErrMsg);
            }
        }

        public override AudioBuffer CreateBuffer()
        {
            return new ALBuffer();
        }

        protected override void PlatformDispose()
        {
            mutex.WaitOne();
            if (usingResource == 1)
            {
                if (_context != IntPtr.Zero)
                {
                    AlNative.alcSuspendContext(_context);
                    checkAlcError();
                }
                AlNative.alcMakeContextCurrent(IntPtr.Zero);
                checkAlcError();

                if (_context != IntPtr.Zero)
                {

                    AlNative.alcDestroyContext(_context);
                    checkAlcError();
                    _context = IntPtr.Zero;
                    AlNative.alcCloseDevice(_device);
                    checkAlcError();
                    _device = IntPtr.Zero;

                }
                usingResource = 0;
            }
            mutex.ReleaseMutex();
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
