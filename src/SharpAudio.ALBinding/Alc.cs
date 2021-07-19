using System;
using System.Runtime.InteropServices;

#pragma warning disable CS1591

namespace SharpAudio.ALBinding
{
    public static unsafe partial class AlNative
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

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        private delegate IntPtr ALC_openDevice_t(string name);
        private static ALC_openDevice_t s_alc_openDevice;
        public static IntPtr alcOpenDevice(string name) => s_alc_openDevice(name);

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        private delegate void ALC_closeDevice_t(IntPtr handle);
        private static ALC_closeDevice_t s_alc_closeDevice;
        public static void alcCloseDevice(IntPtr handle) => s_alc_closeDevice(handle);

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        private delegate int ALC_getError_t(IntPtr device);
        private static ALC_getError_t s_alc_getError;
        public static int alcGetError(IntPtr device) => s_alc_getError(device);

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        private delegate IntPtr ALC_getString_t(IntPtr device, int param);
        private static ALC_getString_t s_alc_getString;
        public static IntPtr alcGetString(IntPtr device, int param) => s_alc_getString(device, param);


        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        private delegate IntPtr ALC_createContext_t(IntPtr device, int[] attribs);
        private static ALC_createContext_t s_alc_createContext;
        public static IntPtr alcCreateContext(IntPtr device, int[] attribs) => s_alc_createContext(device, attribs);

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        private delegate void ALC_makeContextCurrent_t(IntPtr context);
        private static ALC_makeContextCurrent_t s_alc_makeContextCurrent;
        public static void alcMakeContextCurrent(IntPtr context) => s_alc_makeContextCurrent(context);

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        private delegate void ALC_destroyContext_t(IntPtr context);
        private static ALC_destroyContext_t s_alc_destroyContext;
        public static void alcDestroyContext(IntPtr context) => s_alc_destroyContext(context);


        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        private delegate void ALC_processContext_t(IntPtr context);
        private static ALC_processContext_t s_alc_processContext;
        public static void alcProcessContext(IntPtr context) => s_alc_processContext(context);


        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        private delegate void ALC_suspendContext_t(IntPtr context);
        private static ALC_suspendContext_t s_alc_suspendContext;
        public static void alcSuspendContext(IntPtr context) => s_alc_suspendContext(context);


        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        private delegate IntPtr ALC_GetCurrentContext_t();
        private static ALC_GetCurrentContext_t s_alc_getCurrentContext;
        public static IntPtr alcGetCurrentContext() => s_alc_getCurrentContext();

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        private delegate IntPtr ALC_GetCurrentDevice_t(IntPtr context);
        private static ALC_GetCurrentDevice_t s_alc_getCurrentDevice;
        public static IntPtr alcGetContextsDevice(IntPtr context) => s_alc_getCurrentDevice(context);



        private static void LoadAlc()
        {
            s_alc_openDevice = LoadFunction<ALC_openDevice_t>("alcOpenDevice");
            s_alc_closeDevice = LoadFunction<ALC_closeDevice_t>("alcCloseDevice");

            s_alc_getError = LoadFunction<ALC_getError_t>("alcGetError");
            s_alc_getString = LoadFunction<ALC_getString_t>("alcGetString");

            s_alc_createContext = LoadFunction<ALC_createContext_t>("alcCreateContext");
            s_alc_destroyContext = LoadFunction<ALC_destroyContext_t>("alcDestroyContext");

            s_alc_processContext = LoadFunction<ALC_processContext_t>("alcProcessContext");
            s_alc_suspendContext = LoadFunction<ALC_suspendContext_t>("alcSuspendContext");

            s_alc_makeContextCurrent = LoadFunction<ALC_makeContextCurrent_t>("alcMakeContextCurrent");

            s_alc_getCurrentContext = LoadFunction<ALC_GetCurrentContext_t>("alcGetCurrentContext");
            s_alc_getCurrentDevice = LoadFunction<ALC_GetCurrentDevice_t>("alcGetContextsDevice");
        }
    }
}
