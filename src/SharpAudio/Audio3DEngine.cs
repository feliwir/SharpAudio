using System;
using System.Numerics;

namespace SharpAudio
{
    /// <summary>
    /// Represents an abstract 3d audio engine
    /// </summary>
    public abstract class Audio3DEngine : IDisposable
    {
        public abstract void SetListenerPosition(Vector3 position);
        public abstract void SetSourcePosition(AudioSource source, Vector3 position);

        public abstract void Dispose();
    }
}
