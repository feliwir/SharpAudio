using System;
using System.Threading;

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
            using (var engine = AudioEngine.CreateOpenAL())
            {
                TestEngine(engine);
            }
        }

        void TestEngine(AudioEngine engine)
        {
            var buffer = engine.CreateBuffer();
            var source = engine.CreateSource();

            var samples = TestUtil.CreateBeep(440.0f, TimeSpan.FromSeconds(1), out var format);

            buffer.BufferData(samples, format);

            source.QueueBuffer(buffer);

            source.Play();

            //wait since Play is non blocking
            Thread.Sleep(1000);
        }
    }
}
