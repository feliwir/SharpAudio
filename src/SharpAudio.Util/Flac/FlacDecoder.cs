using NLayer;
using System;
using System.IO;

namespace SharpAudio.Util.Flac
{
    public class FlacDecoder : Decoder
    {
        FlacFile _flacStream;
        public override bool IsFinished => _flacStream.Position == _flacStream.Length;

        public FlacDecoder(Stream s)
        {
            _flacStream = new FlacFile(s);
            _audioFormat = _flacStream.WaveFormat;
            _numSamples = (int)_flacStream.Length / sizeof(float);
        }

        public override long GetSamples(int samples, ref byte[] data)
        {
            int bytes = _audioFormat.BytesPerSample * samples;
            Array.Resize(ref data, bytes);
            return _flacStream.Read(data, 0, bytes);
        }
    }
}
