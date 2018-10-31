using SharpAudio.Util.Mp3;
using SharpAudio.Util.Vorbis;
using SharpAudio.Util.Wave;
using System;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace SharpAudio.Util
{
    public class SoundStream : IDisposable
    {
        private Decoder _decoder;
        private AudioSource _source;
        private BufferChain _chain;
        private AudioBuffer _buffer;
        private bool _streamed;
        private byte[] _data;
        private Stopwatch _timer;

        private static byte[] MakeFourCC(string magic)
        {
            return new[] {  (byte)magic[0],
                            (byte)magic[1],
                            (byte)magic[2],
                            (byte)magic[3]};
        }

        /// <summary>
        /// The audio format of this stream
        /// </summary>
        public AudioFormat Format => _decoder.Format;

        /// <summary>
        /// The underlying source
        /// </summary>
        public AudioSource Source => _source;

        /// <summary>
        /// Wether or not the audio is finished
        /// </summary>
        public bool IsPlaying => _source.IsPlaying();

        /// <summary>
        /// Wether or not the audio is streamed
        /// </summary>
        public bool IsStreamed => _streamed;

        /// <summary>
        /// The volume of the source
        /// </summary>
        public float Volume
        {
            get => _source.Volume;
            set => _source.Volume = value;
        }

        /// <summary>
        /// Duration when provided by the decoder. Otherwise 0
        /// </summary>
        public TimeSpan Duration => _decoder.Duration;

        /// <summary>
        /// Current position inside the stream
        /// </summary>
        public TimeSpan Position => _timer.Elapsed;

        /// <summary>
        /// Initializes a new instance of the <see cref="SoundStream"/> class.
        /// </summary>
        /// <param name="stream">The sound stream.</param>
        public SoundStream(Stream stream, AudioEngine engine)
        {
            if (stream == null)
                throw new ArgumentNullException("Stream cannot be null!");

            _streamed = false;
            var fourcc = stream.ReadFourCc();
            stream.Seek(0, SeekOrigin.Begin);

            if (fourcc.SequenceEqual(MakeFourCC("RIFF")))
            {
                _decoder = new WaveDecoder(stream);
            }
            else if (fourcc.SequenceEqual(MakeFourCC("ID3\u0003")) ||
                    fourcc.SequenceEqual(new byte[] { 0xFF, 0xFB, 0xE0, 0x64 }))
            {
                _decoder = new Mp3Decoder(stream);
                _streamed = true;
            }
            else if (fourcc.SequenceEqual(MakeFourCC("OggS")))
            {
                _decoder = new VorbisDecoder(stream);
                _streamed = true;
            }
            else
            {
                throw new InvalidDataException("Unknown format: " + fourcc);
            }

            _source = engine.CreateSource();

            if (_streamed)
            {
                _chain = new BufferChain(engine);
                _decoder.GetSamples(TimeSpan.FromSeconds(1), ref _data);
                _chain.QueryData(_source, _data, _decoder.Format);

                _decoder.GetSamples(TimeSpan.FromSeconds(1), ref _data);
                _chain.QueryData(_source, _data, _decoder.Format);
            }
            else
            {
                _buffer = engine.CreateBuffer();
                _decoder.GetSamples(ref _data);
                _buffer.BufferData(_data, _decoder.Format);
                _source.QueryBuffer(_buffer);
            }
        }

        /// <summary>
        /// Start playing the soundstream 
        /// </summary>
        public void Play()
        {
            _source.Play();
            _timer.Start();

            if (_streamed)
            {
                var t = Task.Run(() =>
                {
                    while (_source.IsPlaying())
                    {
                        if (_source.BuffersQueued < 3 && !_decoder.IsFinished)
                        {
                            _decoder.GetSamples(TimeSpan.FromSeconds(1), ref _data);
                            _chain.QueryData(_source, _data, Format);
                        }

                        Thread.Sleep(100);
                    }
                    _timer.Stop();
                });
            }
        }

        /// <summary>
        /// Stop the soundstream
        /// </summary>
        public void Stop()
        {
            _source.Stop();
            _timer.Stop();
        }

        public void Dispose()
        {
            _buffer?.Dispose();
            _source.Dispose();
        }
    }
}
