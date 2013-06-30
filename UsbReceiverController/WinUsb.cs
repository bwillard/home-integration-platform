using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;
using System.Runtime.InteropServices;
using Microsoft.Win32.SafeHandles;

namespace HomeAutomation.UsbReceiverController
{
    class WinUsb
    {
        [FlagsAttribute]
        private enum DiGetClassFlags : uint
        {
            DIGCF_DEFAULT = 0x00000001,  // only valid with DIGCF_DEVICEINTERFACE
            DIGCF_PRESENT = 0x00000002,
            DIGCF_ALLCLASSES = 0x00000004,
            DIGCF_PROFILE = 0x00000008,
            DIGCF_DEVICEINTERFACE = 0x00000010,
        }

        [Flags]
        private enum EFileAccess : uint
        {
            /// <summary>
            /// 
            /// </summary>
            GenericRead = 0x80000000,
            /// <summary>
            /// 
            /// </summary>
            GenericWrite = 0x40000000,
            /// <summary>
            /// 
            /// </summary>
            GenericExecute = 0x20000000,
            /// <summary>
            /// 
            /// </summary>
            GenericAll = 0x10000000
        }

        [Flags]
        private enum EFileShare : uint
        {
            /// <summary>
            /// 
            /// </summary>
            None = 0x00000000,
            /// <summary>
            /// Enables subsequent open operations on an object to request read access. 
            /// Otherwise, other processes cannot open the object if they request read access. 
            /// If this flag is not specified, but the object has been opened for read access, the function fails.
            /// </summary>
            Read = 0x00000001,
            /// <summary>
            /// Enables subsequent open operations on an object to request write access. 
            /// Otherwise, other processes cannot open the object if they request write access. 
            /// If this flag is not specified, but the object has been opened for write access, the function fails.
            /// </summary>
            Write = 0x00000002,
            /// <summary>
            /// Enables subsequent open operations on an object to request delete access. 
            /// Otherwise, other processes cannot open the object if they request delete access.
            /// If this flag is not specified, but the object has been opened for delete access, the function fails.
            /// </summary>
            Delete = 0x00000004
        }

        private enum ECreationDisposition : uint
        {
            /// <summary>
            /// Creates a new file. The function fails if a specified file exists.
            /// </summary>
            New = 1,
            /// <summary>
            /// Creates a new file, always. 
            /// If a file exists, the function overwrites the file, clears the existing attributes, combines the specified file attributes, 
            /// and flags with FILE_ATTRIBUTE_ARCHIVE, but does not set the security descriptor that the SECURITY_ATTRIBUTES structure specifies.
            /// </summary>
            CreateAlways = 2,
            /// <summary>
            /// Opens a file. The function fails if the file does not exist. 
            /// </summary>
            OpenExisting = 3,
            /// <summary>
            /// Opens a file, always. 
            /// If a file does not exist, the function creates a file as if dwCreationDisposition is CREATE_NEW.
            /// </summary>
            OpenAlways = 4,
            /// <summary>
            /// Opens a file and truncates it so that its size is 0 (zero) bytes. The function fails if the file does not exist.
            /// The calling process must open the file with the GENERIC_WRITE access right. 
            /// </summary>
            TruncateExisting = 5
        }

        [Flags]
        private enum EFileAttributes : uint
        {
            Readonly = 0x00000001,
            Hidden = 0x00000002,
            System = 0x00000004,
            Directory = 0x00000010,
            Archive = 0x00000020,
            Device = 0x00000040,
            Normal = 0x00000080,
            Temporary = 0x00000100,
            SparseFile = 0x00000200,
            ReparsePoint = 0x00000400,
            Compressed = 0x00000800,
            Offline = 0x00001000,
            NotContentIndexed = 0x00002000,
            Encrypted = 0x00004000,
            Write_Through = 0x80000000,
            Overlapped = 0x40000000,
            NoBuffering = 0x20000000,
            RandomAccess = 0x10000000,
            SequentialScan = 0x08000000,
            DeleteOnClose = 0x04000000,
            BackupSemantics = 0x02000000,
            PosixSemantics = 0x01000000,
            OpenReparsePoint = 0x00200000,
            OpenNoRecall = 0x00100000,
            FirstPipeInstance = 0x00080000
        }

        private enum USBD_PIPE_TYPE : uint
        {
            UsbdPipeTypeControl = 0,
            UsbdPipeTypeIsochronous = 1,
            UsbdPipeTypeBulk = 2,
            UsbdPipeTypeInterrupt = 3
        }

        [StructLayout(LayoutKind.Sequential)]
        private class USB_INTERFACE_DESCRIPTOR
        {
            public byte bLength;
            public byte bDescriptorType;
            public byte bInterfaceNumber;
            public byte bAlternateSetting;
            public byte bNumEndpoints;
            public byte bInterfaceClass;
            public byte bInterfaceSubClass;
            public byte bInterfaceProtocol;
            public byte iInterface;
        }

