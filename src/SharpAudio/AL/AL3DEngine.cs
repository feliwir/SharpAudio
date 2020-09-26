using System;
using System.Numerics;

namespace SharpAudio.AL
{
    internal sealed class AL3DEngine : Audio3DEngine
    {
        public override void SetListenerOrientation(Vector3 top, Vector3 front)
        {
            throw new NotImplementedException();
        }

        public override void SetListenerPosition(Vector3 position)
        {
            throw new NotImplementedException();
        }

        public override void SetSourcePosition(AudioSource source, Vector3 position)
        {
            throw new NotImplementedException();
        }
    }
}
