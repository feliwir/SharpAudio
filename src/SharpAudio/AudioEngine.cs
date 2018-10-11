using System;
using System.Collections.Generic;
using System.Text;

namespace SharpAudio
{
    /// <summary>
    /// Represents an abstract audio device, capable of creating device resources and executing commands.
    /// </summary>
    public abstract class AudioEngine : IDisposable
    {
        /// <summary>
        /// Gets a value identifying the specific graphics API used by this instance.
        /// </summary>
        public abstract AudioBackend BackendType { get; }

        /// <summary>
        /// Creates a new <see cref="AudioEngine"/> using XAudio 2.
        /// </summary>
        /// <returns>A new <see cref="AudioEngine"/> using the XAudio 2 API.</returns>
        public static AudioEngine CreateXAudio(AudioEngineOptions options)
        {
            return new XA2.XA2Engine(options);
        }

        /// <summary>
        /// Creates a new <see cref="AudioBuffer"/> with this engine.
        /// </summary>
        /// <returns>A new <see cref="AudioBuffer"/></returns>
        public abstract AudioBuffer CreateBuffer();

        public abstract AudioSource CreateSource();


        /// <summary>
        /// Performs API-specific disposal of resources controlled by this instance.
        /// </summary>
        protected abstract void PlatformDispose();

        public void Dispose()
        {
            PlatformDispose();
        }
    }
}
