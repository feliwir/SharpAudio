using SharpDX.Multimedia;
using SharpDX.XAudio2;

namespace SharpAudio.XA2
{
    class XA2Source : AudioSource
    {
        private readonly XA2Engine _engine;
        private SourceVoice _voice;

        public XA2Source(XA2Engine engine)
        {
            _engine = engine;
        }

        private void SetupVoice(AudioFormat format)
        {
            WaveFormat wFmt = new WaveFormat(format.SampleRate, format.BitsPerSample, format.Channels);
            _voice = new SourceVoice(_engine.Device, wFmt);
        }

        public override int BuffersQueued => _voice.State.BuffersQueued;

        public override float Volume {
            get { _voice.GetVolume(out float val); return val; }
            set => _voice.SetVolume(value); }

        public override void Dispose()
        {
            _voice.DestroyVoice();
            _voice.Dispose();
        }

        public override bool IsPlaying()
        {
           return _voice?.State.BuffersQueued > 0;
        }

        public override void Play()
        {
            _voice?.Start();
        }

        public override void Stop()
        {
            _voice?.Stop();
        }

        public override void QueryBuffer(AudioBuffer buffer)
        {
            if(_voice==null)
            {
                SetupVoice(buffer.Format);
            }

            var xaBuffer = (XA2Buffer)buffer;
            _voice.SubmitSourceBuffer(xaBuffer.Buffer,null);
        }

        public override void Flush()
        {
            _voice.FlushSourceBuffers();
        }
    }
}
