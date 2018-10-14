using System;
using System.Collections.Generic;
using System.Text;
using SharpAudio.ALBinding;

namespace SharpAudio.AL
{
    class ALEngine : AudioEngine
    {
        private IntPtr _device;

        public override AudioBackend BackendType => AudioBackend.OpenAL;

        public ALEngine(AudioEngineOptions options)
        {
            _device = AlNative.alcOpenDevice("");
        }

        public override AudioBuffer CreateBuffer()
        {
            throw new NotImplementedException();
        }

        public override AudioSource CreateSource()
        {
            throw new NotImplementedException();
        }

        protected override void PlatformDispose()
        {
            AlNative.alcCloseDevice(_device);
        }
    }
}
