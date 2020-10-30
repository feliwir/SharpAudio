﻿using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;

namespace SharpAudio.Tests
{
    public sealed class BackendDiscoverer : IXunitTestCaseDiscoverer
    {
        private static readonly ISet<AudioBackend> AvailableBackends;
        private readonly IMessageSink _diagnosticMessageSink;

        static BackendDiscoverer()
        {
            AvailableBackends = new HashSet<AudioBackend>();
            if (RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
            {
                var engine = AudioEngine.CreateXAudio();
                if (engine != null) AvailableBackends.Add(AudioBackend.XAudio2);
            }
            else if (RuntimeInformation.IsOSPlatform(OSPlatform.Linux))
            {
                var engine = AudioEngine.CreateOpenAL();
                if (engine != null) AvailableBackends.Add(AudioBackend.OpenAL);
            }
        }

        public BackendDiscoverer(IMessageSink diagnosticMessageSink)
        {
            _diagnosticMessageSink = diagnosticMessageSink;
        }

        public IEnumerable<IXunitTestCase> Discover(ITestFrameworkDiscoveryOptions discoveryOptions,
            ITestMethod testMethod, IAttributeInfo factAttribute)
        {
            var arguments = factAttribute.GetConstructorArguments().ToList();
            var backend = (AudioBackend) arguments[0];
            var otherBackends = (AudioBackend[]) arguments[1];

            var backends = new[] {backend}.Union(otherBackends).ToArray();

            if (!AvailableBackends.Any(x => backends.Contains(x)))
            {
                var backendNames = string.Join(", ", backends.Select(x => x.ToString()));

                _diagnosticMessageSink.OnMessage(
                    new DiagnosticMessage(
                        $"Skipped test {testMethod.TestClass.Class.Name}.{testMethod.Method.Name}, because it requires one or more of the following backends to be installed: {backendNames}.")
                );
                yield break;
            }

            yield return new XunitTestCase(
                _diagnosticMessageSink,
                discoveryOptions.MethodDisplayOrDefault(),
                TestMethodDisplayOptions.All,
                testMethod);
        }
    }
}