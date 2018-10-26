using SharpAudio.Util;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using Xunit;

namespace SharpAudio.Tests
{
    public class FileFormats
    {
        [Fact]
        public void Wave()
        {
            var files = typeof(FileFormats).Assembly.GetManifestResourceNames();

            var waveStream = typeof(FileFormats).Assembly.GetManifestResourceStream("SharpAudio.Tests.Assets.bach.wav");

            var soundStream = new SoundStream(waveStream);

            Assert.True(soundStream.Format.BitsPerSample == 16);
            Assert.True(soundStream.Format.Channels == 2);
            Assert.True(soundStream.Format.SampleRate == 44100);

            var data = soundStream.ReadAll();

            float duration = data.Length / (float)soundStream.Format.BytesPerSecond;

            Assert.True(MathF.Round(duration) == 54);
        }

        [Fact]
        public void Mp3()
        {
            var files = typeof(FileFormats).Assembly.GetManifestResourceNames();

            var mp3Stream = typeof(FileFormats).Assembly.GetManifestResourceStream("SharpAudio.Tests.Assets.test.mp3");

            var soundStream = new SoundStream(mp3Stream);

            Assert.True(soundStream.Format.BitsPerSample == 16);
            Assert.True(soundStream.Format.Channels == 2);
            Assert.True(soundStream.Format.SampleRate == 44100);

            // var data = soundStream.ReadAll();

            // float duration = data.Length / (float)soundStream.Format.BytesPerSecond;

            // Assert.True(MathF.Round(duration) == 54);
        }
    }
}
