using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace SharpAudio.Tests
{
    public class Capture
    {
        [BackendFact(AudioBackend.MediaFoundation)]
        public void CreateMediaFoundation()
        {
            TestCapture(AudioCapture.CreateDefault());
        }

        [BackendFact(AudioBackend.OpenAL)]
        public void CreateOpenAL()
        {
            using (var engine = AudioCapture.CreateOpenAL())
            {
                TestCapture(engine);
            }
        }

        void TestCapture(AudioCapture capture)
        {


            //wait since Play is non blocking
            Thread.Sleep(1000);
        }
    }
}
