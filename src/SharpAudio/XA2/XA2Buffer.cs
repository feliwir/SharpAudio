using SharpDX;

namespace SharpAudio.XA2
{
    internal sealed class XA2Buffer : AudioBuffer
    {
        private DataStream _dataStream;

        public SharpDX.XAudio2.AudioBuffer Buffer { get; }

        public int SizeInBytes { get; private set; }
        public int TotalSamples => SizeInBytes / Format.BytesPerSample;

        public XA2Buffer()
        {
            Buffer = new SharpDX.XAudio2.AudioBuffer();
        }

        public override unsafe void BufferData<T>(T[] buffer, AudioFormat format)
        {
            int sizeInBytes = sizeof(T) * buffer.Length;

            _dataStream?.Dispose();
            _dataStream = new DataStream(sizeInBytes, true, true);
            _dataStream.WriteRange(buffer, 0, buffer.Length);
            _dataStream.Position = 0;

            _format = format;
            SizeInBytes = sizeInBytes;
            Buffer.AudioDataPointer = _dataStream.PositionPointer;
            Buffer.AudioBytes = SizeInBytes;
        }

        public override void Dispose()
        {
            _dataStream?.Dispose();
         }
    }
}
