using System;

namespace SharpAudio.Tests
{
    public static class TestUtil
    {
        public static short[] CreateBeep(float freq, TimeSpan duration, out AudioFormat format)
        {
            format.BitsPerSample = 16;
            format.Channels = 1;
            format.SampleRate = 44100;
            var size = format.SampleRate * duration.Seconds;
            var samples = new short[size];

            for (var i = 0; i < size; i++) samples[i] = (short) (32760 * Math.Sin(2 * Math.PI * freq / size * i));

            return samples;
        }
    }
}