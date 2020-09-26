using System;
using System.Collections.Generic;
using System.Numerics;
using SharpDX.X3DAudio;
using SharpDX.Mathematics.Interop;
using SharpDX.XAudio2;
using SharpDX.Multimedia;

namespace SharpAudio.XA2
{
    class XA23DEngine : Audio3DEngine
    {
        X3DAudio _x3DAudio;
        Listener _x3DListener;
        Dictionary<AudioSource, Emitter> _x3DEmitters;
        private readonly XA2Engine _engine;

        public XA23DEngine(XA2Engine engine)
        {
            _engine = engine;
            _x3DListener = new Listener();
            _x3DListener.OrientTop = new RawVector3(0, 0, 1);
            _x3DListener.OrientFront = new RawVector3(0, 1, 0);

            Speakers channels = (Speakers) _engine.MasterVoice.ChannelMask;
            _x3DAudio = new X3DAudio(channels);
            _x3DEmitters = new Dictionary<AudioSource, Emitter>();
        }

        public override void Dispose()
        {
        }

        public override void SetListenerPosition(Vector3 position)
        {
            _x3DListener.Position = new RawVector3(position.X, position.Y, position.Z);
        }

        public override void SetSourcePosition(AudioSource source, Vector3 position)
        {
            XA2Source xa2Source = (XA2Source) source;

            if (!_x3DEmitters.TryGetValue(source, out var emitter))
            {
                emitter = new Emitter();
                emitter.CurveDistanceScaler = 10.0f;
                emitter.OrientTop = new RawVector3(0, 0, 1);
                emitter.OrientFront = new RawVector3(0, 1, 0);

                _x3DEmitters.Add(source, emitter);
            }

            emitter.ChannelCount = xa2Source.SourceVoice.VoiceDetails.InputChannelCount;
            emitter.Position = new RawVector3(position.X, position.Y, position.Z);

            var outChannels = _engine.MasterVoice.VoiceDetails.InputChannelCount;

            DspSettings dspSettings = new DspSettings(1, outChannels);

            _x3DAudio.Calculate(_x3DListener, emitter, CalculateFlags.Matrix, dspSettings);
            xa2Source.SourceVoice.SetOutputMatrix(_engine.MasterVoice, 1, outChannels, dspSettings.MatrixCoefficients);
        }
    }
}
