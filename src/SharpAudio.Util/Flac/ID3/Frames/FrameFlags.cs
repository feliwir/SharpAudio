using System;

namespace SharpAudio.Util.ID3.Frames
{
    [Flags]
    public enum FrameFlags
    {
        None = 0,
        ReadOnly = 1,
        PreserveFileAltered = 2,
        PreserveTagAltered = 4,
        Compressed = 8,
        Encrypted = 10,
        GroupIdentified = 12,
        UnsyncApplied = 14,
        DataLengthIndicatorPresent = 16
    }
}