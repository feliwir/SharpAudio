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
            using (var soundStream = new SoundStream(mp3Stream, engine))
            {
                soundStream.Play();
            }
        }
    }
}
