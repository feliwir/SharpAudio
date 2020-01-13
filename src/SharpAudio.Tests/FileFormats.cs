using SharpAudio.Codec;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using Xunit;

namespace SharpAudio.Tests
{
    public class FileFormats
    {
        [BackendFact(AudioBackend.OpenAL, AudioBackend.XAudio2)]
        public void Wave()
        {
            var waveStream = typeof(FileFormats).Assembly.GetManifestResourceStream("SharpAudio.Tests.Assets.bach.wav");

            var engine = AudioEngine.CreateDefault();
            var soundStream = new SoundStream(waveStream, engine);

            Assert.True(soundStream.Format.BitsPerSample == 16);
            Assert.True(soundStream.Format.Channels == 2);
            Assert.True(soundStream.Format.SampleRate == 44100);

            var duration = soundStream.Duration;

            Assert.True(duration.Seconds == 54);
        }

        [BackendFact(AudioBackend.OpenAL, AudioBackend.XAudio2)]
        public void Mp3()
        {
            var mp3Stream = typeof(FileFormats).Assembly.GetManifestResourceStream("SharpAudio.Tests.Assets.test.mp3");

            var engine = AudioEngine.CreateDefault();
            var soundStream = new SoundStream(mp3Stream, engine);

            Assert.True(soundStream.Format.BitsPerSample == 16);
            Assert.True(soundStream.Format.Channels == 2);
            Assert.True(soundStream.Format.SampleRate == 44100);

            var duration = soundStream.Duration;

            Assert.True(duration.Seconds == 27);
        }

        [BackendFact(AudioBackend.OpenAL, AudioBackend.XAudio2)]
        public void Vorbis()
        {
            var vorbisStream = typeof(FileFormats).Assembly.GetManifestResourceStream("SharpAudio.Tests.Assets.Example.ogg");

            var engine = AudioEngine.CreateDefault();
            var soundStream = new SoundStream(vorbisStream, engine);

            Assert.True(soundStream.Format.BitsPerSample == 16);
            Assert.True(soundStream.Format.Channels == 2);
            Assert.True(soundStream.Format.SampleRate == 44100);

            var duration = soundStream.Duration;

            Assert.True(duration.Seconds == 3);
        }
    }
}
