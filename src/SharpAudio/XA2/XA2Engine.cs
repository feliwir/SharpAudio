using System;
using System.Collections.Generic;
using System.Text;
using SharpDX.XAudio2;

namespace SharpAudio.XA2
{
    class XA2Engine : AudioEngine
    {
        private XAudio2 _device;
        private MasteringVoice _master;

        public override AudioBackend BackendType => AudioBackend.XAudio2;
        public XAudio2 Device => _device;

        public XA2Engine(AudioEngineOptions options)
        {
            _device = new XAudio2(XAudio2Flags.DebugEngine,ProcessorSpecifier.AnyProcessor);

            _master = new MasteringVoice(_device,options.SampleChannels,options.SampleRate);
        }

        protected override void PlatformDispose()
        {
            _master.Dispose();
            _device.Dispose();
        }

        public override AudioBuffer CreateBuffer()
        {
            return new XA2Buffer();
        }

        public override AudioSource CreateSource()
        {
            return new XA2Source(this);
        }
    }
}
