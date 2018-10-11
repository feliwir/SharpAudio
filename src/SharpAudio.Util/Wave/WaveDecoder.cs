using System;
using System.IO;

namespace SharpAudio.Util.Wave
{
    internal abstract class WavParser
    {
        public abstract byte[] Parse(BinaryReader reader, int size, WaveFormat format);

        public static WavParser GetParser(WaveFormatType type)
        {
            switch (type)
            {
                case WaveFormatType.Pcm: return new PcmParser();
                case WaveFormatType.DviAdpcm: return new DviAdpcmParser();
                default: throw new NotSupportedException("Invalid or unknown .wav compression format!");
            }
        }
    }

    internal class WaveDecoder : Decoder
    {
        private RiffHeader _header;
        private WaveFormat _format;
        private WaveFact _fact;
        private WaveData _data;
        private AudioFormat _audioFormat;
        private long _numSamples;
        private long _bytesLeft;
        private byte[] _decodedData;

        public override AudioFormat Format => _audioFormat;

        public override long NumberOfSampes => _numSamples;

        public WaveDecoder(Stream s) : base(s)
        {
            using (BinaryReader br = new BinaryReader(_stream))
            {
                _header = RiffHeader.Parse(br);
                _format = WaveFormat.Parse(br);

                if (_format.AudioFormat != WaveFormatType.Pcm)
                {
                    _fact = WaveFact.Parse(br);
                }

                _data = WaveData.Parse(br);
                _decodedData = WavParser.GetParser(_format.AudioFormat)
                                       .Parse(br, (int)_data.SubChunkSize, _format);

                _bytesLeft = _decodedData.Length;

                _audioFormat.BitsPerSample = _format.BitsPerSample;
                _audioFormat.Channels = _format.NumChannels;
                _audioFormat.SampleRate = (int)_format.SampleRate;

                _numSamples = _decodedData.Length / _audioFormat.BytesPerSample;
            }
        }

        public override long GetSamples(int samples, out byte[] data)
        {
            throw new NotImplementedException();
        }

        public override long GetSamples(out byte[] data)
        {
            data = _decodedData;

            long samples = _bytesLeft / _audioFormat.BytesPerSample;
            _bytesLeft = 0;
            return samples;
        }
    }
}
