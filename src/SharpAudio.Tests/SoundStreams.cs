using SharpAudio.Codec;

namespace SharpAudio.Tests
{
    public class SoundStreams
    {
        [BackendFact(AudioBackend.OpenAL, AudioBackend.XAudio2)]
        public void Playback()
        {
            var mp3Stream = typeof(SoundStreams).Assembly.GetManifestResourceStream("SharpAudio.Tests.Assets.test.mp3");

            var engine = AudioEngine.CreateDefault();
            using (var soundStream = new SoundStream(mp3Stream, new SoundSink(engine)))
            {
                soundStream.Play();
            }
        }

        [BackendFact(AudioBackend.OpenAL, AudioBackend.XAudio2)]
        public void Reuse()
        {
            var mp3Stream = typeof(SoundStreams).Assembly.GetManifestResourceStream("SharpAudio.Tests.Assets.test.mp3");

            var engine = AudioEngine.CreateDefault();
            var soundStream = new SoundStream(mp3Stream, new SoundSink(engine));

            soundStream.Play();
            soundStream.Dispose();
            
            mp3Stream = typeof(SoundStreams).Assembly.GetManifestResourceStream("SharpAudio.Tests.Assets.test.mp3");
            soundStream = new SoundStream(mp3Stream, new SoundSink(engine));
            soundStream.Volume = 1.0f;
        }
    }
}
