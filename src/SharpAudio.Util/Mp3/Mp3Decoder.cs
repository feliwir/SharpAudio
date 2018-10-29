using NLayer;
using System;
using System.IO;

namespace SharpAudio.Util.Mp3
{
    internal class Mp3Decoder : Decoder
    {
        private MpegFile _mp3Stream;
        private int _index = 0;

        public Mp3Decoder(Stream s)
        {
            _mp3Stream = new MpegFile(s);

            _audioFormat.Channels = _mp3Stream.Channels;
            _audioFormat.BitsPerSample = 16;
            _audioFormat.SampleRate = _mp3Stream.SampleRate;

            _numSamples = (int)_mp3Stream.Length / sizeof(float);
        }

        public override bool IsFinished => _mp3Stream.Position == _mp3Stream.Length;

        public override long GetSamples(int samples, out byte[] data)
        {
            int bytes = _audioFormat.BytesPerSample * samples;
            data = new byte[bytes];
            int read = _mp3Stream.ReadSamplesInt16(data, 0, 2 * bytes);

            return read;
        }
    }
}
