using System;
using System.Runtime.InteropServices;

namespace SharpAudio.ALBinding
{
    public static unsafe partial class AlNative
    {
        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        private delegate IntPtr ALC_openDevice_t(string name);
        private static ALC_openDevice_t s_alc_openDevice = LoadFunction<ALC_openDevice_t>("alcOpenDevice");
        public static IntPtr alcOpenDevice(string name) => s_alc_openDevice(name);

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        private delegate void ALC_closeDevice_t(IntPtr handle);
        private static ALC_closeDevice_t s_alc_closeDevice = LoadFunction<ALC_closeDevice_t>("alcCloseDevice");
        public static void alcCloseDevice(IntPtr handle) => s_alc_closeDevice(handle);
    }
}
