using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
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
        public static AudioEngine CreateXAudio()
        {
            return CreateXAudio(new AudioEngineOptions());
        }

        /// <summary>
        /// Creates a new <see cref="AudioEngine"/> using XAudio 2.
        /// </summary>
        /// <param name="options">the settings for this audio engine</param>
        /// <returns>A new <see cref="AudioEngine"/> using the XAudio 2 API.</returns>
        public static AudioEngine CreateXAudio(AudioEngineOptions options)
        {
            return new XA2.XA2Engine(options);
        }

        /// <summary>
        /// Creates a new <see cref="AudioEngine"/> using OpenAL.
        /// </summary>
        /// <returns>A new <see cref="AudioEngine"/> using the openal API.</returns>
        public static AudioEngine CreateOpenAL()
        {
            return CreateOpenAL(new AudioEngineOptions());
        }

        /// <summary>
        /// Creates a new <see cref="AudioEngine"/> using OpenAL.
        /// </summary>
        /// <param name="options">the settings for this audio engine</param>
        /// <returns>A new <see cref="AudioEngine"/> using the openal API.</returns>
        public static AudioEngine CreateOpenAL(AudioEngineOptions options)
        {
            return new AL.ALEngine(options);
        }

        /// <summary>
        /// Creates a new <see cref="AudioEngine"/> using OpenAL.
        /// </summary>
        /// <returns>A new <see cref="AudioEngine"/> using the openal API.</returns>
        public static AudioEngine CreateDefault()
        {
            return CreateDefault(new AudioEngineOptions());
        }

        /// <summary>
        /// Create the default backend for the current operating system
        /// </summary>
        /// <param name="options">the settings for this audio engine</param>
        /// <returns></returns>
        public static AudioEngine CreateDefault(AudioEngineOptions options)
        {
            if (RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
                return CreateXAudio(options);
            else
                return CreateOpenAL(options);
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
