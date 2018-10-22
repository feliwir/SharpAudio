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
        }
    }
}
