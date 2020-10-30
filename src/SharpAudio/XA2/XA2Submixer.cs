namespace SharpAudio.XA2
{
    internal sealed class XA2Submixer : Submixer
    {
        private readonly XA2Engine _engine;

        public XA2Submixer(XA2Engine engine)
        {
            _engine = engine;
            SubMixerVoice = new SubmixVoice(_engine.Device);
        }

        internal SubmixVoice SubMixerVoice { get; }

        public override float Volume
        {
            get => _volume;
            set
            {
                _volume = value;
                SubMixerVoice?.SetVolume(value);
            }
        }

        public override void Dispose()
        {
            SubMixerVoice.DestroyVoice();
            SubMixerVoice.Dispose();
        }
    }
}