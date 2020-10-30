using System;
using System.Linq;

namespace SharpAudio.Tests
{
    [XunitTestCaseDiscoverer("SharpAudio.Tests.BackendDiscoverer", "SharpAudio.Tests")]
    [AttributeUsage(AttributeTargets.Method)]
    public sealed class BackendFactAttribute : FactAttribute
    {
        public readonly AudioBackend[] Backends;

        public BackendFactAttribute(AudioBackend backend, params AudioBackend[] otherBackends)
        {
            Backends = new[] {backend}.Union(otherBackends).ToArray();
        }
    }
}