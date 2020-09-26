using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;

namespace SharpAudio.Tests
{
    public class SubMixing
    {
        [BackendFact(AudioBackend.XAudio2)]
        public void SubMixingXAudio()
        {
            TestSubMixing(AudioEngine.CreateXAudio());
        }

        [BackendFact(AudioBackend.OpenAL)]
        public void SubMixingOpenAL()
        {
            TestSubMixing(AudioEngine.CreateOpenAL());
        }

        void TestSubMixing(AudioEngine engine)
        {
            var buffer = engine.CreateBuffer();
            var source = engine.CreateSource();

            var samples = TestUtil.CreateBeep(440.0f, TimeSpan.FromSeconds(1), out var format);

            buffer.BufferData(samples, format);

            source.QueueBuffer(buffer);
            source.Play();

            //wait since Play is non blocking
            Thread.Sleep(1500);

            var submixer = engine.CreateSubmixer();
            var mixedSource = engine.CreateSource(submixer);
            submixer.Volume = 0.5f;

            mixedSource.QueueBuffer(buffer);
            mixedSource.Play();

            Thread.Sleep(1500);
        }
    }
}
