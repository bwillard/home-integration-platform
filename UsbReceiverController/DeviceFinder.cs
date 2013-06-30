using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.InteropServices;
using System.ComponentModel;

namespace HomeAutomation.UsbReceiverController
{
    class DeviceFinder
    {
        private const int BUFFER_SIZE = 256;

        [FlagsAttribute]
        private enum DiGetClassFlags : uint
        {
            DIGCF_DEFAULT = 0x00000001,  // only valid with DIGCF_DEVICEINTERFACE
            DIGCF_PRESENT = 0x00000002,
            DIGCF_ALLCLASSES = 0x00000004,
            DIGCF_PROFILE = 0x00000008,
            DIGCF_DEVICEINTERFACE = 0x00000010,
        }

        [StructLayout(LayoutKind.Sequential)]
        private class SP_DEVINFO_DATA
        {
            public uint cbSize;
            public Guid ClassGuid;
            public uint DevInst;
            public IntPtr Reserved;
        }

        [StructLayout(LayoutKind.Sequential)]
        private class SP_DEVICE_INTERFACE_DATA
        {
            public uint cbSize;
            public Guid InterfaceClassGuid;
            public uint Flags;
            public IntPtr Reserved;
        }

        [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Auto)]
        private struct SP_DEVICE_INTERFACE_DETAIL_DATA
        {
            public int cbSize;
            [MarshalAs(UnmanagedType.ByValTStr, SizeConst = BUFFER_SIZE)]
            public string DevicePath;
        }


        [DllImport("setupapi.dll", CharSet = CharSet.Auto)]
        private static extern IntPtr SetupDiGetClassDevs(
                                                      ref Guid classGuid,
                                                      [MarshalAs(UnmanagedType.LPTStr)] string enumerator,
                                                      IntPtr hwndParent,
                                                      DiGetClassFlags flags
                                                     );

        [DllImport(@"setupapi.dll", CharSet = CharSet.Auto, SetLastError = true)]
        private static extern Boolean SetupDiEnumDeviceInterfaces(
           IntPtr hDevInfo,
           SP_DEVINFO_DATA devInfo,
           ref Guid interfaceClassGuid,
           UInt32 memberIndex,
           SP_DEVICE_INTERFACE_DATA deviceInterfaceData
        );

        [DllImport(@"setupapi.dll", CharSet = CharSet.Auto, SetLastError = true)]
        private static extern Boolean SetupDiGetDeviceInterfaceDetail(
           IntPtr hDevInfo,
           SP_DEVICE_INTERFACE_DATA deviceInterfaceData,
           ref SP_DEVICE_INTERFACE_DETAIL_DATA deviceInterfaceDetailData,
           UInt32 deviceInterfaceDetailDataSize,
           out UInt32 requiredSize,
           SP_DEVINFO_DATA deviceInfoData
        );

        [DllImport("winusb.dll", SetLastError = true, CharSet = CharSet.Auto)]
        private static extern bool WinUsb_Initialize(
             IntPtr deviceHandle,
             out IntPtr interfaceHandle
             );

        [DllImport(@"setupapi.dll", CharSet = CharSet.Auto, SetLastError = true)]
        private static extern bool SetupDiDestroyDeviceInfoList(IntPtr hDevInfo);

        public string GetDevicePath(Guid classGuid)
        {
            IntPtr hDevInfo = IntPtr.Zero;
            try
            {
                hDevInfo = SetupDiGetClassDevs(ref classGuid, null, IntPtr.Zero, DiGetClassFlags.DIGCF_DEVICEINTERFACE | DiGetClassFlags.DIGCF_PRESENT);

                if (hDevInfo.ToInt64() <= 0)
                {
                    throw new Win32Exception(Marshal.GetLastWin32Error());
                }

                SP_DEVICE_INTERFACE_DATA dia = new SP_DEVICE_INTERFACE_DATA();
                dia.cbSize = (uint)Marshal.SizeOf(dia);

                SP_DEVINFO_DATA devInfo = new SP_DEVINFO_DATA();
                devInfo.cbSize = (UInt32)Marshal.SizeOf(devInfo);

                UInt32 i = 0;

                // start the enumeration 
                if (!SetupDiEnumDeviceInterfaces(hDevInfo, null, ref classGuid, i, dia))
                {
                    throw new Win32Exception(Marshal.GetLastWin32Error());
                }

                SP_DEVICE_INTERFACE_DETAIL_DATA didd = new SP_DEVICE_INTERFACE_DETAIL_DATA();
                didd.cbSize = 4 + Marshal.SystemDefaultCharSize; // trust me :)

                UInt32 requiredSize = 0;

                if (!SetupDiGetDeviceInterfaceDetail(hDevInfo, dia, ref didd, 256, out requiredSize, devInfo))
                {
                    throw new Win32Exception(Marshal.GetLastWin32Error());
                }

                return didd.DevicePath;


            }
            finally
            {
                SetupDiDestroyDeviceInfoList(hDevInfo);
            }
        }
    }
}
