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

            var engine = AudioEngine.CreateXAudio(new AudioEngineOptions());
            var buffer = engine.CreateBuffer();
            buffer.BufferData(data, soundStream.Format, data.Length);

            var source = engine.CreateSource();
            source.QueryBuffer(buffer);
            source.Play();


            while (source.IsPlaying())
            {
                Thread.Sleep(100);
            }
        }
    }
}
