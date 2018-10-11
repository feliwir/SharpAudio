using SharpDX;
using System;
using System.Collections.Generic;
using System.Text;

namespace SharpAudio.XA2
{
    class XA2Buffer : AudioBuffer
    {
        private DataStream _dataStream;

        public SharpDX.XAudio2.AudioBuffer Buffer { get; private set; }

        public int SizeInBytes { get; private set; }
        public int TotalSamples => SizeInBytes / Format.BytesPerSample;

        public override AudioFormat Format { get; protected set; }

        public override void BufferData<T>(T[] buffer, AudioFormat format, int sizeInBytes)
        {
            _dataStream?.Dispose();
            _dataStream = new DataStream(sizeInBytes, true, true);
            _dataStream.WriteRange(buffer, 0, buffer.Length);
            _dataStream.Position = 0;

            Format = format;
            SizeInBytes = sizeInBytes;

            Buffer = new SharpDX.XAudio2.AudioBuffer(_dataStream);
            Buffer.AudioBytes = SizeInBytes;
        }

        public override void Dispose()
        {
            _dataStream?.Dispose();
        }
    }
}
