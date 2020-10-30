using System;
using System.Runtime.InteropServices;

#pragma warning disable CS1591

namespace SharpAudio.ALBinding
{
    public static partial class AlNative
    {
        /* typedef int ALenum; */
        public const int AL_NONE = 0x0000;
        public const int AL_FALSE = 0x0000;
        public const int AL_TRUE = 0x0001;

        public const int AL_SOURCE_RELATIVE = 0x0202;

        public const int AL_CONE_INNER_ANGLE = 0x1001;
        public const int AL_CONE_OUTER_ANGLE = 0x1002;

        public const int AL_PITCH = 0x1003;
        public const int AL_POSITION = 0x1004;
        public const int AL_DIRECTION = 0x1005;
        public const int AL_VELOCITY = 0x1006;
        public const int AL_LOOPING = 0x1007;
        public const int AL_BUFFER = 0x1009;
        public const int AL_GAIN = 0x100A;
        public const int AL_MIN_GAIN = 0x100D;
        public const int AL_MAX_GAIN = 0x100E;
        public const int AL_ORIENTATION = 0x100F;

        public const int AL_SOURCE_STATE = 0x1010;
        public const int AL_INITIAL = 0x1011;
        public const int AL_PLAYING = 0x1012;
        public const int AL_PAUSED = 0x1013;
        public const int AL_STOPPED = 0x1014;

        public const int AL_BUFFERS_QUEUED = 0x1015;
        public const int AL_BUFFERS_PROCESSED = 0x1016;

        public const int AL_REFERENCE_DISTANCE = 0x1020;
        public const int AL_ROLLOFF_FACTOR = 0x1021;
        public const int AL_CONE_OUTER_GAIN = 0x1022;

        public const int AL_MAX_DISTANCE = 0x1023;

        public const int AL_SOURCE_TYPE = 0x1027;
        public const int AL_STATIC = 0x1028;
        public const int AL_STREAMING = 0x1029;
        public const int AL_UNDETERMINED = 0x1030;

        public const int AL_FORMAT_MONO8 = 0x1100;
        public const int AL_FORMAT_MONO16 = 0x1101;
        public const int AL_FORMAT_STEREO8 = 0x1102;
        public const int AL_FORMAT_STEREO16 = 0x1103;

        public const int AL_FREQUENCY = 0x2001;
        public const int AL_BITS = 0x2002;
        public const int AL_CHANNELS = 0x2003;
        public const int AL_SIZE = 0x2004;

        public const int AL_NO_ERROR = 0x0000;
        public const int AL_INVALID_NAME = 0xA001;
        public const int AL_INVALID_ENUM = 0xA002;
        public const int AL_INVALID_VALUE = 0xA003;
        public const int AL_INVALID_OPERATION = 0xA004;
        public const int AL_OUT_OF_MEMORY = 0xA005;

        public const int AL_VENDOR = 0xB001;
        public const int AL_VERSION = 0xB002;
        public const int AL_RENDERER = 0xB003;
        public const int AL_EXTENSIONS = 0xB004;

        public const int AL_DOPPLER_FACTOR = 0xC000;

        /* Deprecated! */
        public const int AL_DOPPLER_VELOCITY = 0xC001;

        public const int AL_DISTANCE_MODEL = 0xD000;
        public const int AL_INVERSE_DISTANCE = 0xD001;
        public const int AL_INVERSE_DISTANCE_CLAMPED = 0xD002;
        private static AL_getError_t s_al_getError;
        private static AL_genBuffers_t s_al_genBuffers;
        private static AL_deleteBuffers_t s_al_deleteBuffers;
        private static AL_isBuffer_t s_al_isBuffer;
        private static AL_bufferData_t s_al_bufferData;
        private static AL_genSources_t s_al_genSources;
        private static AL_deleteSources_t s_al_deleteSources;
        private static AL_sourceQueueBuffers_t s_al_sourceQueueBuffers;
        private static AL_sourceUnqueueBuffers_t s_al_sourceUnqueueBuffers;
        private static AL_sourcePlay_t s_al_sourcePlay;
        private static AL_sourceStop_t s_al_sourceStop;
        private static AL_sourcei_t s_al_sourcei;
        private static AL_getSourcei_t s_al_getSourcei;
        private static AL_sourcef_t s_al_sourcef;
        private static AL_getSourcef_t s_al_getSourcef;
        private static AL_isExtensionPresent_t s_al_isExtensionPresent;

        public static int alGetError()
        {
            return s_al_getError();
        }

        public static void alGenBuffers(int n, uint[] buffers)
        {
            s_al_genBuffers(n, buffers);
        }