        [StructLayout(LayoutKind.Sequential)]
        private class WINUSB_PIPE_INFORMATION
        {
            public USBD_PIPE_TYPE pipeType;
            public sbyte pipeId;
            public ushort maximumPacketSize;
            public sbyte interval;
        }

        [DllImport("winusb.dll", SetLastError = true, CharSet = CharSet.Auto)]
        public static extern bool WinUsb_Initialize(
             IntPtr DeviceHandle,
             out IntPtr InterfaceHandle
             );

        [DllImport("winusb.dll", SetLastError = true, CharSet = CharSet.Auto)]
        private static extern bool WinUsb_QueryInterfaceSettings(
             IntPtr usbInferfaceHandle,
             sbyte alternateSettingNumber,
             USB_INTERFACE_DESCRIPTOR usbAltInterfaceDescriptor);

        [DllImport("winusb.dll", SetLastError = true, CharSet = CharSet.Auto)]
        private static extern bool WinUsb_QueryPipe(
            IntPtr usbInferfaceHandle,
            sbyte alternateSettingNumber,
            sbyte pipeIndex,
            WINUSB_PIPE_INFORMATION usbAltInterfaceDescriptor);

        [DllImport("winusb.dll", SetLastError = true, CharSet = CharSet.Auto)]
        private static extern bool WinUsb_ReadPipe(
            IntPtr usbInferfaceHandle,
            sbyte pipeIndex,
            sbyte[] buffer,
            UInt32 bufferLength,
            ref UInt32 bytesTransfered,
            IntPtr overlap);

        [DllImport("kernel32.dll", SetLastError = true, CharSet = CharSet.Auto)]
        private static extern IntPtr CreateFile(
           string lpFileName,
           EFileAccess dwDesiredAccess,
           EFileShare dwShareMode,
           IntPtr lpSecurityAttributes,
           ECreationDisposition dwCreationDisposition,
           EFileAttributes dwFlagsAndAttributes,
           IntPtr hTemplateFile);

        private string m_devicePath;
        private IntPtr m_usbInferface;
        private SafeFileHandle m_safeFileHandle;
        private sbyte m_pipeID;

        private byte[] m_data = new byte[256];

        public WinUsb(string devicePath)
        {
            m_devicePath = devicePath;
        }

        public void Connect()
        {
            IntPtr fileHandler = CreateFile(m_devicePath,
                  EFileAccess.GenericRead | EFileAccess.GenericWrite,
                  EFileShare.Read | EFileShare.Write,
                  IntPtr.Zero,
                  ECreationDisposition.OpenExisting,
                  EFileAttributes.Normal | EFileAttributes.Overlapped, IntPtr.Zero);
            m_safeFileHandle = new SafeFileHandle(fileHandler, true);

            if (IntPtr.Zero == fileHandler)
            {
                throw new Win32Exception(Marshal.GetLastWin32Error());
            }

            if (!WinUsb_Initialize(fileHandler, out m_usbInferface))
            {
                throw new Win32Exception(Marshal.GetLastWin32Error());
            }
        }

        public void SetupEndPoints()
        {
            USB_INTERFACE_DESCRIPTOR usbInterfaceDescriptor = new USB_INTERFACE_DESCRIPTOR();
            if (!WinUsb_QueryInterfaceSettings(m_usbInferface,
                                            0,
                                            usbInterfaceDescriptor))
            {
                throw new Win32Exception(Marshal.GetLastWin32Error());
            }

            for (int i = 0; i < usbInterfaceDescriptor.bNumEndpoints; i++)
            {
                WINUSB_PIPE_INFORMATION pipeInfo = new WINUSB_PIPE_INFORMATION();
                if (!WinUsb_QueryPipe(m_usbInferface,
                                 0,
                                 (sbyte)i,
                                 pipeInfo))
                {
                    throw new Win32Exception(Marshal.GetLastWin32Error());
                }

                if (pipeInfo.pipeType == USBD_PIPE_TYPE.UsbdPipeTypeInterrupt)
                {
                    m_pipeID = pipeInfo.pipeId;
                }

            }

            if (m_pipeID == 0)
            {
                throw new Exception("interupt pipe no found");
            }
        }

        public IEnumerable<int> BeginRead()
        {
            sbyte[] data = new sbyte[256];
            UInt32 bytesRead = 0;
            if (!WinUsb_ReadPipe(m_usbInferface, m_pipeID, data, (UInt32)data.Length, ref bytesRead, IntPtr.Zero))
            {
                throw new Win32Exception(Marshal.GetLastWin32Error());
            }

            if (bytesRead == 0)
            {
                return null;
            }

            return data.Take((int)bytesRead).Select(s => (int)s);
        }
    }
}
