namespace SharpAudio.Util.Flac
{
    internal unsafe class FlacSubFrameData
    {
        public int* DestinationBuffer;
        public int* ResidualBuffer;
        public FlacPartitionedRiceContent Content = new FlacPartitionedRiceContent();
    }
}