        public static void alDeleteBuffers(int n, uint[] buffers)
        {
            s_al_deleteBuffers(n, buffers);
        }

        public static bool alIsBuffer(uint buffer)
        {
            return s_al_isBuffer(buffer);
        }

        public static bool alBufferData(uint buffer, int format, IntPtr data, int size, int freq)
        {
            return s_al_bufferData(buffer, format, data, size, freq);
        }

        public static void alGenSources(int n, uint[] sources)
        {
            s_al_genSources(n, sources);
        }

        public static void alDeleteSources(int n, uint[] sources)
        {
            s_al_deleteSources(n, sources);
        }

        public static void alSourceQueueBuffers(uint source, int n, uint[] sources)
        {
            s_al_sourceQueueBuffers(source, n, sources);
        }

        public static void alSourceUnqueueBuffers(uint source, int n, uint[] sources)
        {
            s_al_sourceUnqueueBuffers(source, n, sources);
        }

        public static void alSourcePlay(uint source)
        {
            s_al_sourcePlay(source);
        }

        public static void alSourceStop(uint source)
        {
            s_al_sourceStop(source);
        }

        public static void alSourcei(uint source, int param, int value)
        {
            s_al_sourcei(source, param, value);
        }

        public static void alGetSourcei(uint source, int param, out int value)
        {
            s_al_getSourcei(source, param, out value);
        }

        public static void alSourcef(uint source, int param, float value)
        {
            s_al_sourcef(source, param, value);
        }

        public static void alGetSourcef(uint source, int param, out float value)
        {
            s_al_getSourcef(source, param, out value);
        }

        public static bool alIsExtensionPresent(string extname)
        {
            return s_al_isExtensionPresent(extname);
        }


        private static void LoadAl()
        {
            s_al_getError = LoadFunction<AL_getError_t>("alGetError");

            s_al_isExtensionPresent = LoadFunction<AL_isExtensionPresent_t>("alIsExtensionPresent");

            s_al_genBuffers = LoadFunction<AL_genBuffers_t>("alGenBuffers");
            s_al_deleteBuffers = LoadFunction<AL_deleteBuffers_t>("alDeleteBuffers");
            s_al_isBuffer = LoadFunction<AL_isBuffer_t>("alIsBuffer");

            s_al_bufferData = LoadFunction<AL_bufferData_t>("alBufferData");

            s_al_genSources = LoadFunction<AL_genSources_t>("alGenSources");
            s_al_deleteSources = LoadFunction<AL_deleteSources_t>("alDeleteSources");

            s_al_sourcePlay = LoadFunction<AL_sourcePlay_t>("alSourcePlay");
            s_al_sourceStop = LoadFunction<AL_sourceStop_t>("alSourceStop");
            s_al_sourceQueueBuffers = LoadFunction<AL_sourceQueueBuffers_t>("alSourceQueueBuffers");
            s_al_sourceUnqueueBuffers = LoadFunction<AL_sourceUnqueueBuffers_t>("alSourceUnqueueBuffers");

            s_al_sourcei = LoadFunction<AL_sourcei_t>("alSourcei");
            s_al_getSourcei = LoadFunction<AL_getSourcei_t>("alGetSourcei");

            s_al_sourcef = LoadFunction<AL_sourcef_t>("alSourcef");
            s_al_getSourcef = LoadFunction<AL_getSourcef_t>("alGetSourcef");
        }

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        private delegate int AL_getError_t();

        /* n refers to an ALsizei */
        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        private delegate void AL_genBuffers_t(int n, uint[] buffers);

        /* n refers to an ALsizei */
        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        private delegate void AL_deleteBuffers_t(int n, uint[] buffers);

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        private delegate bool AL_isBuffer_t(uint buffer);

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        private delegate bool AL_bufferData_t(uint buffer, int format, IntPtr data, int size, int freq);

        /* n refers to an ALsizei */
        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        private delegate void AL_genSources_t(int n, uint[] sources);

        /* n refers to an ALsizei */
        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        private delegate void AL_deleteSources_t(int n, uint[] sources);

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        private delegate void AL_sourceQueueBuffers_t(uint source, int n, uint[] buffers);

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        private delegate void AL_sourceUnqueueBuffers_t(uint source, int n, uint[] buffers);

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        private delegate void AL_sourcePlay_t(uint source);

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        private delegate void AL_sourceStop_t(uint source);

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        private delegate void AL_sourcei_t(uint source, int param, int value);

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        private delegate void AL_getSourcei_t(uint source, int param, out int value);

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        private delegate void AL_sourcef_t(uint source, int param, float value);

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        private delegate void AL_getSourcef_t(uint source, int param, out float value);

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        private delegate bool AL_isExtensionPresent_t(string extname);
    }
}