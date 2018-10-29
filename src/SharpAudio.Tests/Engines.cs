using System;
using Xunit;

namespace SharpAudio.Tests
{
    public class Engines
    {
        [Fact]
        public void CreateXAudio()
        {
            TestEngine(AudioEngine.CreateXAudio());  
        }

        [Fact]
        public void CreateOpenAL()
        {
            TestEngine(AudioEngine.CreateOpenAL());
        }

        void TestEngine(AudioEngine engine)
        {
            var buffer = engine.CreateBuffer();
            var source = engine.CreateSource();

            AudioFormat format;
            format.BitsPerSample = 16;
            format.Channels = 1;
            format.SampleRate = 8000;

            short[] data = { 0, -100, 100, -200, 200 };

            buffer.BufferData(data, format);

            source.QueryBuffer(buffer);

            source.Play();

        }
    }
}
