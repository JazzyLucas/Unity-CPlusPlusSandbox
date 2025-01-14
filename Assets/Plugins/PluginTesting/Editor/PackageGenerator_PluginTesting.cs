#if UNITY_EDITOR

using System.Collections;
using System.Collections.Generic;
using JazzyLucas.Core;
using JazzyLucas.Core.Editor;
using JazzyLucas.Packaging;
using UnityEditor;
using UnityEngine;
using L = JazzyLucas.Core.Utils.Logger;

public class PackageGenerator_PluginTesting : Editor
{
    private const string CONFIG_SO_PATH = "Assets/Plugins/PluginTesting/Editor/PackageGeneratorConfig.asset";

    [MenuItem("Tools/PluginTesting/GeneratePackage", false, 0)]
    public static void GeneratePackage()
    {
        var config = AssetDatabase.LoadAssetAtPath<PackageGeneratorConfigSO>(CONFIG_SO_PATH);
        PackageGenerator.GeneratePackage(config);
    }
}

#endif