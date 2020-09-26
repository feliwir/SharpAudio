using SharpDX.Multimedia;
using SharpDX.XAudio2;

namespace SharpAudio.XA2
{
    internal sealed class XA2Source : AudioSource
    {
        private readonly XA2Engine _engine;
        private readonly XA2Submixer _submixer;

        internal SourceVoice SourceVoice { get; private set; }

        public XA2Source(XA2Engine engine, XA2Submixer submixer)
        {
            _engine = engine;
            _submixer = submixer;
        }

        private void SetupVoice(AudioFormat format)
        {

            WaveFormat wFmt = new WaveFormat(format.SampleRate, format.BitsPerSample, format.Channels);
            SourceVoice = new SourceVoice(_engine.Device, wFmt);

            if (_submixer != null)
            {
                var vsDesc = new VoiceSendDescriptor(_submixer.SubMixerVoice);
                SourceVoice.SetOutputVoices(new VoiceSendDescriptor[] { vsDesc });
            }

            SourceVoice.SetVolume(_volume);
        }

        public override int BuffersQueued => SourceVoice.State.BuffersQueued;

        public override float Volume
        {
            get { return _volume; }
            set { _volume = value; SourceVoice?.SetVolume(value); }
        }

        public override bool Looping
        {
            get { return _looping; }
            set { _looping = value; }
        }

        public override void Dispose()
        {
            SourceVoice.DestroyVoice();
            SourceVoice.Dispose();
        }

        public override bool IsPlaying()
        {
            return SourceVoice?.State.BuffersQueued > 0;
        }

        public override void Play()
        {
            SourceVoice?.Start();
        }

        public override void Stop()
        {
            SourceVoice?.Stop();
        }

        public override void QueueBuffer(AudioBuffer buffer)
        {
            if (SourceVoice == null)
            {
                SetupVoice(buffer.Format);
            }

            var xaBuffer = (XA2Buffer) buffer;
            if (_looping)
            {
                xaBuffer.Buffer.LoopCount = SharpDX.XAudio2.AudioBuffer.LoopInfinite;
            }
            SourceVoice.SubmitSourceBuffer(xaBuffer.Buffer, null);
        }

        public override void Flush()
        {
            SourceVoice.FlushSourceBuffers();
        }
    }
}
