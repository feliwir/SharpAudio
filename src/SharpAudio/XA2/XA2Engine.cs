using System;
using System.Runtime.InteropServices;
using SharpDX.XAudio2;

namespace SharpAudio.XA2
{
    internal sealed class XA2Engine : AudioEngine
    {
        private const uint RPC_E_CHANGED_MODE = 0x80010106;
        private const uint COINIT_MULTITHREADED = 0x0;
        private const uint COINIT_APARTMENTTHREADED = 0x2;

        static XA2Engine()
        {
            var hr = CoInitializeEx(IntPtr.Zero, COINIT_APARTMENTTHREADED);
            if (hr == RPC_E_CHANGED_MODE) hr = CoInitializeEx(IntPtr.Zero, COINIT_MULTITHREADED);
        }

        public XA2Engine(AudioEngineOptions options)
        {
            Device = new XAudio2(XAudio2Flags.None, ProcessorSpecifier.DefaultProcessor);
            MasterVoice = new MasteringVoice(Device, options.SampleChannels, options.SampleRate);
        }

        internal MasteringVoice MasterVoice { get; }

        public override AudioBackend BackendType => AudioBackend.XAudio2;
        public XAudio2 Device { get; }

        [DllImport("ole32.dll", SetLastError = true, CallingConvention = CallingConvention.StdCall)]
        private static extern uint CoInitializeEx([In] [Optional] IntPtr pvReserved, [In] uint dwCoInit);

        protected override void PlatformDispose()
        {
            MasterVoice.Dispose();
            Device.Dispose();
        }

        public override AudioBuffer CreateBuffer()
        {
            return new XA2Buffer();
        }

        public override AudioSource CreateSource(Submixer mixer = null)
        {
            return new XA2Source(this, (XA2Submixer) mixer);
        }

        public override Audio3DEngine Create3DEngine()
        {
            return new XA23DEngine(this);
        }

        public override Submixer CreateSubmixer()
        {
            return new XA2Submixer(this);
        }
    }
}