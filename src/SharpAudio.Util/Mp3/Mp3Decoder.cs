using MP3Sharp;
using System;
using System.IO;

namespace SharpAudio.Util.Mp3
{
    internal class Mp3Decoder : Decoder
    {
        private MP3Stream _mp3Stream;

        public Mp3Decoder(Stream s)
        {
            _mp3Stream = new MP3Stream(s);

            _audioFormat.Channels = _mp3Stream.ChannelCount;
            _audioFormat.BitsPerSample = 16;
            _audioFormat.SampleRate = _mp3Stream.Frequency;
        }

        public override long GetSamples(int samples, out byte[] data)
        {
            int bytes = _audioFormat.BytesPerSample * samples;
            data = new byte[bytes];
            int read = _mp3Stream.Read(data, 0, bytes);

            return read / _audioFormat.BytesPerSample;
        }

        public override long GetSamples(out byte[] data)
        {
            throw new NotImplementedException();
        }
    }
}
