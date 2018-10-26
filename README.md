# SharpAudio
SharpAudio is a cross-platform, backend-agnostic library to playback sounds in .NET. It achieves that by wrapping the platform specific backends.

Supported backends:
- XAudio2
- OpenAL

# Structure

SharpAudio provides a low-level interface that wraps audio sources & buffers. A high level interface that can load and play sound files is provided in the SharpAudio.Util package. The following sound formats are supported at the moment:
- `.wav` (PCM & ADPCM)
- `.mp3` 


# Build status

[![Build status](https://ci.appveyor.com/api/projects/status/c3e98wk0mwcsje00?svg=true)](https://ci.appveyor.com/project/feliwir/sharpaudio)
[![MyGet Badge](https://buildstats.info/myget/feliwir/SharpAudio)](https://www.myget.org/feed/feliwir/package/nuget/SharpAudio)