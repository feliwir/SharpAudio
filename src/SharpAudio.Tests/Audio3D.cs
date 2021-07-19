using System;
using System.Numerics;
using System.Threading;

namespace SharpAudio.Tests
{
    public class Audio3D
    {
        [BackendFact(AudioBackend.XAudio2)]
        public void SpatialXAudio()
        {
            TestSpatial(AudioEngine.CreateXAudio());
        }

        [BackendFact(AudioBackend.OpenAL)]
        public void SpatialOpenAL()
        {
            using (var engine = AudioEngine.CreateOpenAL())
            {
                TestSpatial(engine);
            }
        }

        void TestSpatial(AudioEngine engine)
        {
            var audio3d = engine.Create3DEngine();
            audio3d.SetListenerPosition(new Vector3(0, 0, 0));

            var buffer = engine.CreateBuffer();
            var leftSource = engine.CreateSource();
            var rightSource = engine.CreateSource();

            var samples = TestUtil.CreateBeep(440.0f, TimeSpan.FromSeconds(1), out var format);

            buffer.BufferData(samples, format);

            leftSource.QueueBuffer(buffer);
            rightSource.QueueBuffer(buffer);

            audio3d.SetSourcePosition(leftSource, Vector3.UnitX * -10);
            audio3d.SetSourcePosition(rightSource, Vector3.UnitX * 10);

            leftSource.Play();

            //wait since Play is non blocking
            Thread.Sleep(1500);

            rightSource.Play();

            //wait since Play is non blocking
            Thread.Sleep(1500);
        }
    }
}
