using System;
using System.Runtime.InteropServices;

#pragma warning disable CS1591

namespace SharpAudio.ALBinding
{
    public static partial class AlNative
    {
        public const int ALC_FALSE = 0x0000;
        public const int ALC_TRUE = 0x0001;
        public const int ALC_FREQUENCY = 0x1007;
        public const int ALC_REFRESH = 0x1008;
        public const int ALC_SYNC = 0x1009;

        public const int ALC_NO_ERROR = 0x0000;
        public const int ALC_INVALID_DEVICE = 0xA001;
        public const int ALC_INVALID_CONTEXT = 0xA002;
        public const int ALC_INVALID_ENUM = 0xA003;
        public const int ALC_INVALID_VALUE = 0xA004;
        public const int ALC_OUT_OF_MEMORY = 0xA005;

        public const int ALC_ATTRIBUTES_SIZE = 0x1002;
        public const int ALC_ALL_ATTRIBUTES = 0x1003;
        public const int ALC_DEFAULT_DEVICE_SPECIFIER = 0x1004;
        public const int ALC_DEVICE_SPECIFIER = 0x1005;
        public const int ALC_EXTENSIONS = 0x1006;
        private static ALC_openDevice_t s_alc_openDevice;
        private static ALC_closeDevice_t s_alc_closeDevice;
        private static ALC_getError_t s_alc_getError;
        private static ALC_createContext_t s_alc_createContext;
        private static ALC_makeContextCurrent_t s_alc_makeContextCurrent;
        private static ALC_destroyContext_t s_alc_destroyContext;

        public static IntPtr alcOpenDevice(string name)
        {
            return s_alc_openDevice(name);
        }

        public static void alcCloseDevice(IntPtr handle)
        {
            s_alc_closeDevice(handle);
        }

        public static int alcGetError(IntPtr device)
        {
            return s_alc_getError(device);
        }

        public static IntPtr alcCreateContext(IntPtr device, int[] attribs)
        {
            return s_alc_createContext(device, attribs);
        }

        public static void alcMakeContextCurrent(IntPtr handle)
        {
            s_alc_makeContextCurrent(handle);
        }

        public static void alcDestroyContext(IntPtr handle)
        {
            s_alc_destroyContext(handle);
        }

        private static void LoadAlc()
        {
            s_alc_openDevice = LoadFunction<ALC_openDevice_t>("alcOpenDevice");
            s_alc_closeDevice = LoadFunction<ALC_closeDevice_t>("alcCloseDevice");

            s_alc_getError = LoadFunction<ALC_getError_t>("alcGetError");

            s_alc_createContext = LoadFunction<ALC_createContext_t>("alcCreateContext");
            s_alc_destroyContext = LoadFunction<ALC_destroyContext_t>("alcDestroyContext");

            s_alc_makeContextCurrent = LoadFunction<ALC_makeContextCurrent_t>("alcMakeContextCurrent");
        }

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        private delegate IntPtr ALC_openDevice_t(string name);

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        private delegate void ALC_closeDevice_t(IntPtr handle);

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        private delegate int ALC_getError_t(IntPtr device);

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        private delegate IntPtr ALC_createContext_t(IntPtr device, int[] attribs);

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        private delegate void ALC_makeContextCurrent_t(IntPtr context);

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        private delegate void ALC_destroyContext_t(IntPtr context);
    }
}