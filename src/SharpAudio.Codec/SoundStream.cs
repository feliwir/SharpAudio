using System;
using System.ComponentModel;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using SharpAudio.Codec.FFmpeg;
using SharpAudio.Codec.Mp3;
using SharpAudio.Codec.Vorbis;
using SharpAudio.Codec.Wave;
using SharpDX.Multimedia;

namespace SharpAudio.Codec
{
    public sealed class SoundStream : IDisposable, INotifyPropertyChanged
    {
        private byte[] _data;
        private Decoder _decoder;
        private readonly SoundSink _soundSink;
        private SoundStreamState _state = SoundStreamState.Paused;
        private static readonly TimeSpan SampleQuantum = TimeSpan.FromSeconds(0.05);

        /// <summary>
        ///     Initializes a new instance of the <see cref="SharpDX.Multimedia.SoundStream" /> class.
        /// </summary>
        /// <param name="stream">The sound stream.</param>
        /// <param name="engine">The audio engine</param>
        public SoundStream(Stream stream, SoundSink sink, bool autoDisposeSink = true)
        {
            if (stream == null)
            {
                throw new ArgumentNullException("Stream cannot be null!");
            }

            _targetStream = stream;
            _autoDisposeSink = autoDisposeSink;
            _soundSink = sink;

            if (stream == null)
            {
                throw new ArgumentNullException(nameof(stream));
            }

            IsStreamed = !stream.CanSeek;

            if (!IsStreamed)
            {
                _decoder = new FFmpegDecoder(stream);
            }
            else
            {
                var fourcc = stream.ReadFourCc();
                stream.Seek(0, SeekOrigin.Begin);

                if (fourcc.SequenceEqual(MakeFourCC("RIFF")))
                {
                    _decoder = new WaveDecoder(stream);
                }
                else if (fourcc.SequenceEqual(MakeFourCC("ID3\u0001")) ||
                         fourcc.SequenceEqual(MakeFourCC("ID3\u0002")) ||
                         fourcc.SequenceEqual(MakeFourCC("ID3\u0003")) ||
                         fourcc.AsSpan(0, 2).SequenceEqual(new byte[] { 0xFF, 0xFB }))
                {
                    _decoder = new Mp3Decoder(stream);
                }
                else if (fourcc.SequenceEqual(MakeFourCC("OggS")))
                {
                    _decoder = new VorbisDecoder(stream);
                }
                else
                {
                    _decoder = new FFmpegDecoder(stream);
                }
            }

            var streamThread = new Thread(MainLoop);

            streamThread.Start();
        }

        public event PropertyChangedEventHandler PropertyChanged;

        /// <summary>
        ///     Whether or not the audio is finished
        /// </summary>
        public bool IsPlaying => State == SoundStreamState.Playing;

        /// <summary>
        ///     Whether or not the audio is streamed
        /// </summary>
        public bool IsStreamed { get; }

        public AudioFormat Format => _decoder.Format;

        private readonly Stream _targetStream;

        /// <summary>
        ///     The volume of the source
        /// </summary>
        public float Volume
        {
            get => _soundSink?.Source.Volume ?? 0;
            set => _soundSink.Source.Volume = value;
        }

        /// <summary>
        ///     Duration when provided by the decoder. Otherwise 0
        /// </summary>
        public TimeSpan Duration => _decoder.Duration;

        /// <summary>
        ///     Current position inside the stream
        /// </summary>
        public TimeSpan Position => _decoder.Position;

        public static object stateLock = new object();
        private bool _autoDisposeSink;

        public SoundStreamState State
        {
            set
            {
                lock (stateLock)
                {
                    _state = value;
                }
            }
            get
            {
                lock (stateLock)
                {
                    return _state;
                }
            }
        }

        public void Dispose()
        {
            State = SoundStreamState.Stop;
        }

        public void TrySeek(TimeSpan seek)
        {
            _soundSink.ClearBuffers();
            _decoder.TrySeek(seek);
        }

        /// <summary>
        ///     Start playing the soundstream
        /// </summary>
        public void Play()
        {
            switch (State)
            {
                case SoundStreamState.Idle:
                    State = SoundStreamState.PreparePlay;
                    break;

                case SoundStreamState.PreparePlay:
                case SoundStreamState.Playing:
                    State = SoundStreamState.Paused;
                    break;

                case SoundStreamState.Paused:
                    State = SoundStreamState.Playing;
                    break;
            }

            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(State)));
        }

        private void MainLoop()
        {
            while (State != SoundStreamState.Stop & State != SoundStreamState.TrackFinished)
            {
                switch (State)
                {
                    case SoundStreamState.PreparePlay:
                        State = SoundStreamState.Paused;
                        break;

                    case SoundStreamState.Playing:
                        if (_soundSink.NeedsNewSample)
                        {
                            var res = _decoder.GetSamples(SampleQuantum, ref _data);

                            if (res == 0)
                            {
                                continue;
                            }

                            if (res == -1)
                            {
                                State = SoundStreamState.TrackFinished;
                                continue;
                            }

                            _soundSink.Send(_data);
                            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(Position)));
                        }

                        if (_decoder.IsFinished)
                        {
                            State = SoundStreamState.TrackFinished;
                            continue;
                        }
                        break;
                }

                Thread.Sleep(2);
            }

            if (_autoDisposeSink)
            {
                _soundSink?.Dispose();
            }

            _decoder.Dispose();
            _targetStream.Dispose();

            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(Position)));
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(State)));
        }

        /// <summary>
        ///     Stop the soundstream
        /// </summary>
        public void Stop()
        {
            State = SoundStreamState.Stop;
        }

        private static byte[] MakeFourCC(string magic)
        {
            return new[] {  (byte)magic[0],
                (byte)magic[1],
                (byte)magic[2],
                (byte)magic[3]};
        }
    }
}
