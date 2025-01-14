using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using UnityEngine;

namespace Project
{
    public class PluginTest : MonoBehaviour
    {
        [DllImport("UnityPlugin")]
        private static extern float AddNumbers(float a, float b);

        [DllImport("UnityPlugin")]
        private static extern IntPtr GetGreeting();

        void Start()
        {
            float result = AddNumbers(5.5f, 4.5f);
            Debug.Log($"C++ Addition Result: {result}");

            string greeting = Marshal.PtrToStringAnsi(GetGreeting());
            Debug.Log($"C++ Greeting: {greeting}");
        }
    }
}
