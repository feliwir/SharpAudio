using System;
using System.Collections.Generic;
using NVorbis;
using System.IO;
using System.Text;

namespace SharpAudio.Util.Vorbis
{
    internal class VorbisDecoder : Decoder
    {
        private VorbisReader _reader;

        public override bool IsFinished => throw new NotImplementedException();

        public VorbisDecoder(Stream s)
        {
            _reader = new VorbisReader(s, true);

            _audioFormat.Channels = _reader.Channels;
            _audioFormat.BitsPerSample = 32;
            _audioFormat.SampleRate = _reader.SampleRate;

            _numSamples = (int)_reader.TotalSamples;
        }

        public override long GetSamples(int samples, out byte[] data)
        {
            int bytes = _audioFormat.BytesPerSample * samples;
            data = new byte[bytes];
            return samples;
        }
    }
}
