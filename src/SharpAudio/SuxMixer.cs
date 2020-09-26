using System;

namespace SharpAudio
{
    /// <summary>
    /// Represents an abstract source
    /// </summary>
    public abstract class SubMixer : IDisposable
    {
        public void Dispose()
        {
            throw new NotImplementedException();
        }
    }
}
