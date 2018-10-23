using SharpAudio.Util;
using System;
using System.IO;
using System.Threading;
using CommandLine;
using System.Collections.Generic;

namespace SharpAudio.Sample
{
    class Program
    {
        public class Options
        {
            [Option('i', "input", Required = true, HelpText = "Specify the file(s) that should be played")]
            public IEnumerable<string> InputFiles { get; set; }

            [Option('v', "volume", Required = false, HelpText = "Set the output volume (0-100).",Default=100)]
            public int Volume { get; set; }
        }

        static void Main(string[] args)
        {
            CommandLine.Parser.Default.ParseArguments<Options>(args)
              .WithParsed<Options>(opts => RunOptionsAndReturnExitCode(opts));       
        }

        private static void RunOptionsAndReturnExitCode(Options opts)
        {
            var engine = AudioEngine.CreateDefault();

            foreach (var file in opts.InputFiles)
            {
                var soundStream = new SoundStream(File.OpenRead(file));

                var data = soundStream.ReadSamples(TimeSpan.FromSeconds(5));

                float duration = data.Length / (float)soundStream.Format.BytesPerSecond;

                var buffer = engine.CreateBuffer();
                buffer.BufferData(data, soundStream.Format, data.Length);

                var source = engine.CreateSource();
                source.QueryBuffer(buffer);
                source.Volume = opts.Volume / 100.0f;

                source.Play();

                while (source.IsPlaying())
                {
                    Thread.Sleep(100);
                }
            }
        }
    }
}
