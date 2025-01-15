using System;
using System.Runtime.InteropServices;
using UnityEngine;

public static class DynamicNativeLibrary
{
    public static IntPtr Load(string filePath)
    {
        var handle = LoadLibrary(filePath);
        if (handle == IntPtr.Zero)
        {
            Debug.LogError($"Failed to load library: {filePath}. Error Code: {Marshal.GetLastWin32Error()}");
        }
        return handle;
    }

    public static bool Unload(IntPtr libraryHandle)
    {
        if (libraryHandle == IntPtr.Zero)
        {
            Debug.LogWarning("Attempted to unload a null library handle.");
            return false;
        }

        bool result = FreeLibrary(libraryHandle);
        if (!result)
        {
            Debug.LogError($"Failed to unload library. Error Code: {Marshal.GetLastWin32Error()}");
        }
        return result;
    }

    public static T Invoke<T, TDelegate>(IntPtr libraryHandle, string functionName, params object[] parameters)
        where TDelegate : Delegate
    {
        var funcPtr = GetProcAddress(libraryHandle, functionName);
        if (funcPtr == IntPtr.Zero)
        {
            Debug.LogWarning($"Function '{functionName}' not found in the library.");
            return default;
        }

        try
        {
            var function = Marshal.GetDelegateForFunctionPointer<TDelegate>(funcPtr);
            return (T)function.DynamicInvoke(parameters);
        }
        catch (Exception ex)
        {
            Debug.LogError($"Error invoking function '{functionName}': {ex.Message}");
            return default;
        }
    }

    [DllImport("kernel32", SetLastError = true, CharSet = CharSet.Unicode)]
    public static extern IntPtr LoadLibrary(string lpFileName);

    [DllImport("kernel32", SetLastError = true)]
    [return: MarshalAs(UnmanagedType.Bool)]
    public static extern bool FreeLibrary(IntPtr hModule);

    [DllImport("kernel32", CharSet = CharSet.Ansi)]
    private static extern IntPtr GetProcAddress(IntPtr hModule, string procedureName);
}
