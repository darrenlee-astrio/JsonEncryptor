using System.Runtime.InteropServices;
using System.Security;

namespace Application.Common.Extensions;

public static class SecureStringExtensions
{
    public static string? ToInsecureString(this SecureString secureString)
    {
        IntPtr ptr = IntPtr.Zero;
        try
        {
            ptr = Marshal.SecureStringToGlobalAllocUnicode(secureString);
            return Marshal.PtrToStringUni(ptr);
        }
        finally
        {
            Marshal.ZeroFreeGlobalAllocUnicode(ptr);
        }
    }
    public static byte[] ToByteArray(this SecureString secureString)
    {
        IntPtr ptr = IntPtr.Zero;
        try
        {
            ptr = Marshal.SecureStringToGlobalAllocAnsi(secureString);
            byte[] byteArray = new byte[secureString.Length];
            Marshal.Copy(ptr, byteArray, 0, secureString.Length);
            return byteArray;
        }
        finally
        {
            Marshal.ZeroFreeGlobalAllocAnsi(ptr);
        }
    }

    public static void Clear(this SecureString secureString)
    {
        IntPtr ptr = IntPtr.Zero;
        try
        {
            ptr = Marshal.SecureStringToGlobalAllocUnicode(secureString);
            for (int i = 0; i < secureString.Length; i++)
            {
                Marshal.WriteInt16(ptr, i * 2, 0);
            }
        }
        finally
        {
            Marshal.ZeroFreeGlobalAllocUnicode(ptr);
        }
    }
}
