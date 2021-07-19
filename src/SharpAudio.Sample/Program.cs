using System;
using System.Collections.Generic;
using System.IO;
using System.Threading;
using CommandLine;
using SharpAudio.Codec;

namespace SharpAudio.Sample
{
    class Program
    {
        public class Options
        {
            [Option('i', "input", Required = true, HelpText = "Specify the file(s) that should be played")]
            public IEnumerable<string> InputFiles { get; set; }

            [Option('v', "volume", Required = false, HelpText = "Set the output volume (0-100).", Default = 100)]
            public int Volume { get; set; }
        }

        static void Main(string[] args)
        {
            CommandLine.Parser.Default.ParseArguments<Options>(args)
              .WithParsed<Options>(opts => RunOptionsAndReturnExitCode(opts));
        }

        private static void RunOptionsAndReturnExitCode(Options opts)
        {
            using (var engine = AudioEngine.CreateDefault())
            {

                if (engine == null)
                {
                    Console.WriteLine("Failed to create an audio backend!");
                }

                foreach (var file in opts.InputFiles)
                {
                    var soundStream = new SoundStream(File.OpenRead(file), engine);

                    soundStream.Volume = opts.Volume / 100.0f;

                    soundStream.Play();

                    Console.WriteLine("Playing file with duration: " + soundStream.Duration);

                    while (soundStream.IsPlaying)
                    {
                        Thread.Sleep(100);
                    }
                }
            }
        }
    }
}
