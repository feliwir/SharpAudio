using System;
using System.Runtime.InteropServices;
using SharpDX.XAudio2;

namespace SharpAudio.XA2
{
    internal sealed class XA2Engine : AudioEngine
    {
        internal MasteringVoice MasterVoice { get; }

        public override AudioBackend BackendType => AudioBackend.XAudio2;
        public XAudio2 Device { get; }

        private const uint S_OK = 0;
        private const uint S_FALSE = 1;
        private const uint RPC_E_CHANGED_MODE = 0x80010106;
        private const uint COINIT_MULTITHREADED = 0x0;
        private const uint COINIT_APARTMENTTHREADED = 0x2;

        [DllImport("ole32.dll", SetLastError = true, CallingConvention = CallingConvention.StdCall)]
        private static extern uint CoInitializeEx([In, Optional] IntPtr pvReserved, [In] uint dwCoInit);

        static XA2Engine()
        {
            var hr = CoInitializeEx(IntPtr.Zero, COINIT_APARTMENTTHREADED);
            if (hr == RPC_E_CHANGED_MODE)
            {
                hr = CoInitializeEx(IntPtr.Zero, COINIT_MULTITHREADED);
            }
        }

        public XA2Engine(AudioEngineOptions options)
        {
            Device = new XAudio2(XAudio2Flags.DebugEngine, ProcessorSpecifier.AnyProcessor);
            MasterVoice = new MasteringVoice(Device, options.SampleChannels, options.SampleRate);
        }

        protected override void PlatformDispose()
        {
            MasterVoice.Dispose();
            Device.Dispose();
        }

        public override AudioBuffer CreateBuffer()
        {
            return new XA2Buffer();
        }

        public override AudioSource CreateSource()
        {
            return new XA2Source(this);
        }

        public override Audio3DEngine Create3DEngine()
        {
            return new XA23DEngine(this);
        }
    }
}
