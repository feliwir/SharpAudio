namespace SharpAudio
{
    public class AudioEngineOptions
    {
        /// <summary>
        /// The sample rate to which all sources will be resampled to (if required). 
        /// Then coverts to the maximum supported sample rate by the device.
        /// </summary>
        public int SampleRate;

        /// <summary>
        /// The number of channels that this device is sammpling
        /// </summary>
        public int SampleChannels;

        public AudioEngineOptions() : this(44100, 2)
        {

        }

        public AudioEngineOptions(int sampleRate, int sampleChannels)
        {
            SampleRate = sampleRate;
            SampleChannels = sampleChannels;
        }
    }
}
