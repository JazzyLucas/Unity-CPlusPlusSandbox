using System;
using System.Runtime.InteropServices;
using UnityEngine;
// ReSharper disable ConvertToConstant.Local

namespace Project
{
    public class PluginTest : MonoBehaviour
    {
        private IntPtr _libraryHandle;

        private delegate float AddNumbersDelegate(float a, float b);
        private delegate IntPtr GetGreetingDelegate();
        private delegate IntPtr GetSquaresDelegate(int count);
        private delegate double ApproximatePiDelegate(int iterations);

        private void Start()
        {
            _libraryHandle = DynamicNativeLibrary.LoadLibrary(
                $"{Application.dataPath}/Plugins/PluginTesting/UnityPlugin.dll");
            if (_libraryHandle == IntPtr.Zero)
            {
                Debug.LogError("Failed to load the UnityPlugin.dll.");
                return;
            }

            try
            {
                float result = DynamicNativeLibrary.Invoke<float, AddNumbersDelegate>(_libraryHandle, "AddNumbers", 5.5f, 4.5f);
                Debug.Log($"C++ Addition Result: {result}");

                var greetingPtr = DynamicNativeLibrary.Invoke<IntPtr, GetGreetingDelegate>(_libraryHandle, "GetGreeting");
                string greeting = Marshal.PtrToStringAnsi(greetingPtr);
                Debug.Log($"C++ Greeting: {greeting}");

                int squareCount = 10;
                var squaresPtr = DynamicNativeLibrary.Invoke<IntPtr, GetSquaresDelegate>(_libraryHandle, "GetSquares", squareCount);
                float[] squares = new float[squareCount];
                Marshal.Copy(squaresPtr, squares, 0, squareCount);
                Debug.Log($"Squares from C++: {string.Join(", ", squares)}");

                int iterations = 100000;
                double piApprox = DynamicNativeLibrary.Invoke<double, ApproximatePiDelegate>(_libraryHandle, "ApproximatePi", iterations);
                Debug.Log($"Approximation of Pi with {iterations} iterations: {piApprox}");
            }
            catch (Exception ex)
            {
                Debug.LogError($"An error occurred while invoking the native plugin: {ex.Message}");
            }
            finally
            {
                DynamicNativeLibrary.FreeLibrary(_libraryHandle);
                _libraryHandle = IntPtr.Zero;
            }
        }
    }
}
