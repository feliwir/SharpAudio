# SharpAudio
SharpAudio is a cross-platform, backend-agnostic library to playback sounds in .NET. It achieves that by wrapping the platform specific backends.

Supported backends:
- XAudio2
- OpenAL


# Build status

[![Build Status](https://github.com/feliwir/SharpAudio/workflows/CI/badge.svg)](https://github.com/feliwir/SharpAudio/actions)
[![MyGet Badge](https://buildstats.info/myget/feliwir/SharpAudio)](https://www.myget.org/feed/feliwir/package/nuget/SharpAudio)

# Example

SharpAudio provides a low-level interface that wraps audio sources & buffers:
```csharp
    var engine = AudioEngine.CreateDefault();
    var buffer = engine.CreateBuffer();
    var source = engine.CreateSource();

    // Play a 1s long sound at 440hz
    AudioFormat format;
    format.BitsPerSample = 16;
    format.Channels = 1;
    format.SampleRate = 44100;
    float freq = 440.0f;
    var size = format.SampleRate;
    var samples = new short[size];

    for (int i = 0; i < size; i++)
    {
        samples[i] = (short)(32760 * Math.Sin((2 * Math.PI * freq) / size * i));
    }

    buffer.BufferData(samples, format);

    source.QueueBuffer(buffer);

    source.Play();
```

A high level interface that can load and play sound files is provided in the SharpAudio.Codec package:
```csharp
    var engine = AudioEngine.CreateDefault();
    var soundStream = new SoundStream(File.OpenRead("test.mp3"), engine);

    soundStream.Volume = 50.0f;
    soundStream.Play();
```

The following sound formats are supported at the moment:
- `.wav` (PCM & ADPCM)
- `.mp3` 
- `.ogg` (WIP: Vorbis & Opus)
