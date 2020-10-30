using System;
using System.IO;
using NLayer;

namespace SharpAudio.Codec.Mp3
{
    public class Mp3Decoder : Decoder
    {
        private int _index = 0;
        private MpegFile _mp3Stream;

        public Mp3Decoder(Stream s)
        {
            _mp3Stream = new MpegFile(s);

            _audioFormat.Channels = _mp3Stream.Channels;
            _audioFormat.BitsPerSample = 16;
            _audioFormat.SampleRate = _mp3Stream.SampleRate;

            _numSamples = (int) _mp3Stream.Length / sizeof(float);
        }

        public override bool IsFinished => _mp3Stream.Position == _mp3Stream.Length;

        public override long GetSamples(int samples, ref byte[] data)
        {
            var bytes = _audioFormat.BytesPerSample * samples;
            Array.Resize(ref data, bytes);

            int read = _mp3Stream.ReadSamplesInt16(data, 0, 2 * bytes);

            return read;
        }
    }
}