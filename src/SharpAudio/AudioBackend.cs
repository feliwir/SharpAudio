using System;
using System.Collections.Generic;
using System.Text;

namespace SharpAudio
{
    /// <summary>
    /// The specific graphics API used by the <see cref="AudioEngine"/>.
    /// </summary>
    public enum AudioBackend
    {
        // <summary>
        /// XAudio2
        /// </summary>
        XAudio2,
        // <summary>
        /// Alsa
        /// </summary>
        Alsa,
        // <summary>
        /// CoreAudio
        /// </summary>
        CoreAudio,
    }
}
