using System;
using System.Threading;
using Xunit;

namespace SharpAudio.Tests
{
    public class Engines
    {
        [BackendFact(AudioBackend.XAudio2)]
        public void CreateXAudio()
        {
            TestEngine(AudioEngine.CreateXAudio());
        }

        [BackendFact(AudioBackend.OpenAL)]
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
            format.SampleRate = 44100;
            float freq = 440.0f;
            var size = format.SampleRate;
            var samples = new short[size];

            for (int i = 0; i < size; i++)
            {
                samples[i] = (short)(32760 * Math.Sin((2 * Math.PI * freq) / size * i));
            }

            buffer.BufferData(samples, format);

            source.QueueBuffer(buffer);

            source.Play();

            //wait since Play is non blocking
            Thread.Sleep(1000);
        }
    }
}
