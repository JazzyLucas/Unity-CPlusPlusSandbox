#if UNITY_EDITOR

using System;
using System.Diagnostics;
using System.IO;
using JazzyLucas.Core.Utils;
using UnityEditor;
using UnityEngine;
using L = JazzyLucas.Core.Utils.Logger;

public class BuildPlugin
{
    [MenuItem("Tools/Build C++ Plugin")]
    public static void BuildCppPlugin()
    {
        // Paths
        string projectRoot = Application.dataPath;
        string cppFile = Path.Combine(projectRoot, "Plugins/PluginTesting/UnityPlugin.cpp");
        string outputDir = Path.Combine(projectRoot, "Plugins/PluginTesting");
        string outputFile = Path.Combine(outputDir, "UnityPlugin.dll");
        
        L.Log($"System PATH: {Environment.GetEnvironmentVariable("PATH")}");

        // Ensure the output directory exists
        if (!Directory.Exists(outputDir))
            Directory.CreateDirectory(outputDir);

        // Compiler command
        string compiler = "g++"; // Change to `cl` for MSVC or `clang` for macOS/Linux
        string args = $"-shared -o \"{outputFile}\" \"{cppFile}\" -std=c++17";

        // Run the build process
        Process process = new()
        {
            StartInfo = new()
            {
                FileName = compiler,
                Arguments = args,
                UseShellExecute = false,
                RedirectStandardOutput = true,
                RedirectStandardError = true,
                CreateNoWindow = true
            }
        };

        try
        {
            process.Start();
            string output = process.StandardOutput.ReadToEnd();
            string error = process.StandardError.ReadToEnd();
            process.WaitForExit();

            if (process.ExitCode == 0)
                L.Log($"C++ Plugin built successfully: {outputFile}\n{output}");
            else
                L.Log(LogSeverity.ERROR, $"C++ Plugin build failed:\n{error}");
        }
        catch (System.Exception ex)
        {
            L.Log(LogSeverity.ERROR, $"Failed to build plugin: {ex.Message}");
        }
    }
}

#endif