﻿using System.Collections.Generic;
using System.Numerics;

namespace SharpAudio.XA2
{
    internal sealed class XA23DEngine : Audio3DEngine
    {
        private readonly XA2Engine _engine;
        private X3DAudio _x3DAudio;
        private readonly Dictionary<AudioSource, Emitter> _x3DEmitters;
        private Listener _x3DListener;

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

        public override void SetListenerPosition(Vector3 position)
        {
            _x3DListener.Position = new RawVector3(position.X, position.Y, position.Z);
        }

        public override void SetSourcePosition(AudioSource source, Vector3 position)
        {
            var xa2Source = (XA2Source) source;

            if (!_x3DEmitters.TryGetValue(source, out var emitter))
            {
                emitter = new Emitter();
                emitter.CurveDistanceScaler = float.MinValue;
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

        public override void SetListenerOrientation(Vector3 top, Vector3 front)
        {
            _x3DListener.OrientTop = new RawVector3(top.X, top.Y, top.Z);
            _x3DListener.OrientFront = new RawVector3(front.X, front.Y, front.Z);
        }
    }
}