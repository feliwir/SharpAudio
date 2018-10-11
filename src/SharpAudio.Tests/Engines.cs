using System;
using Xunit;

namespace SharpAudio.Tests
{
    public class Engines
    {
        [Fact]
        public void CreateXAudio()
        {
            var engine = AudioEngine.CreateXAudio(new AudioEngineOptions());
            var buffer = engine.CreateBuffer();
        }
    }
}
